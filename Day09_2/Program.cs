using Tools;

var movements = File.ReadAllLines("input.txt");

Point[] rope = new Point[10];

var visited = new HashSet<Point>();
visited.Add(rope[9]);

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
        rope[0] += direction;

        for (int knot = 1; knot < 10; knot++)
        {
            var headPosition = rope[knot - 1];
            var currentPosition = rope[knot];
            if (Math.Abs(headPosition.X - currentPosition.X) > 1 ||
                Math.Abs(headPosition.Y - currentPosition.Y) > 1)
            {
                if (headPosition.X != currentPosition.X)
                {
                    var xDiff = (headPosition.X - currentPosition.X) / Math.Abs(headPosition.X - currentPosition.X);
                    currentPosition.X += xDiff;
                }

                if (headPosition.Y != currentPosition.Y)
                {
                    var yDiff = (headPosition.Y - currentPosition.Y) / Math.Abs(headPosition.Y - currentPosition.Y);
                    currentPosition.Y += yDiff;
                }

                rope[knot] = currentPosition;

                if (knot == 9)
                {
                    visited.Add(currentPosition);
                }
            }
        }
    }
}

Console.WriteLine(visited.Count);