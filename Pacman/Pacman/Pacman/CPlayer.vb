Public Class CPlayer

    Private _score As Integer

    Private _lives As Byte

    Private _position As Vector2

    Private _drawPosition As Vector2

    Private _textureInfo As CTextureInfo

    Private _animationOpen As Boolean

    Private _orientation As Integer

    Enum oreientation

        up = 0

        down = 1

        left = 2

        right = 3

    End Enum

    Sub New()

        _score = 0

        _lives = 3

        _position = New Vector2(0, 0)

        _drawPosition = New Vector2(0, 0)

        _textureInfo = New CTextureInfo(Game1.LoadTextureInfo("Content/pacmanTextureInfo.txt"))

        _animationOpen = False

        _orientation = -1

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

            If _orientation = -1 Then

                _orientation = oreientation.up

            End If

        ElseIf Game1._oldKbState.IsKeyDown(Keys.W) Then

            _orientation = oreientation.up

        End If

        If Game1._kbState.IsKeyDown(Keys.S) Then

            If _orientation = -1 Then

                _orientation = oreientation.down

            End If

        ElseIf Game1._oldKbState.IsKeyDown(Keys.S) Then

            _orientation = oreientation.down

        End If

        If Game1._kbState.IsKeyDown(Keys.A) Then

            If _orientation = -1 Then

                _orientation = oreientation.left

            End If

        ElseIf Game1._oldKbState.IsKeyDown(Keys.A) Then

            _orientation = oreientation.left

        End If

        If Game1._kbState.IsKeyDown(Keys.D) Then

            If _orientation = -1 Then

                _orientation = oreientation.right

            End If

        ElseIf Game1._oldKbState.IsKeyDown(Keys.D) Then

            _orientation = oreientation.right

        End If

        Select Case _orientation
            Case oreientation.up

                'load up animation
                _drawPosition.Y -= 2

            Case oreientation.down

                'load down animation
                _drawPosition.Y += 2

            Case oreientation.right

                _drawPosition.X += 2

            Case oreientation.left

                _drawPosition.X -= 2

        End Select

    End Sub

    Sub Draw(ByRef textures As SpriteBatch)

        Dim sourceRectangle, destinationRectangle As Rectangle

        Dim pacmanTexture As Texture2D = Game1._contentLoader.Load(Of Texture2D)(_textureInfo.GetFileLocation)

        sourceRectangle = New Rectangle(CInt(_textureInfo.GetPosition.X), CInt(_textureInfo.GetPosition.Y), CInt(_textureInfo.GetSize.X), CInt(_textureInfo.GetSize.Y))

        destinationRectangle = New Rectangle(CInt(_drawPosition.X), CInt(_drawPosition.Y), CInt(_textureInfo.GetSize.X), CInt(_textureInfo.GetSize.Y))

        textures.Begin()

        textures.Draw(pacmanTexture, destinationRectangle, sourceRectangle, Color.White)

        debug(textures)

        textures.End()

    End Sub

    Sub debug(ByVal textures As SpriteBatch)

        Dim textWidth As Integer

        Dim livesText As String = CStr(_drawPosition.X)

        Dim textHeight As Integer = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferHeight * 0.5) - CInt(Game1._font.MeasureString("I").Y)

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(livesText).X / 2)

        textures.DrawString(Game1._font, livesText, New Vector2(textWidth, textHeight + 120), Color.Yellow)

        livesText = CStr(_drawPosition.Y)

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(livesText).X / 2)

        textures.DrawString(Game1._font, livesText, New Vector2(textWidth, textHeight + 180), Color.Yellow)

        livesText = CStr(_position.X)

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(livesText).X / 2)

        textures.DrawString(Game1._font, livesText, New Vector2(textWidth, textHeight + 240), Color.Yellow)

        livesText = CStr(_position.X)

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(livesText).X / 2)

        textures.DrawString(Game1._font, livesText, New Vector2(textWidth, textHeight + 300), Color.Yellow)

    End Sub

End Class
