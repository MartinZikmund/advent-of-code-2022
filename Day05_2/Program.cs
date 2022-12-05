using System.Text;

var lines = File.ReadAllLines("input.txt");
var dividerLineIndex = Array.IndexOf(lines, string.Empty);

var numberOfStacks = int.Parse(lines[dividerLineIndex - 1]
    .Trim()
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Last());
var stacks = new Stack<char>[numberOfStacks];

// Read diagram
for (int lineIndex = dividerLineIndex - 2; lineIndex >= 0; lineIndex--)
{
    var line = lines[lineIndex];
    for (int stackId = 0; stackId < numberOfStacks; stackId++)
    {
        var crate = line[stackId * 4 + 1];
        if (char.IsLetter(crate))
        {
            stacks[stackId] ??= new Stack<char>();
            stacks[stackId].Push(crate);
        }
    }
}

// Read instructions
for (int instructionId = dividerLineIndex + 1; instructionId < lines.Length; instructionId++)
{
    var instruction = lines[instructionId];
    var parts = instruction.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var numberOfCrates = int.Parse(parts[1]);
    var sourceStackId = int.Parse(parts[3]) - 1;
    var targetStackId = int.Parse(parts[5]) - 1;

    var transferStack = new Stack<char>();
    for (int i = 0; i < numberOfCrates; i++)
    {
        var crate = stacks[sourceStackId].Pop();
        transferStack.Push(crate);
    }
    
    while(transferStack.TryPop(out var crate))
    {
        stacks[targetStackId].Push(crate);
    }
}

// Print result
Console.WriteLine(string.Join("", stacks.Select(s => s.Peek())));