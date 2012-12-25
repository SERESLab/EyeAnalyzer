''' <summary>
''' Represents a video recording with frames that can be rendered on request.
''' </summary>
Public Class VideoRecording

    Private _lengthMs As ULong
    Private _timeBetweenFramesMs As UInteger
    Private _width As Integer
    Private _height As Integer

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

        ' TODO load video and set these values
        _lengthMs = 66666666
        _timeBetweenFramesMs = 10
        _width = 400
        _height = 700
    End Sub

    ''' <summary>
    ''' Destructor.
    ''' </summary>
    Protected Overrides Sub Finalize()
        
    End Sub

    ''' <summary>
    ''' Renders the frame at the specified position, scaled to the fit the specified
    ''' dimensions.
    ''' </summary>
    ''' <param name="positionMs">the position within the recording, in milliseconds</param>
    ''' <param name="width">width of the frame image</param>
    ''' <param name="height">height of the frame image</param>
    Public Function drawFrameAtPosition(ByVal positionMs As ULong, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim b As Bitmap = New Bitmap(width, height, Imaging.PixelFormat.Format32bppPArgb)
        Using g = Graphics.FromImage(b)

            ' TODO scale and render the specified frame
            g.Clear(Color.Aquamarine)
            g.DrawString("Frame " & positionMs, SystemFonts.DefaultFont, Brushes.Black, 50, 50)

        End Using
        Return b
    End Function
End Class
