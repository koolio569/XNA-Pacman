Public Class CMenu

    Private _textureInfo As CTextureInfo

    Private _optionsText As List(Of String)

    Private _optionsKeys As CMenuOption

    Private _menuState As Integer

    Sub New(ByVal newOptions As List(Of String), ByVal newStates As List(Of Integer), ByVal newMenuState As Integer)

        _optionsKeys = New CMenuOption

        _optionsKeys.SetGameSates(newStates)

        _optionsText = newOptions

        _menuState = newMenuState

    End Sub

    Function GetMenuState() As Integer

        Return _menuState

    End Function

    Sub LoadTextureInfo(ByVal filePath As String)

        _textureInfo = New CTextureInfo(Game1.LoadTextureInfo(filePath))

    End Sub

    Sub Update()

        Dim gameState As Integer

        Game1._kbState = Keyboard.GetState

        If Game1._kbState.GetPressedKeys.Length > 0 Then

            gameState = _optionsKeys.GetGameStateFromKey(Game1._kbState.GetPressedKeys(0))

            If Not gameState = -1 Then

                If gameState = 1 And Game1._gameState = 5 Then

                    Dim l As Integer = 0

                End If

                Game1._gameState = gameState

            End If

        End If

    End Sub

    Sub Draw(ByRef textures As SpriteBatch)

        Dim sourceRectangle, destinationRectangle As Rectangle

        Dim textHeight As Integer = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferHeight * 0.5) - CInt((_optionsText.Count / 2) * Game1._font.MeasureString("I").Y)

        Dim textWidth As Integer = 0

        Dim menuTexture As Texture2D = Game1._contentLoader.Load(Of Texture2D)(_textureInfo.GetFileLocation)

        sourceRectangle = New Rectangle(CInt(_textureInfo.GetPosition.X), CInt(_textureInfo.GetPosition.Y), CInt(_textureInfo.GetSize.X), CInt(_textureInfo.GetSize.Y))

        destinationRectangle = New Rectangle(CInt(_textureInfo.GetPosition.X), CInt(_textureInfo.GetPosition.Y), CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth), CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferHeight))

        textures.Begin()

        textures.Draw(menuTexture, destinationRectangle, sourceRectangle, Color.White)

        For i = 0 To _optionsText.Count - 1

            textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.5) - CInt(Game1._font.MeasureString(_optionsText(i)).X / 2)

            textures.DrawString(Game1._font, _optionsText(i), New Vector2(textWidth, textHeight + (60 * i)), Color.Yellow)

        Next

        textures.End()

    End Sub

End Class
