using Tools;

var movements = File.ReadAllLines("input.txt");

Point tailPosition = new();
Point headPosition = new();

var visited = new HashSet<Point>();
visited.Add(tailPosition);

foreach (var movement in movements)
{
    var movementParts = movement.Split(" ");
    var direction = movementParts[0] switch
    {
        "U" => new Point(0, 1),
        "D" => new Point(0, -1),
        "R" => new Point(1, 0),
        "L" => new Point(-1, 0),
        _ => throw new Exception("Invalid direction"),
    };

    var distance = int.Parse(movementParts[1]);

    for (int i = 0; i < distance; i++)
    {
        headPosition += direction;
        if (Math.Abs(headPosition.X - tailPosition.X) > 1 || 
            Math.Abs(headPosition.Y - tailPosition.Y) > 1)
        {
            if (headPosition.X != tailPosition.X)
            {
                var xDiff = (headPosition.X - tailPosition.X) / Math.Abs(headPosition.X - tailPosition.X);
                tailPosition.X += xDiff;
            }

            if (headPosition.Y != tailPosition.Y)
            {
                var yDiff = (headPosition.Y - tailPosition.Y) / Math.Abs(headPosition.Y - tailPosition.Y);
                tailPosition.Y += yDiff;
            }

            visited.Add(tailPosition);
        }
    }
}

Console.WriteLine(visited.Count);