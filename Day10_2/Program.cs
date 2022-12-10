using System.Text;

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

StringBuilder outputBuilder = new();

var tick = 0;
foreach (var instruction in instructions)
{
    ProcessCycle();
        
    if (instruction != "noop")
    {
        ProcessCycle();
        var addSplit = instruction.Split(' ');
        int value = int.Parse(addSplit[1]);
        x += value;
    }
}

Console.WriteLine(outputBuilder.ToString());

void ProcessCycle()
{
    var spritePosition = x;
    if (spritePosition - 1 <= tick && tick <= spritePosition + 1)
    {
        outputBuilder.Append("#");
    }
    else
    {
        outputBuilder.Append(".");
    }

    tick++;
    if (tick % 40 == 0)
    {
        outputBuilder.AppendLine();
        tick = 0;
    }
}