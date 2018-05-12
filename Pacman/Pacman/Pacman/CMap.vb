Public Class CMap

    Private _numberOfRows As Integer

    Private _numberOfColumns As Integer

    Private _maze(,) As CCell

    Private _graph As CGraph

    Private _stackOfCells As Stack(Of CCell)

    Sub New(ByVal newNumberOfRows As Integer, ByVal newNumberOfColumns As Integer)

        _numberOfRows = newNumberOfRows

        _numberOfColumns = newNumberOfColumns

        ReDim MyClass._maze(_numberOfColumns, _numberOfRows)

        GenerateMaze()

        GenerateGraph()

    End Sub

    Private Sub GenerateMaze()

        _stackOfCells = New Stack(Of CCell)

        For y = 0 To CByte(_numberOfRows - 1)

            For x = 0 To CByte(_numberOfColumns - 1)

                _maze(x, y) = New CCell(New Vector2(x, y))

            Next

        Next

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



    End Sub

    Sub Update()



    End Sub

    Sub Draw(ByVal textures As SpriteBatch)

        Dim sourceRectangle, destinationRectangle As Rectangle

        textures.Begin()



        textures.End()

    End Sub

End Class
