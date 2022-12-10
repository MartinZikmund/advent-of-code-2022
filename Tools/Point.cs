﻿namespace Tools;

public record struct Point(int X, int Y)
{
    public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);

    public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);
}