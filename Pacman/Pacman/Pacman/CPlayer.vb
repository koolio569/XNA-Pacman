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

        Const movementSpeed As Integer = 4


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

                If _drawPosition.Y Mod 64 = 0 Then

                    If Game1._currentMap.GetMaze(CInt(_position.X), CInt(_position.Y)).GetWalls(0) = True Then

                        _orientation = -1

                    Else

                        _drawPosition.Y -= movementSpeed

                    End If

                Else

                    _drawPosition.Y -= movementSpeed

                End If

                _position.Y = CSng(Math.Floor((_drawPosition.Y / 64)))

            Case oreientation.down

                'load down animation
                If (_drawPosition.Y + 64) Mod 64 = 0 Then

                    If Game1._currentMap.GetMaze(CInt(_position.X), CInt(_position.Y)).GetWalls(2) = True Then

                        _orientation = -1

                    Else

                        _drawPosition.Y += movementSpeed

                    End If

                Else

                    _drawPosition.Y += movementSpeed

                End If

                _position.Y = CSng(Math.Floor((_drawPosition.Y / 64)))

            Case oreientation.right

                If (_drawPosition.X + 64) Mod 64 = 0 Then

                    If Game1._currentMap.GetMaze(CInt(_position.X), CInt(_position.Y)).GetWalls(1) = True Then

                        _orientation = -1

                    Else

                        _drawPosition.X += movementSpeed

                    End If

                Else

                    _drawPosition.X += movementSpeed

                End If

                _position.X = CSng(Math.Floor((_drawPosition.X / 64)))

            Case oreientation.left

                If _drawPosition.X Mod 64 = 0 Then

                    If Game1._currentMap.GetMaze(CInt(_position.X), CInt(_position.Y)).GetWalls(3) = True Then

                        _orientation = -1

                    Else

                        _drawPosition.X -= movementSpeed

                    End If

                Else

                    _drawPosition.X -= movementSpeed

                End If

                _position.X = CSng(Math.Floor((_drawPosition.X / 64)))

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

        livesText = "North: " & CStr(Game1._currentMap.GetMaze(CInt(_position.X), CInt(_position.Y)).GetWalls(0))

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(livesText).X / 2)

        textures.DrawString(Game1._font, livesText, New Vector2(textWidth, textHeight - 240), Color.Yellow)

        livesText = "South: " & CStr(Game1._currentMap.GetMaze(CInt(_position.X), CInt(_position.Y)).GetWalls(2))

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(livesText).X / 2)

        textures.DrawString(Game1._font, livesText, New Vector2(textWidth, textHeight - 120), Color.Yellow)

        livesText = "East: " & CStr(Game1._currentMap.GetMaze(CInt(_position.X), CInt(_position.Y)).GetWalls(1))

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(livesText).X / 2)

        textures.DrawString(Game1._font, livesText, New Vector2(textWidth, textHeight - 180), Color.Yellow)

        livesText = "West: " & CStr(Game1._currentMap.GetMaze(CInt(_position.X), CInt(_position.Y)).GetWalls(3))

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(livesText).X / 2)

        textures.DrawString(Game1._font, livesText, New Vector2(textWidth, textHeight - 60), Color.Yellow)

        livesText = CStr(_position.Y)

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(livesText).X / 2)

        textures.DrawString(Game1._font, livesText, New Vector2(textWidth, textHeight + 300), Color.Yellow)

    End Sub

End Class
