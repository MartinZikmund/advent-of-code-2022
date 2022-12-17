using Tools;

var lines = File.ReadAllLines("input.txt");

const int MaxSize = 1000;
var cave = new char[MaxSize, MaxSize];

foreach (var line in lines)
{
    var parts = line.Split(" -> ");

    static Point ParsePoint(string pointString)
    {
        var pointParts = pointString.Split(',');
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

var sandCount = 0;
while (DropSand())
{
    sandCount++;
}

OutputCave(490, 0, 504, 10);
Console.Clear();

Console.WriteLine(sandCount);

bool DropSand()
{
    var currentPosition = new Point(500, 0);
    while (true)
    {
        if (currentPosition.Y == MaxSize - 1)
        {
            return false;
        }

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
            return true;
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
