using Tools;

var lines = File.ReadAllLines("input.txt");
var cubes = new List<Point3d>();
foreach(var line in lines)
{
    var pointCoordinates = line.Split(',');
    cubes.Add(
        new(
            int.Parse(pointCoordinates[0]),
            int.Parse(pointCoordinates[1]),
            int.Parse(pointCoordinates[2])
        ));
}

var surfaceArea = 0;

foreach(var cube in cubes)
{
    var openSides = 6;
    foreach (var otherCube in cubes)
    {
        if (otherCube == cube)
        {
            continue;
        }

        if (AreAdjacent(cube, otherCube))
        {
            openSides--;
            if (openSides == 0)
            {
                break;
            }
        }
    }

    surfaceArea += openSides;
}

Console.WriteLine(surfaceArea);

static bool AreAdjacent(Point3d a, Point3d b) =>
    (a.X == b.X && a.Y == b.Y && Math.Abs(a.Z - b.Z) == 1) ||
    (a.X == b.X && a.Z == b.Z && Math.Abs(a.Y - b.Y) == 1) ||
    (a.Y == b.Y && a.Z == b.Z && Math.Abs(a.X - b.X) == 1);