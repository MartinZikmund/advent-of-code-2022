long totalPriority = 0;

while (
    await Console.In.ReadLineAsync() is string line &&
    await Console.In.ReadLineAsync() is string line2 &&
    await Console.In.ReadLineAsync() is string line3)
{
    totalPriority += line
        .Intersect(line2)
        .Intersect(line3)
        .Sum(c => c <= 'Z' ? (c - 'A' + 27) : (c - 'a' + 1));
}

await Console.Out.WriteLineAsync(totalPriority.ToString());