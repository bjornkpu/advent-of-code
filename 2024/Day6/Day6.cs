namespace AoC2024.Day6;

internal static class Day6
{
    public static int Part1(string input)
    {
        var map = MapParser.Parse(input);
        var startPosition = map.GetStartPosition();
        var startDirection = map.GetStartDirection(startPosition);
        var guard = new Guard(startPosition,startDirection);
        var patrolService = new GuardPatrolService(guard, map);

        patrolService.StartPatrol();
        var distinctPositions = patrolService.GetDistinctPositions();

        return distinctPositions;
    }
    public static int Part2(string input)
    {
        return default;
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class Guard
{
    public (int x, int y) Position { get; private set; }
    public Direction Facing { get; private set; }
    private HashSet<(int x, int y)> visitedPositions;
    public bool IsDone = false;

    public Guard((int x, int y) startPosition, Direction startDirection)
    {
        Position = startPosition;
        Facing = startDirection;
        visitedPositions = new HashSet<(int x, int y)>();
        visitedPositions.Add(Position);
    }

    public void Move(Map map)
    {
        (int x, int y) nextPosition = Facing switch
        {
            Direction.Up => (Position.x, Position.y - 1),
            Direction.Down => (Position.x, Position.y + 1),
            Direction.Left => (Position.x - 1, Position.y),
            Direction.Right => (Position.x + 1, Position.y),
            _ => Position
        };

        if (!map.IsInBounds(nextPosition))
        {
            IsDone = true;
            return;
        }

        if (!map.IsObstacle(nextPosition))
        {
            Position = nextPosition;
            visitedPositions.Add(Position);
        }
        else
        {
            TurnRight();
        }
    }

    public void TurnRight()
    {
        Facing = Facing switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => Facing
        };
    }

    public HashSet<(int x, int y)> GetVisitedPositions() => visitedPositions;
}

public class Map
{
    private char[,] grid;

    public Map(char[,] grid)
    {
        this.grid = grid;
    }

    public (int, int) GetStartPosition()
    {
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                switch (grid[i, j])
                {
                    case '^':
                    case 'v':
                    case '<':
                    case '>':
                        return (i, j);
                }
            }
        }
        throw new InvalidOperationException("Starting position not found in the map.");
    }
    public Direction GetStartDirection((int, int) position)
    {
        return grid[position.Item1, position.Item2] switch
        {
            '^' => Direction.Up,
            'v' => Direction.Down,
            '<' => Direction.Left,
            '>' => Direction.Right,
            _ => throw new InvalidOperationException("Starting direction not found in the position.")
        };
    }

    public bool IsInBounds((int x, int y) position)
    {
        return position.x >= 0 && position.x < grid.GetLength(0) &&
               position.y >= 0 && position.y < grid.GetLength(1);
    }

    public bool IsObstacle((int x, int y) position)
    {
        return grid[position.x, position.y] == '#';
    }

    public void MarkPosition((int x, int y) position)
    {
        grid[position.x, position.y] = 'X';
    }
   
    public void PrintToFile(string filePath)
    {
        try
        {
            // Ensure the directory exists
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Write grid state to file
            using var writer = new StreamWriter(filePath, false); // False to overwrite the file
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    writer.Write(grid[j, i]);
                }
                writer.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }
}

public class GuardPatrolService
{
    private Guard guard;
    private Map map;

    public GuardPatrolService(Guard guard, Map map)
    {
        this.guard = guard;
        this.map = map;
    }

    public void StartPatrol()
    {
        while (true)
        {
            guard.Move(map);
            map.MarkPosition(guard.Position);

            // map.PrintToFile("../../../Day6/map.txt"); // For live debugging
            
            if (guard.IsDone)
            {
                break;
            }

        }
    }

    public int GetDistinctPositions()
    {
        return guard.GetVisitedPositions().Count;
    }
}

public static class MapParser
{
    public static Map Parse(string input)
    {
        var lines = input.ReplaceLineEndings("\n").Split('\n');
        var width = lines[0].Length;
        var height = lines.Length;
        var grid = new char[height, width];

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                grid[j, i] = lines[i][j];
            }
        }

        var map = new Map(grid);

        return map;
    }
}