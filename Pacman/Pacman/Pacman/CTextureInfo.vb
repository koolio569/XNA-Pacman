Public Class CTextureInfo

    Private _fileLocation As String

    Private _size As Vector2

    Private _position As Vector2

    Sub New(ByVal loadedTextureInfo As List(Of String))

        If loadedTextureInfo.Count = 5 Then

            _fileLocation = loadedTextureInfo(0)

            _size = New Vector2(CSng(loadedTextureInfo(1)), CSng(loadedTextureInfo(2)))

            _position = New Vector2(CSng(loadedTextureInfo(3)), CSng(loadedTextureInfo(4)))

        Else

            _fileLocation = ""

            _size = New Vector2(0, 0)

            _position = New Vector2(0, 0)

        End If

    End Sub

    Function GetFileLocation() As String

        Return _fileLocation

    End Function

    Function GetSize() As Vector2

        Return _size

    End Function

    Function GetPosition() As Vector2

        Return _position

    End Function

    Sub SetFileLocation(ByVal newFileLocation As String)

        _fileLocation = newFileLocation

    End Sub

    Sub SetSize(ByVal newX As Single, ByVal newY As Single)

        _size = New Vector2(newX, newY)

    End Sub

    Sub SetPosition(ByVal newX As Single, ByVal newY As Single)

        _position = New Vector2(newX, newY)

    End Sub

End Class
