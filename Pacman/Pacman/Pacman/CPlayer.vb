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

        _textureInfo = New CTextureInfo(Game1.LoadTextureInfo("Content/pacmanTextureInfo.txt"))

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

        Game1._kbState = Keyboard.GetState

        If Game1._kbState.IsKeyDown(Keys.W) Then

            _drawPosition.Y -= 3

        ElseIf Game1._kbState.IsKeyDown(Keys.A) Then

            _drawPosition.X -= 3

        ElseIf Game1._kbState.IsKeyDown(Keys.S) Then

            _drawPosition.Y += 3

        ElseIf Game1._kbState.IsKeyDown(Keys.D) Then

            _drawPosition.X += 3

        End If

    End Sub

    Sub Draw(ByRef textures As SpriteBatch)

        Dim sourceRectangle, destinationRectangle As Rectangle

        Dim pacmanTexture As Texture2D = Game1._contentLoader.Load(Of Texture2D)(_textureInfo.GetFileLocation)

        sourceRectangle = New Rectangle(CInt(_textureInfo.GetPosition.X), CInt(_textureInfo.GetPosition.Y), CInt(_textureInfo.GetSize.X), CInt(_textureInfo.GetSize.Y))

        destinationRectangle = New Rectangle(CInt(_drawPosition.X), CInt(_drawPosition.Y), CInt(_textureInfo.GetSize.X), CInt(_textureInfo.GetSize.Y))

        textures.Begin()

        textures.Draw(pacmanTexture, destinationRectangle, sourceRectangle, Color.White)

        textures.End()

    End Sub

End Class
