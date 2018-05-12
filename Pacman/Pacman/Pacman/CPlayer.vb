Public Class CPlayer

    Private _score As Integer

    Private _lives As Byte

    Private _position As Vector2

    Private _drawPosition As Vector2

    Private _textureInfo As CTextureInfo

    Sub New()

        _score = 0

        _lives = 3

        _position = New Vector2(0, 0)

        _drawPosition = New Vector2(0, 0)

        '_textureInfo = New CTextureInfo(Game1.LoadTextureInfo("pacmanTexture.txt"))

    End Sub

    Function GetScore() As Integer

        Return _score

    End Function

    Function GetLives() As Byte

        Return _lives

    End Function

    Function GetPosition() As Vector2

        Return _position

    End Function

    Function GetTextureInfo() As CTextureInfo

        Return _textureInfo

    End Function

    Sub AddToScore(ByVal additionalScore As Integer)

        _score += additionalScore

    End Sub

    Sub AddToLives(ByVal additionalLives As Byte)

        _lives += additionalLives

    End Sub

    Sub LoadTextureInfo(ByVal filePath As String)

        _textureInfo = New CTextureInfo(Game1.LoadTextureInfo(filePath))

    End Sub

    Sub Update()



    End Sub

    Sub Draw()



    End Sub

End Class
