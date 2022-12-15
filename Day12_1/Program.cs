using Tools;

var lines = File.ReadAllLines("input.txt");
var width = lines[0].Length;
var height = lines.Length;
var map = new char[width, height];
var steps = new int[width, height];
Point startPosition = default;
Point endPosition = default;

for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        map[x, y] = lines[y][x];
        if (map[x, y] == 'S')
        {
            startPosition = new Point(x, y);
        }
        else if (map[x, y] == 'E')
        {
            endPosition = new Point(x, y);
        }
    }
}

map[startPosition.X, startPosition.Y] = 'z';
map[endPosition.X, endPosition.Y] = 'z';
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
                    Console.WriteLine(nextSteps);
                    return;
                }

                steps[newPosition.X, newPosition.Y] = nextSteps;
                queue.Enqueue(newPosition);
            }
        }
    }
}