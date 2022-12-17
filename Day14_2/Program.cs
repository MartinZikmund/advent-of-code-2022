using Tools;

var lines = File.ReadAllLines("input.txt");

const int MaxSize = 1000;
var cave = new char[MaxSize, MaxSize];

var maxY = 0;

foreach (var line in lines)
{
    var parts = line.Split(" -> ");

    Point ParsePoint(string pointString)
    {
        var pointParts = pointString.Split(',');
        var x = int.Parse(pointParts[0]);
        var y = int.Parse(pointParts[1]);
        maxY = Math.Max(y, maxY);
        return new Point(int.Parse(pointParts[0]), int.Parse(pointParts[1]));
    }

    Point previousPoint = ParsePoint(parts[0]);
    for (int i = 1; i < parts.Length; i++)
    {
        var targetPoint = ParsePoint(parts[i]);
        var direction = (targetPoint - previousPoint).Normalize();

        cave[previousPoint.X, previousPoint.Y] = '#';
        while (previousPoint != targetPoint)
        {
            previousPoint += direction;
            cave[previousPoint.X, previousPoint.Y] = '#';
        }
    }
}

for (int x = 0; x < MaxSize; x++)
{
    cave[x, maxY + 2] = '#';
}

var sandCount = 0;
while (cave[500, 0] == 0)
{
    DropSand();
    sandCount++;
}

OutputCave(490, 0, 520, 13);

Console.WriteLine(sandCount);

void DropSand()
{
    var currentPosition = new Point(500, 0);
    while (true)
    {
        if (cave[currentPosition.X, currentPosition.Y + 1] == 0)
        {
            currentPosition = new Point(currentPosition.X, currentPosition.Y + 1);
        }
        else if (cave[currentPosition.X - 1, currentPosition.Y + 1] == 0)
        {
            currentPosition = new Point(currentPosition.X - 1, currentPosition.Y + 1);
        }
        else if (cave[currentPosition.X + 1, currentPosition.Y + 1] == 0)
        {
            currentPosition = new Point(currentPosition.X + 1, currentPosition.Y + 1);
        }
        else
        {
            // Rest
            cave[currentPosition.X, currentPosition.Y] = 'o';
            break;
        }
    }
}

void OutputCave(int fromX, int fromY, int toX, int toY)
{
    for (int y = fromY; y < toY; y++)
    {
        for (int x = fromX; x < toX; x++)
        {
            Console.Write(cave![x, y] != 0 ? cave[x, y] : '.');
        }
        Console.WriteLine();
    }
}
