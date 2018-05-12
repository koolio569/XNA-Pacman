Public Class Game1

    Inherits Microsoft.Xna.Framework.Game

    Public Shared WithEvents _graphics As GraphicsDeviceManager

    Private WithEvents _spriteBatch As SpriteBatch

    Public Shared _contentLoader As ContentManager

    Public Shared _gameState As Integer

    Public Shared _kbState As KeyboardState

    Public Shared _font As SpriteFont

    Public Shared _currentMenu As CMenu

    Public Shared _player As CPlayer

    Public Shared _currentMap As CMap

    Public Enum GameStateEnum

        Exiting = 0

        MainMenu = 1

        Playing = 2

        GameOver = 3

        Paused = 4

        HighScores = 5

        NewGame = 6

    End Enum

    Public Sub New()

        _graphics = New GraphicsDeviceManager(Me)

        Content.RootDirectory = "Content"

        _graphics.PreferredBackBufferWidth = 1280

        _graphics.PreferredBackBufferHeight = 768

        _graphics.ApplyChanges()

    End Sub

    Protected Overrides Sub Initialize()

        MyBase.Initialize()

        _contentLoader = Content

        _font = Content.Load(Of SpriteFont)("font")

        _gameState = GameStateEnum.MainMenu

        _currentMenu = New CMenu(New List(Of String), New List(Of Integer), -1)

        _player = New CPlayer

    End Sub

    Public Shared Function LoadListOfString(ByVal filePath As String) As List(Of String)

        Dim textFile As New System.IO.StreamReader(filePath)

        Dim listOfString As New List(Of String)

        While Not textFile.EndOfStream

            listOfString.Add(CStr(textFile.ReadLine))

        End While

        textFile.Close()

        Return listOfString

    End Function

    Public Shared Function LoadListOfInteger(ByVal filePath As String) As List(Of Integer)

        Dim integerFile As New System.IO.StreamReader(filePath)

        Dim listOfInteger As New List(Of Integer)

        While Not integerFile.EndOfStream

            listOfInteger.Add(CInt(integerFile.ReadLine))

        End While

        integerFile.Close()

        Return listOfInteger

    End Function

    Public Shared Function LoadTextureInfo(ByVal filePath As String) As List(Of String)

        Dim textureFile As New System.IO.StreamReader(filePath)

        Dim listOfTextureInfo As New List(Of String)

        While Not textureFile.EndOfStream

            listOfTextureInfo.Add(CStr(textureFile.ReadLine))

        End While

        Return listOfTextureInfo

    End Function

    Protected Overrides Sub LoadContent()

        _spriteBatch = New SpriteBatch(GraphicsDevice)

    End Sub

    Protected Overrides Sub UnloadContent()

    End Sub

    Private Sub StartNewGame()

        _currentMap = New CMap(12, 12)

        _player = New CPlayer

        Const FPpauseMenuText As String = "Content/PauseMenuText.txt"

        Const FPpauseMenuStates As String = "Content/PauseMenuStates.txt"

        _currentMenu = New CMenu(LoadListOfString(FPpauseMenuText), LoadListOfInteger(FPpauseMenuStates), GameStateEnum.Paused)

        _currentMenu.LoadTextureInfo("Content/PauseMenuTextureInfo.txt")

        _gameState = GameStateEnum.Playing

    End Sub

    Protected Overrides Sub Update(ByVal gameTime As GameTime)

        MyBase.Update(gameTime)

        Me.TargetElapsedTime = TimeSpan.FromSeconds(1.0F / 10.0F)

        _kbState = Keyboard.GetState

        Select Case _gameState

            Case GameStateEnum.Exiting

                _kbState = Keyboard.GetState

                Me.Exit()

            Case GameStateEnum.MainMenu

                If _currentMenu.GetMenuState <> GameStateEnum.MainMenu Then

                    Const FPmainMenuText As String = "Content/MainMenuText.txt"

                    Const FPmainMenuStates As String = "Content/MainMenuStates.txt"

                    _currentMenu = New CMenu(LoadListOfString(FPmainMenuText), LoadListOfInteger(FPmainMenuStates), GameStateEnum.MainMenu)

                    _currentMenu.LoadTextureInfo("Content/MainMenuTextureInfo.txt")

                End If

                _currentMenu.Update()

            Case GameStateEnum.Playing

                _player.Update()

                _currentMap.Update()

            Case GameStateEnum.HighScores

                If _currentMenu.GetMenuState <> GameStateEnum.HighScores Then

                    Const FPhighScoreMenuText As String = "Content/HighScoreMenuText.txt"

                    Const FPhighScoreMenuStates As String = "Content/HighScoreMenuStates.txt"

                    _currentMenu = New CMenu(LoadListOfString(FPhighScoreMenuText), LoadListOfInteger(FPhighScoreMenuStates), GameStateEnum.HighScores)

                    _currentMenu.LoadTextureInfo("Content/HighScoreMenuTextureInfo.txt")

                End If

                _currentMenu.Update()

            Case GameStateEnum.Paused

                If _currentMenu.GetMenuState <> GameStateEnum.Paused Then

                    Const FPpauseMenuText As String = "Content/PauseMenuText.txt"

                    Const FPpauseMenuStates As String = "Content/PauseMenuStates.txt"

                    _currentMenu = New CMenu(LoadListOfString(FPpauseMenuText), LoadListOfInteger(FPpauseMenuStates), GameStateEnum.Paused)

                    _currentMenu.LoadTextureInfo("Content/PauseMenuTextureInfo.txt")

                End If

                _currentMenu.Update()

            Case GameStateEnum.NewGame

            Case GameStateEnum.GameOver

        End Select

    End Sub

    Protected Overrides Sub Draw(ByVal gameTime As GameTime)

        GraphicsDevice.Clear(Color.Black)

        MyBase.Draw(gameTime)

        Select Case _gameState

            Case GameStateEnum.MainMenu, GameStateEnum.HighScores

                _currentMenu.Draw(_spriteBatch)

            Case GameStateEnum.Playing

                _currentMap.Draw(_spriteBatch)

                _player.Draw()

            Case GameStateEnum.NewGame

                StartNewGame()

            Case GameStateEnum.Paused

                _currentMap.Draw(_spriteBatch)

                _player.Draw()

                _currentMenu.Draw(_spriteBatch)

        End Select

    End Sub

End Class
