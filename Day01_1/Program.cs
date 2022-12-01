long best = 0;
long current = 0;
while (await Console.In.ReadLineAsync() is { } line)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        best = Math.Max(best, current);
        current = 0;
    }
    else
    {
        var calories = long.Parse(line);
        current += calories;
    }
}
best = Math.Max(best, current);
Console.WriteLine(best);
