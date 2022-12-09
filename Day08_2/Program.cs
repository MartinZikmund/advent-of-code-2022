using Tools;

var lines = File.ReadAllLines("input.txt");
var width = lines[0].Length;
var height = lines.Length;

var grid = new int[width, height];
var visible = new bool[width, height];

for (int x = 0; x < width; x++)
{
    for (int y = 0; y < height; y++)
    {
        grid[x, y] = lines[y][x] - '0';
    }
}

var bestScore = 0;

for (var x = 1; x < width - 1; x++)
{
    for (var y = 1; y < height - 1; y++)
    {
        VisitTree(x, y);
    }
}

Console.WriteLine(bestScore.ToString());

void VisitTree(int x, int y)
{
    var ourHeight = grid[x, y];
    var currentMultiple = 1;
    foreach (var direction in Directions.WithoutDiagonals)
    {
        var viewingDistance = 0;
        var currentX = x + direction.X;
        var currentY = y + direction.Y;
        while (currentX >= 0 && currentY >= 0 && currentX < width && currentY < height)
        {
            viewingDistance++;
            if (grid[currentX, currentY] >= ourHeight)
            {
                break;
            }
            else
            {
                currentX += direction.X;
                currentY += direction.Y;
            }
        }

        currentMultiple *= viewingDistance;
    }

    if (currentMultiple > bestScore)
    {
        bestScore = currentMultiple;
    }
}