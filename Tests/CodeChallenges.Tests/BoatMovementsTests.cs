using CodeChallengesConsole.Classes;

namespace CodeChallenges.Tests;

public class BoatMovementsTests
{
    [Theory]
    [MemberData(nameof(GetPathTestData))]
    public void GetPath_ReturnsCorrectBooleanArrayForMovementPath(bool[,] gameMatrix, int fromRow, int fromColumn, int toRow, int toColumn, bool[] expected)
    {
        // Act
        bool[] result = BoatMovements.GetPath(gameMatrix, fromRow, fromColumn, toRow, toColumn);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(CanTravelToTestData))]
    public void CanTravelTo_ShouldReturnConsistently(bool[,] gameMatrix, int fromRow, int fromColumn, int toRow,
        int toColumn, bool expected)
    {
        var result = BoatMovements.CanTravelTo(gameMatrix, fromRow, fromColumn, toRow, toColumn);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetPath_ReturnsEmptyArrayForOutOfBoundsCoordinates()
    {
        // Arrange
        bool[,] gameMatrix = new bool[3, 3];
        bool[] expected = new bool[0];

        // Act
        bool[] result = BoatMovements.GetPath(gameMatrix, 0, 0, 3, 3);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> GetPathTestData()
    {
        yield return new object[]
        {
            new bool[,] { { true, false, true }, { false, true, false }, { true, false, true } },
            0, 0, 0, 2, // Horizontal: (0,0) to (0,2)
            new bool[] { true, false, true }
        };
        yield return new object[]
        {
            new bool[,] { { true, false, true }, { false, true, false }, { true, false, true } },
            0, 0, 2, 0, // Vertical: (0,0) to (2,0)
            new bool[] { true, false, true }
        };
        // Optionally add more horizontal/vertical cases
        yield return new object[]
        {
            new bool[,] { { true, false, true }, { false, true, false }, { true, false, true } },
            1, 0, 1, 2, // Horizontal: (1,0) to (1,2)
            new bool[] { false, true, false }
        };
        
        // Case 1: 6x6 matrix, Horizontal movement
        bool[,] matrix6x6 = 
        {
            { false, true,  true,  false, false, false },
            { true,  true,  true,  false, false, false },
            { true,  true,  true,  true,  true,  true  },
            { false, true,  true,  false, true,  true  },
            { false, true,  true,  true,  false, true  },
            { false, false, false, false, false, false }
        };
        yield return new object[]
        {
            matrix6x6,
            2, 0, 2, 5,
            new bool[] { true, true, true, true, true, true }
        };

        // Case 2: 8x8 matrix, Vertical movement
        bool[,] matrix8x8 = 
        {
            { false, true,  true,  false, false, false, true,  false },
            { true,  true,  true,  false, false, false, true,  true  },
            { true,  true,  true,  true,  true,  true,  false, false },
            { false, true,  true,  false, true,  true,  false, true  },
            { false, true,  true,  true,  false, true,  true,  false },
            { false, false, false, false, false, false, true,  true  },
            { true,  false, true,  true,  false, false, false, true  },
            { false, true,  false, false, true,  true,  false, false }
        };
        yield return new object[]
        {
            matrix8x8,
            0, 1, 7, 1,
            new bool[] { true, true, true, true, true, false, false, true }
        };

        // Case 3: 10x10 matrix, Horizontal movement
        bool[,] matrix10x10 = 
        {
            { false, true,  true,  false, false, false, true,  false, true,  true  },
            { true,  true,  true,  false, false, false, true,  true,  false, false },
            { true,  true,  true,  true,  true,  true,  false, false, true,  false },
            { false, true,  true,  false, true,  true,  false, true,  false, true  },
            { false, true,  true,  true,  false, true,  true,  false, true,  false },
            { false, false, false, false, false, false, true,  true,  false, true  },
            { true,  false, true,  true,  false, false, false, true,  true,  false },
            { false, true,  false, false, true,  true,  false, false, true,  true  },
            { true,  false, true,  false, false, true,  true,  false, false, false },
            { false, true,  false, true,  true,  false, false, true,  false, true  }
        };
        yield return new object[]
        {
            matrix10x10,
            4, 2, 4, 7,
            new bool[] { true, true, false, true, true, false }
        };

        // Case 4: 10x10 matrix, Vertical movement
        yield return new object[]
        {
            matrix10x10,
            3, 6, 8, 6,
            new bool[] { false, true, true, false, false, true }
        };
    }
    
    public static IEnumerable<object[]> CanTravelToTestData() 
    {
        bool[,] matrix6x6 = 
        {
            { false, true,  true,  false, false, false },
            { true,  true,  true,  false, false, false },
            { true,  true,  true,  true,  true,  true  },
            { false, true,  true,  false, true,  true  },
            { false, true,  true,  true,  false, true  },
            { false, false, false, false, false, false }
        };
        yield return new object[] { matrix6x6, 2, 0, 2, 5, true };  // All true
        yield return new object[] { matrix6x6, 1, 0, 1, 5, false }; // Mixed

        bool[,] matrix8x8 = 
        {
            { false, true,  true,  false, false, false, true,  false },
            { true,  true,  true,  false, false, false, true,  true  },
            { true,  true,  true,  true,  true,  true,  false, false },
            { false, true,  true,  false, true,  true,  false, true  },
            { false, true,  true,  true,  false, true,  true,  false },
            { false, false, false, false, false, false, true,  true  },
            { true,  false, true,  true,  false, false, false, true  },
            { false, true,  false, false, true,  true,  false, false }
        };
        yield return new object[] { matrix8x8, 2, 4, 4, 4, false }; // Mixed
        yield return new object[] { matrix8x8, 0, 1, 7, 1, false }; // Mixed
        yield return new object[] { matrix8x8, 2, 0, 2, 5, true };

        bool[,] matrix10x10 = 
        {
            { false, true,  true,  false, false, false, true,  false, true,  true  },
            { true,  true,  true,  false, false, false, true,  true,  false, false },
            { true,  true,  true,  true,  true,  true,  false, false, true,  false },
            { false, true,  true,  false, true,  true,  false, true,  false, true  },
            { false, true,  true,  true,  false, true,  true,  false, true,  false },
            { false, false, false, false, false, false, true,  true,  false, true  },
            { true,  false, true,  true,  false, false, false, true,  true,  false },
            { false, true,  false, false, true,  true,  false, false, true,  true  },
            { true,  false, true,  false, false, true,  true,  false, false, false },
            { false, true,  false, true,  true,  false, false, true,  false, true  }
        };
        yield return new object[] { matrix10x10, 0, 0, 6, 0, false }; 
        yield return new object[] { matrix10x10, 0, 0, 10, 0, false }; // Out-of-bounds
        yield return new object[] { matrix10x10, 2, 0, 2, 5, true }; 
        
        // New scenario cases with 6x6 matrix
        yield return new object[] { matrix6x6, 3, 2, 2, 2, true };  // Vertical, all true
        yield return new object[] { matrix6x6, 3, 2, 3, 4, false }; // Horizontal, mixed
        yield return new object[] { matrix6x6, 3, 2, 6, 2, false }; // Out-of-bounds
    }
    
}