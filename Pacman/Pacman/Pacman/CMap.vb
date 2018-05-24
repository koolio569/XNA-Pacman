Public Class CMap

    Private _numberOfRows As Integer

    Private _numberOfColumns As Integer

    Private _maze(,) As CCell

    Private _graph As CGraph

    Private _stackOfCells As Stack(Of CCell)

    Private _textureInfo As CTextureInfo

    Private _numberOfAI As Integer

    Private _mapAI As List(Of CAi)

    Enum ItemEnum

        coin = 10

        megacoin = 50

        cherry = 100

    End Enum

    Sub New(ByVal newNumberOfRows As Integer, ByVal newNumberOfColumns As Integer)

        _numberOfRows = newNumberOfRows

        _numberOfColumns = newNumberOfColumns

        ReDim MyClass._maze(_numberOfColumns, _numberOfRows)

        LoadTextureInfo("Content/MapTextureInfo.txt")

        GenerateMaze()

        GenerateGraph()

        _mapAI = New List(Of CAi)

        _numberOfAI = 3

        For i = 1 To _numberOfAI

            _mapAI.Add(New CAi(New Vector2(0, 0), CAi.comptence.medium))

        Next

    End Sub

    Function GetMaze() As CCell(,)

        Return _maze

    End Function

    Function GetGraph() As CGraph

        Return _graph

    End Function

    Sub LoadTextureInfo(ByVal filePath As String)

        _textureInfo = New CTextureInfo(Game1.LoadTextureInfo(filePath))

    End Sub

    Private Sub GenerateMaze()

        Const numberOfMegaCoins As Integer = 10

        _stackOfCells = New Stack(Of CCell)

        For y = 0 To CByte(_numberOfRows - 1)

            For x = 0 To CByte(_numberOfColumns - 1)

                _maze(x, y) = New CCell(New Vector2(x, y))

            Next

        Next

        _maze(0, 0).SetVisited(True)

        Algorithim(_maze(0, 0))

        For y = 0 To CByte(_numberOfRows - 1)

            For x = 0 To CByte(_numberOfColumns - 1)

                _maze(x, y).SetSymbol()

                _maze(x, y).SetItem(ItemEnum.coin)

            Next

        Next

        _maze(0, 0).SetItem(ItemEnum.megacoin)

        Dim randX, randY As Integer

        For i = 1 To numberOfMegaCoins

            Do

                Randomize()

                randX = CInt(Math.Floor(Rnd() * _numberOfColumns))

                randY = CInt(Math.Floor(Rnd() * _numberOfRows))

            Loop Until _maze(randX, randY).GetItem <> ItemEnum.megacoin

            _maze(randX, randY).SetItem(ItemEnum.megacoin)

        Next

        _maze(0, 0).SetItem(ItemEnum.coin)

    End Sub

    Sub Algorithim(ByRef current As CCell)

        Dim neighbourghCell As CCell = CheckNeighbourghs(_maze(current.GetPositionX, current.GetPositionY))

        If neighbourghCell.GetPositionX = -1 And neighbourghCell.GetPositionY = -1 Then

            If _stackOfCells.Count > 0 Then

                Algorithim(_stackOfCells.Pop())

            End If

        Else

            _maze(neighbourghCell.GetPositionX, neighbourghCell.GetPositionY).SetVisited(True)

            _stackOfCells.Push(current)

            RemoveWalls(_maze(current.GetPositionX, current.GetPositionY), _maze(neighbourghCell.GetPositionX, neighbourghCell.GetPositionY))

            Algorithim(neighbourghCell)

        End If

    End Sub

    Function CheckNeighbourghs(ByRef current As CCell) As CCell

        Dim neighbours As New List(Of CCell)

        If current.GetPositionX + 1 < _numberOfColumns Then

            If _maze(current.GetPositionX + 1, current.GetPositionY).GetVisited = False Then

                neighbours.Add(_maze(current.GetPositionX + 1, current.GetPositionY))

            End If

        End If

        If current.GetPositionX - 1 > -1 Then

            If _maze(current.GetPositionX - 1, current.GetPositionY).GetVisited = False Then

                neighbours.Add(_maze(current.GetPositionX - 1, current.GetPositionY))

            End If

        End If

        If current.GetPositionY + 1 < _numberOfRows Then

            If _maze(current.GetPositionX, current.GetPositionY + 1).GetVisited = False Then

                neighbours.Add(_maze(current.GetPositionX, current.GetPositionY + 1))

            End If

        End If

        If current.GetPositionY - 1 > -1 Then

            If _maze(current.GetPositionX, current.GetPositionY - 1).GetVisited = False Then

                neighbours.Add(_maze(current.GetPositionX, current.GetPositionY - 1))

            End If

        End If

        Randomize()

        If neighbours.Count > 0 Then

            Return neighbours(CInt(Math.Floor(Rnd() * neighbours.Count)))

        Else

            Return New CCell(New Vector2(-1, -1))

        End If

    End Function

    Sub RemoveWalls(ByRef current As CCell, ByRef neighbour As CCell)

        Dim xResult As Integer = current.GetPositionX - neighbour.GetPositionX

        Dim yResult As Integer = current.GetPositionY - neighbour.GetPositionY

        If xResult = 0 Then

            If yResult = -1 Then

                current.SetWall(2, False)

                neighbour.SetWall(0, False)

            Else

                current.SetWall(0, False)

                neighbour.SetWall(2, False)

            End If

        ElseIf xResult = -1 Then

            current.SetWall(1, False)

            neighbour.SetWall(3, False)

        Else

            current.SetWall(3, False)

            neighbour.SetWall(1, False)

        End If

    End Sub

    Private Sub GenerateGraph()

        Dim listOfNodes As New List(Of CNode)

        Dim identifer As Integer = 0

        Dim position As Vector2

        Dim listOfNeighbours As List(Of Integer)

        For y = 0 To _numberOfRows - 1

            For x = 0 To _numberOfColumns - 1

                identifer = ((y * _numberOfRows) + x)

                position = New Vector2(x, y)

                listOfNeighbours = GetListOfNodeNeighbours(x, y)

                listOfNodes.Add(New CNode(identifer, position, listOfNeighbours))

            Next

        Next

        _graph = New CGraph("Map:1", listOfNodes)

    End Sub

    Private Function GetListOfNodeNeighbours(ByVal xPosition As Integer, ByVal yPosition As Integer) As List(Of Integer)

        Dim listOfIdentifier As New List(Of Integer)

        'North
        If _maze(xPosition, yPosition).GetWalls(0) = False Then

            listOfIdentifier.Add((((yPosition - 1) * _numberOfRows) + xPosition))

        End If

        'East
        If _maze(xPosition, yPosition).GetWalls(1) = False Then

            listOfIdentifier.Add(((yPosition * _numberOfRows) + (xPosition + 1)))

        End If

        'South
        If _maze(xPosition, yPosition).GetWalls(2) = False Then

            listOfIdentifier.Add((((yPosition + 1) * _numberOfRows) + xPosition))

        End If

        'West
        If _maze(xPosition, yPosition).GetWalls(3) = False Then

            listOfIdentifier.Add((((yPosition - 1) * _numberOfRows) + (xPosition - 1)))

        End If

        Return listOfIdentifier

    End Function

    Sub Update()

        Game1._kbState = Keyboard.GetState

        If Game1._kbState.IsKeyDown(Keys.D1) Then

        ElseIf Game1._oldKbState.IsKeyDown(Keys.D1) Then

            Game1._gameState = Game1.GameStateEnum.Paused

        End If

        If Game1._kbState.IsKeyDown(Keys.D2) Then

        ElseIf Game1._oldKbState.IsKeyDown(Keys.D2) Then

            Game1._gameState = Game1.GameStateEnum.MainMenu

            Game1.LoadMainMenu()

        End If

        Game1._oldKbState = Game1._kbState

        'player picks up item

    End Sub

    Sub Draw(ByRef textures As SpriteBatch)

        Const tileMapTextureColumns As Integer = 8

        Const tileMapTextureSize As Integer = 64

        Dim tileMapTexture As Texture2D = Game1._contentLoader.Load(Of Texture2D)(_textureInfo.GetFileLocation)

        Dim sourceRectangle, destinationRectangle As Rectangle

        Dim a, b As Integer

        textures.Begin()

        For y = 0 To _numberOfColumns - 1

            For x = 0 To _numberOfRows - 1

                'Texture X position
                a = (_maze(x, y).GetSymbol Mod tileMapTextureColumns) * tileMapTextureSize

                'Texture Y position
                b = CInt(Math.Floor(_maze(x, y).GetSymbol / tileMapTextureColumns)) * tileMapTextureSize

                sourceRectangle = New Rectangle(a, b, tileMapTextureSize, tileMapTextureSize)

                destinationRectangle = New Rectangle((x * tileMapTextureSize), (y * tileMapTextureSize), tileMapTextureSize, tileMapTextureSize)

                textures.Draw(tileMapTexture, destinationRectangle, sourceRectangle, Color.White)

                Select Case _maze(x, y).GetItem

                    Case ItemEnum.coin

                        sourceRectangle = New Rectangle(0, 128, tileMapTextureSize, tileMapTextureSize)

                        textures.Draw(tileMapTexture, destinationRectangle, sourceRectangle, Color.White)

                    Case ItemEnum.megacoin

                        sourceRectangle = New Rectangle(64, 128, tileMapTextureSize, tileMapTextureSize)

                        textures.Draw(tileMapTexture, destinationRectangle, sourceRectangle, Color.White)

                End Select

            Next

        Next

        Dim scoreText As String = "Score: " + CStr(Game1._player.GetScore)

        Dim livesText As String = "Lives: " + CStr(Game1._player.GetLives)

        Dim textHeight As Integer = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferHeight * 0.5) - CInt(Game1._font.MeasureString("I").Y)

        Dim textWidth As Integer = 0

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(scoreText).X / 2)

        textures.DrawString(Game1._font, scoreText, New Vector2(textWidth, textHeight), Color.Yellow)

        textWidth = CInt(Game1._graphics.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.8) - CInt(Game1._font.MeasureString(livesText).X / 2)

        textures.DrawString(Game1._font, livesText, New Vector2(textWidth, textHeight + 60), Color.Yellow)

        textures.End()

    End Sub

End Class
