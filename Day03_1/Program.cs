long totalPriority = 0;

while (await Console.In.ReadLineAsync() is string line)
{
    totalPriority += line
        .Take(line.Length / 2)
        .Intersect(line.TakeLast(line.Length / 2))
        .Sum(c => c <= 'Z' ? (c - 'A' + 27) : (c - 'a' + 1));
}

await Console.Out.WriteLineAsync(totalPriority.ToString());