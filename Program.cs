using System.Reflection;

enum Direction { NORTH, EAST, SOUTH, WEST }

class Robot
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Direction Facing { get; private set; }
    private bool isPlaced = false;

    public void Place(int x, int y, Direction facing)
    {
        if (x >= 0 && x <= 4 && y >= 0 && y <= 4)
        {
            X = x;
            Y = y;
            Facing = facing;
            isPlaced = true;
        }
    }

    public void Move()
    {
        if (!isPlaced) return;

        switch (Facing)
        {
            case Direction.NORTH:
                if (Y < 4) Y++;
                break;
            case Direction.EAST:
                if (X < 4) X++;
                break;
            case Direction.SOUTH:
                if (Y > 0) Y--;
                break;
            case Direction.WEST:
                if (X > 0) X--;
                break;
        }
    }

    public void Left()
    {
        if (!isPlaced) return;
        Facing = (Direction)(((int)Facing + 3) % 4);
    }

    public void Right()
    {
        if (!isPlaced) return;
        Facing = (Direction)(((int)Facing + 1) % 4);
    }

    public void Report()
    {
        if (!isPlaced) return;
        Console.WriteLine($"{X},{Y},{Facing}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var robot = new Robot();
        string currentDirectory = System.IO.Directory.GetCurrentDirectory();
        string pathBeforeBin = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(currentDirectory));

        string filePath = string.Concat(pathBeforeBin.Replace("bin",""), "commands.txt");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("The file commands.txt does not exist!");
            return;
        }

        var commands = File.ReadAllLines(filePath);

        foreach (var command in commands)
        {
            var parts = command.Split(' ');

            switch (parts[0])
            {
                case "PLACE":
                    var arg = parts[1].Split(',');
                    robot.Place(int.Parse(arg[0]), int.Parse(arg[1]), (Direction)Enum.Parse(typeof(Direction), arg[2]));
                    break;
                case "MOVE":
                    robot.Move();
                    break;
                case "LEFT":
                    robot.Left();
                    break;
                case "RIGHT":
                    robot.Right();
                    break;
                case "REPORT":
                    robot.Report();
                    break;
            }
        }
    }
}
