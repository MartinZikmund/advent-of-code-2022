using Tools;

var lines = File.ReadAllLines("input.txt");
var width = lines[0].Length;
var height = lines.Length;
var map = new char[width, height];

Point endPosition = default;

for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        map[x, y] = lines[y][x];
        if (map[x, y] == 'S')
        {
            map[x, y] = 'a';
        }
        else if (map[x, y] == 'E')
        {
            endPosition = new Point(x, y);
        }
    }
}

map[endPosition.X, endPosition.Y] = 'z';

var best = int.MaxValue;
for (int x = 0; x < width; x++)
{
    for (int y = 0; y < height; y++)
    {
        if (map[x, y] == 'a')
        {
            best = Math.Min(Solve(x, y), best);
        }
    }
}

Console.WriteLine(best);

int Solve(int x, int y)
{
    Point startPosition = (x,y);

    var steps = new int[width, height];

    var queue = new Queue<Point>();
    queue.Enqueue(startPosition);

    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
        var nextSteps = steps[current.X, current.Y] + 1;
        foreach (var direction in Directions.WithoutDiagonals)
        {
            var newPosition = current + direction;
            if (newPosition.X >= 0 &&
                newPosition.Y >= 0 &&
                newPosition.X < width &&
                newPosition.Y < height &&
                steps[newPosition.X, newPosition.Y] == 0 &&
                newPosition != startPosition)
            {
                if (map[newPosition.X, newPosition.Y] <= map[current.X, current.Y] + 1)
                {
                    if (newPosition == endPosition)
                    {
                        return nextSteps;
                    }

                    steps[newPosition.X, newPosition.Y] = nextSteps;
                    queue.Enqueue(newPosition);
                }
            }
        }
    }

    return int.MaxValue;
}