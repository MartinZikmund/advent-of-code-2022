namespace Tools;

public static class Directions
{
    public static Point[] WithoutDiagonals { get; } = new Point[]
    {
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
    };

    public static Point[] WithDiagonals { get; } = new Point[]
    {
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
        (1, 1),
        (-1, 1),
        (1, -1),
        (-1, -1)
    };

    public static Point3d[] WithoutDiagonals3d { get; } = new Point3d[]
    {
        (1, 0, 0),
        (0, 1, 0),
        (0, 0, 1),
        (-1, 0, 0),
        (0, -1, 0),
        (0, 0, -1),
    };
}
