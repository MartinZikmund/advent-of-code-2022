int result = 0;

while (await Console.In.ReadLineAsync() is string line)
{
    var assignments = line.Split(',');
    var interval1 = ReadInterval(assignments[0]);
    var interval2 = ReadInterval(assignments[1]);
    if (interval1.start <= interval2.end && interval1.end >= interval2.start)
    {
        result++;
    }
}

await Console.Out.WriteLineAsync(result.ToString());

static (int start, int end) ReadInterval(string assignment)
{
    var intervalParts = assignment.Split('-');
    return (int.Parse(intervalParts[0]), int.Parse(intervalParts[1]));
}