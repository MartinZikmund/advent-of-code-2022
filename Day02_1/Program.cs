long totalScore = 0;

while (await Console.In.ReadLineAsync() is string line)
{
    var parts = line.Split(" ");
    var opponentShape = (Shape)(parts[0][0] - 'A');
    var ourShape = (Shape)(parts[1][0] - 'X');
    totalScore += CalculateRoundScore(opponentShape, ourShape);
}

await Console.Out.WriteLineAsync(totalScore.ToString());

long CalculateRoundScore(Shape opponent, Shape we)
{
    var shapeScore = GetShapeScore(we);
    var roundScore = 3;
    if (opponent != we)
    {
        int ourShapeIndex = (int)we;
        int opponentShapeIndex = (int)opponent;
        roundScore = ourShapeIndex == (opponentShapeIndex + 1) % 3 ? 6 : 0;
    }

    return shapeScore + roundScore;
}

long GetShapeScore(Shape shape) => (int)shape + 1;

enum Shape
{
    Rock,
    Paper,
    Scissors
}