Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports DirectShowLib

''' <summary>
''' Represents a video recording with frames that can be rendered on request.
''' Uses the DirectShow.NET library from: http://directshownet.sourceforge.net/
''' </summary>
Public Class VideoRecording
    Implements ISampleGrabberCB, IDisposable

    Private _lengthMs As ULong
    Private _timeBetweenFramesMs As UInteger
    Private _width As Integer
    Private _height As Integer
    Private _filterGraph As IFilterGraph2 = Nothing
    Private _seek As IMediaSeeking = Nothing
    Private _control As IMediaControl = Nothing
    Private _frameImage As Bitmap = Nothing
    Private _imageAvailable As Boolean = False
    Private _imageBytes(0) As Byte
    Private _imageFlippedBytes(0) As Byte

    Private _lastRequestedPosition As ULong = ULong.MaxValue


    ''' <summary>
    ''' Gets the length of the video in milliseconds.
    ''' </summary>
    Public ReadOnly Property LengthMs() As ULong
        Get
            Return _lengthMs
        End Get
    End Property

    ''' <summary>
    ''' Gets the number of milliseconds between frames.
    ''' </summary>
    Public ReadOnly Property TimeBetweenFramesMs() As ULong
        Get
            Return _timeBetweenFramesMs
        End Get
    End Property

    ''' <summary>
    ''' Gets the width of the video.
    ''' </summary>
    Public ReadOnly Property Width() As ULong
        Get
            Return _width
        End Get
    End Property

    ''' <summary>
    ''' Gets the height of the video.
    ''' </summary>
    Public ReadOnly Property Height() As ULong
        Get
            Return _height
        End Get
    End Property

    ''' <summary>
    ''' Creates a new video recording object from the specified video file.
    ''' </summary>
    Public Shared Function FromFile(ByVal filename As String) As VideoRecording
        Return New VideoRecording(filename)
    End Function

    ''' <summary>
    ''' Private constructor that loads the specified video file.
    ''' </summary>
    Private Sub New(ByVal filename As String)
        Try
            setupFilterGraph(filename)
            ' create frame image, assuming 24 bits per pixel for video
            _frameImage = New Bitmap(_width, _height, PixelFormat.Format24bppRgb)
        Catch
            Dispose()
        End Try

        _timeBetweenFramesMs = 60
    End Sub

    ''' <summary>
    ''' Destructor.
    ''' </summary>
    Protected Overrides Sub Finalize()
        Dispose()
    End Sub

    ''' <summary>
    ''' Release media interfaces.
    ''' </summary>
    Public Sub Dispose() Implements System.IDisposable.Dispose

        _control = Nothing
        _seek = Nothing

        If _filterGraph IsNot Nothing Then
            Marshal.ReleaseComObject(_filterGraph)
            _filterGraph = Nothing
        End If

        GC.Collect()
    End Sub

    ''' <summary>
    ''' Renders the frame at the specified position, scaled to the fit the specified
    ''' dimensions.
    ''' </summary>
    ''' <param name="positionMs">the position within the recording, in milliseconds</param>
    ''' <param name="width">width of the frame image</param>
    ''' <param name="height">height of the frame image</param>
    Public Function drawFrameAtPosition(ByVal positionMs As ULong, ByVal width As Integer, ByVal height As Integer) As Bitmap

        If Not positionMs = _lastRequestedPosition Then
            _imageAvailable = False
            Dim posRefUnits = positionMs * 10000
            DsError.ThrowExceptionForHR(_seek.SetPositions(posRefUnits, AMSeekingSeekingFlags.AbsolutePositioning, posRefUnits, AMSeekingSeekingFlags.AbsolutePositioning))

            DsError.ThrowExceptionForHR(_control.Run())
            ' spin wait for frame image to become available
            For i As Integer = 0 To Integer.MaxValue - 1
                If _imageAvailable Then
                    Exit For
                End If
            Next
            DsError.ThrowExceptionForHR(_control.Stop())
            _lastRequestedPosition = positionMs
        End If

        Dim b As Bitmap = New Bitmap(width, height, Imaging.PixelFormat.Format32bppPArgb)
        Using g = Graphics.FromImage(b)

            If Not _imageAvailable Then
                g.Clear(Color.Black)
                g.DrawString("Preview is not available.", SystemFonts.DefaultFont, Brushes.White, 50, 50)
            Else
                g.Clear(Color.Black)
                g.DrawImage(_frameImage, New Rectangle(0, 0, width, height), New Rectangle(0, 0, _width, _height), GraphicsUnit.Pixel)
            End If
        End Using
        Return b
    End Function


    ''' <summary>
    ''' Sets up the filter graph for DirectShowLib.
    ''' </summary>
    Private Sub setupFilterGraph(ByVal filename As String)

        Dim capFilter As IBaseFilter = Nothing
        Dim sampleGrabber As ISampleGrabber = Nothing
        Dim nullRenderer As IBaseFilter = Nothing

        Try
            ' create filter graph
            Dim filterGraph As IFilterGraph2 = New FilterGraph()
            DsError.ThrowExceptionForHR(filterGraph.AddSourceFilter(filename, "Ds.NET FileFilter", capFilter))


            ' configure sample grabber to use buffer callback
            sampleGrabber = New SampleGrabber()
            Dim mt As New AMMediaType()
            mt.majorType = MediaType.Video
            mt.subType = MediaSubType.RGB24
            mt.formatType = FormatType.VideoInfo
            DsError.ThrowExceptionForHR(sampleGrabber.SetMediaType(mt))
            DsUtils.FreeAMMediaType(mt)
            DsError.ThrowExceptionForHR(sampleGrabber.SetCallback(Me, 1))
            DsError.ThrowExceptionForHR(filterGraph.AddFilter(sampleGrabber, "Ds.NET Grabber"))



            ' connect media source
            Dim pinOut As IPin = DsFindPin.ByDirection(capFilter, PinDirection.Output, 0)
            Dim pinIn As IPin = DsFindPin.ByDirection(sampleGrabber, PinDirection.Input, 0)
            DsError.ThrowExceptionForHR(filterGraph.Connect(pinOut, pinIn))


            ' connect null renderer
            nullRenderer = New NullRenderer()
            DsError.ThrowExceptionForHR(filterGraph.AddFilter(nullRenderer, "Null renderer"))
            pinOut = DsFindPin.ByDirection(sampleGrabber, PinDirection.Output, 0)
            pinIn = DsFindPin.ByDirection(nullRenderer, PinDirection.Input, 0)
            DsError.ThrowExceptionForHR(filterGraph.Connect(pinOut, pinIn))


            ' read size data
            mt = New AMMediaType()
            DsError.ThrowExceptionForHR(sampleGrabber.GetConnectedMediaType(mt))
            If Not mt.formatType = FormatType.VideoInfo Or mt.formatPtr = IntPtr.Zero Then
                Throw New NotSupportedException()
            End If
            Dim info As VideoInfoHeader = Marshal.PtrToStructure(mt.formatPtr, GetType(VideoInfoHeader))
            _width = info.BmiHeader.Width
            _height = info.BmiHeader.Height
            DsUtils.FreeAMMediaType(mt)


            ' read time data
            Dim seek As IMediaSeeking = filterGraph
            Dim durationRefTime As Long ' duration in 100 ns units
            DsError.ThrowExceptionForHR(seek.GetDuration(durationRefTime))
            _lengthMs = durationRefTime * 0.0001

            _seek = seek
            _control = filterGraph
            _filterGraph = filterGraph
        Finally
            If capFilter IsNot Nothing Then
                Marshal.ReleaseComObject(capFilter)
            End If

            If sampleGrabber IsNot Nothing Then
                Marshal.ReleaseComObject(sampleGrabber)
            End If

            If nullRenderer IsNot Nothing Then
                Marshal.ReleaseComObject(nullRenderer)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Sample callback, required for ISampleGrabberCB.
    ''' </summary>
    Public Function SampleCB(ByVal sampleTime As Double, ByVal pSample As IMediaSample) As Integer _
        Implements ISampleGrabberCB.SampleCB

        Marshal.ReleaseComObject(pSample)
        Return 0
    End Function

    ''' <summary>
    ''' Buffer callback for getting frame images.
    ''' </summary>
    Public Function BufferCB(ByVal sampleTime As Double, ByVal pBuffer As System.IntPtr, ByVal bufferLen As Integer) As Integer _
        Implements ISampleGrabberCB.BufferCB

        ReDim _imageBytes(bufferLen - 1)
        ReDim _imageFlippedBytes(bufferLen - 1)
        Dim frameImageData As BitmapData = _frameImage.LockBits(New Rectangle(0, 0, _width, _height), _
            ImageLockMode.WriteOnly, _frameImage.PixelFormat)

        Marshal.Copy(pBuffer, _imageBytes, 0, bufferLen)

        ' flip image vertically
        Dim orig_i As Integer = bufferLen - frameImageData.Stride
        For i As Integer = 0 To bufferLen - 1 Step frameImageData.Stride
            For j As Integer = 0 To frameImageData.Stride - 1
                _imageFlippedBytes(i + j) = _imageBytes(orig_i + j)
            Next
            orig_i = orig_i - frameImageData.Stride
        Next

        Marshal.Copy(_imageFlippedBytes, 0, frameImageData.Scan0, bufferLen)
        _frameImage.UnlockBits(frameImageData)
        _imageAvailable = True

        Return 0
    End Function
End Class
