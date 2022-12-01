List<long> elves = new();
long current = 0;
while (await Console.In.ReadLineAsync() is { } line)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        elves.Add(current);
        current = 0;
    }
    else
    {
        var calories = long.Parse(line);
        current += calories;
    }
}
elves.Add(current);

var result = elves.OrderByDescending(i => i).Take(3).Sum();

Console.WriteLine(result);
