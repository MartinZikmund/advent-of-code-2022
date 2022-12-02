int totalScore = 0;

while (await Console.In.ReadLineAsync() is string line)
{
    var parts = line.Split(" ");
    var opponentShape = (Shape)(parts[0][0] - 'A');
    var outcome = (RoundOutcome)(parts[1][0] - 'X');
    totalScore += CalculateRoundScore(opponentShape, outcome);
}

await Console.Out.WriteLineAsync(totalScore.ToString());

int CalculateRoundScore(Shape opponent, RoundOutcome outcome)
{
    var roundScore = (int)outcome * 3;
    var shapeScore = 0;
    if (outcome == RoundOutcome.Draw)
    {
        shapeScore = GetShapeScore(opponent);
    }
    else if (outcome == RoundOutcome.Win)
    {
        shapeScore = GetShapeScore((Shape)(((int)opponent + 1) % 3));
    }
    else
    {
        shapeScore = GetShapeScore((Shape)(((int)opponent + 2) % 3));
    }
    return shapeScore + roundScore;
}

int GetShapeScore(Shape shape) => (int)shape + 1;

enum Shape
{
    Rock,
    Paper,
    Scissors
}

enum RoundOutcome
{
    Loss,
    Draw,
    Win
}