using var inputStream = File.OpenRead("input.txt");
using var reader = new StreamReader(inputStream);

var monkeys = new List<Monkey>();
while (Monkey.Read(reader) is { } monkey)
{
    monkeys.Add(monkey);
}

for (int i = 0; i < 20; i++)
{
    PerformRound();
}

var mostActive = monkeys.Select(m => m.Inspections).OrderByDescending(i => i).ToArray();

Console.WriteLine(mostActive[0] * mostActive[1]);

void PerformRound()
{
    foreach (var monkey in monkeys)
    {
        while (monkey.Items.Count > 0)
        {
            var item = monkey.Items.Dequeue();
            monkey.Inspections++;
            var inspectedItem = monkey.Operation.Evaluate(item);
            inspectedItem /= 3;
            if (inspectedItem % monkey.DivisibilityCondition == 0)
            {
                monkeys[monkey.TrueTarget].Items.Enqueue(inspectedItem);
            }else
            {
                monkeys[monkey.FalseTarget].Items.Enqueue(inspectedItem);
            }
        }
    }
}

class Monkey
{
    public static Monkey Read(StreamReader reader)
    {
        if (reader.ReadLine() is not { } line)
        {
            return null;
        }

        Monkey monkey = new Monkey();
        monkey.Id = int.Parse(line.Split(' ', ':')[1]);
        var itemsLine = reader.ReadLine();
        var itemsList = itemsLine.Substring(itemsLine.IndexOf(':') + 1);
        var itemsArray = itemsList.Split(',');
        foreach (var item in itemsArray)
        {
            monkey.Items.Enqueue(long.Parse(item.Trim()));
        }
        var operationLine = reader.ReadLine();
        monkey.Operation = Operation.Parse(operationLine);

        var divisibilityTestLine = reader.ReadLine();
        var divisibilityString = divisibilityTestLine
            .Substring(divisibilityTestLine.IndexOf("by") + 2)
            .Trim();
        monkey.DivisibilityCondition = long.Parse(divisibilityString);
        var trueConditionLine = reader.ReadLine();
        monkey.TrueTarget = int.Parse(trueConditionLine.Substring(trueConditionLine.IndexOf("monkey") + 6).Trim());
        var falseConditionLine = reader.ReadLine();
        monkey.FalseTarget = int.Parse(falseConditionLine.Substring(falseConditionLine.IndexOf("monkey") + 6).Trim());
        reader.ReadLine();
        return monkey;
    }

    public int Id { get; set; }

    public Queue<long> Items { get; } = new Queue<long>();

    public Operation Operation { get; set; }

    public int Inspections { get; set; }

    public long DivisibilityCondition { get; set; }

    public int TrueTarget { get; set; }

    public int FalseTarget { get; set; }
}

record Operation(string Operand1, char Operator, string Operand2)
{
    public static Operation Parse(string operationLine)
    {
        var equation = operationLine.Substring(operationLine.IndexOf('=') + 1);
        var equationParts = equation.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return new Operation(equationParts[0], equationParts[1][0], equationParts[2]);
    }

    public long Evaluate(long old)
    {
        var firstValue = old;
        if (Operand1 != "old")
        {
            firstValue = long.Parse(Operand1);
        }

        var secondValue = old;
        if (Operand2 != "old")
        {
            secondValue = long.Parse(Operand2);
        }

        return Operator switch
        {
            '+' => firstValue + secondValue,
            '-' => firstValue - secondValue,
            '*' => firstValue * secondValue,
            '/' => firstValue / secondValue,
            _ => throw new InvalidOperationException()
        };
    }
}