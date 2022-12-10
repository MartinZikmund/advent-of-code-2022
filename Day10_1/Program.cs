var instructions = File.ReadAllLines("input.txt");

long sumOfSignals = 0;
long x = 1;

var breakpoints = new int[]
{
    20,
    60,
    100,
    140,
    180,
    220
};
var nextBreakpointId = 0;

var tick = 0;
foreach (var instruction in instructions)
{
    if (breakpoints[nextBreakpointId] == tick ||
        (instruction != "noop" && breakpoints[nextBreakpointId] <= tick + 2))
    {
        var signalStrength = x * breakpoints[nextBreakpointId];
        sumOfSignals += signalStrength;
        nextBreakpointId++;
        if (breakpoints.Length == nextBreakpointId)
        {
            break;
        }
    }
    
    if (instruction == "noop")
    {
        tick++;
    }
    else
    {
        var addSplit = instruction.Split(' ');
        int value = int.Parse(addSplit[1]);
        x += value;
        tick += 2;
    }
}

Console.WriteLine(sumOfSignals.ToString());