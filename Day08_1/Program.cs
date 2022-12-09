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

for (int x = 0; x < width; x++)
{
    VisitTree(x, 0);
    VisitTree(x, height - 1);
}

for (int y = 0; y < height; y++)
{
    VisitTree(0, y);
    VisitTree(width - 1, y);
}

var visibilityCounter = 0;
for (var x = 0; x < width; x++)
{
    for(var y = 0; y < height; y++)
    {
        if (visible[x, y])
        {
            visibilityCounter++;
        }
    }
}

Console.WriteLine(visibilityCounter.ToString());

void VisitTree(int x, int y)
{
    visible[x, y] = true;
    (int x, int y) direction = (0, 0);
    if (x == 0)
    {
        direction = (1, 0);
    }
    else if (x == width - 1)
    {
        direction = (-1, 0);
    }
    else if (y == 0)
    {
        direction = (0, 1);
    }
    else if (y == height - 1)
    {
        direction = (0, -1);
    }

    var maxHeight = grid[x, y];
    x += direction.x;
    y += direction.y;
    while (x >= 0 && y >= 0 && x < width && y < height)
    {
        if (grid[x, y] > maxHeight)
        {
            visible[x, y] = true;
            maxHeight = grid[x, y];
            if (maxHeight == 9)
            {
                break;
            }
        }

        x += direction.x;
        y += direction.y;
    }
}