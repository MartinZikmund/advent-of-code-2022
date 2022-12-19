using Tools;

var lines = File.ReadAllLines("input.txt");
var cubes = new List<Point3d>();
var interior = new HashSet<Point3d>();
var exterior = new HashSet<Point3d>();

int minX = int.MaxValue;
int minY = int.MaxValue;
int minZ = int.MaxValue;
int maxX = int.MinValue;
int maxY = int.MinValue;
int maxZ = int.MinValue;
foreach (var line in lines)
{
    var pointCoordinates = line.Split(',');
    Point3d point = new(
            int.Parse(pointCoordinates[0]),
            int.Parse(pointCoordinates[1]),
            int.Parse(pointCoordinates[2])
        );
    cubes.Add(point);

    minX = Math.Min(minX, point.X);
    minY = Math.Min(minY, point.Y);
    minZ = Math.Min(minZ, point.Z);
    maxX = Math.Max(maxX, point.X);
    maxY = Math.Max(maxY, point.Y);
    maxZ = Math.Max(maxZ, point.Z);
}

minX -= 1;
minY -= 1;
minZ -= 1;
maxX += 1;
maxY += 1;
maxZ += 1;

var surfaceArea = 0;

foreach (var cube in cubes)
{
    var openSides = 6;
    foreach(var direction in Directions.WithoutDiagonals3d)
    {
        var side = cube + direction;
        if (!CanReachBoundary(side))
        {
            openSides--;
        }
    }

    surfaceArea += openSides;
}

bool CanReachBoundary(Point3d side)
{
    var visited = new HashSet<Point3d>();
    var queue = new Queue<Point3d>();
    queue.Enqueue(side);
    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
        if (cubes.Contains(current))
        {
            continue;
        }
        if (current.X <= minX || current.X >= maxX ||
            current.Y <= minY || current.Y >= maxY ||
            current.Z <= minZ || current.Z >= maxZ)
        {
            foreach (var visit in visited)
            {
                exterior.Add(visit);
            }
            return true;
        }
        
        if (exterior.Contains(current))
        {
            return true;
        }

        if(interior.Contains(current))
        {
            return false;
        }

        foreach (var direction in Directions.WithoutDiagonals3d)
        {
            var newPoint = current + direction;
            if (!visited.Contains(newPoint))
            {
                visited.Add(newPoint);
                queue.Enqueue(newPoint);
            }
        }
    }

    foreach (var visit in visited)
    {
        interior.Add(visit);
    }

    return false;
}

Console.WriteLine(surfaceArea);

