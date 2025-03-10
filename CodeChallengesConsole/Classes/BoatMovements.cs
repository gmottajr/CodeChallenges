namespace CodeChallengesConsole.Classes;

public static class BoatMovements
{
    public static bool[] GetPath(bool[,] gameMatrix, int fromRow, int fromColumn, int toRow, int toColumn)
    {
        // Get grid dimensions
        int rows = gameMatrix.GetLength(0);
        int cols = gameMatrix.GetLength(1);

        // Validate coordinates
        if (!IsValidPosition(fromRow, fromColumn, rows, cols) || !IsValidPosition(toRow, toColumn, rows, cols))
        {
            return new bool[0];
        }

        // Calculate differences
        int deltaRow = toRow - fromRow;
        int deltaCol = toColumn - fromColumn;

        // Determine path length (number of steps including start and end)
        int steps = Math.Max(Math.Abs(deltaRow), Math.Abs(deltaCol)) + 1;

        // Resulting path array
        bool[] path = new bool[steps];
        int currentStep = 0;

        // Handle different movement types
        if (deltaRow == 0) // Horizontal movement
        {
            int startCol = Math.Min(fromColumn, toColumn);
            for (int col = startCol; col <= Math.Max(fromColumn, toColumn); col++)
            {
                path[currentStep++] = gameMatrix[fromRow, col];
            }
        }
        else if (deltaCol == 0) // Vertical movement
        {
            int startRow = Math.Min(fromRow, toRow);
            var finalToRow = Math.Max(fromRow, toRow);
            for (int row = startRow; row <= finalToRow; row++)
            {
                path[currentStep++] = gameMatrix[row, fromColumn];
            }
        }
        
        return path;
    }
    
    public static bool IsValidPosition(int row, int col, int rows, int cols)
    {
        return row >= 0 && row < rows && col >= 0 && col < cols;
    }
    
    public static bool CanTravelTo(bool[,] gameMatrix, int fromRow, int fromColumn, int toRow, int toColumn)
    {
        var theWay = GetPath(gameMatrix, fromRow, fromColumn, toRow, toColumn);
        return theWay.Length > 0 && theWay.All(t => t);
    }
}