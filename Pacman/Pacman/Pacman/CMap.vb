Public Class CMap

    Private _numberOfRows As Integer

    Private _numberOfColumns As Integer

    Private _maze(,) As CCell

    Private _graph As CGraph

    Sub New(ByVal newNumberOfRows As Integer, ByVal newNumberOfColumns As Integer)

        _numberOfRows = newNumberOfRows

        _numberOfColumns = newNumberOfColumns

        ReDim MyClass._maze(_numberOfColumns, _numberOfRows)

        GenerateMaze()

        GenerateGraph()

    End Sub

    Private Sub GenerateMaze()



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
