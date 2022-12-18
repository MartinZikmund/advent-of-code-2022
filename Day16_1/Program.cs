using System.Text.RegularExpressions;

var lines = File.ReadAllLines("input.txt");

var regex = new Regex("Valve ([A-Z]*) .*=([0-9]*);.*valve.? (.*)");

var valves = new Dictionary<string, Valve>();

int counter = 0;
foreach (var line in lines)
{
    var match = regex.Match(line);
    var name = match.Groups[1].Value;
    var value = int.Parse(match.Groups[2].Value);
    var connections = match.Groups[3].Value.Split(", ");

    var valve = new Valve(counter++, name, value, connections);
    valves.Add(name, valve);
}

long bestPressure = 0;
Dictionary<(int, int, long), long> bestPressures = new Dictionary<(int, int, long), long>();

FindSolution("AA", 0, 30, 0);

void FindSolution(string position, int totalPressure, int timeLeft, long openValves)
{
    var currentValve = valves[position];
    if (bestPressures.TryGetValue((currentValve.Id, timeLeft, openValves), out var best) &&
        best >= totalPressure)
    {
        return;
    }

    bestPressures[(currentValve.Id, timeLeft, openValves)] = totalPressure;

    timeLeft--;
    if (timeLeft == 0)
    {
        if (totalPressure > bestPressure)
        {
            bestPressure = totalPressure;
        }
        return;
    }

    // Open valve
    if (currentValve.Pressure > 0 && (openValves & (1L << currentValve.Id)) == 0)
    {
        openValves |= 1L << currentValve.Id;
        FindSolution(position, totalPressure + currentValve.Pressure * timeLeft, timeLeft, openValves);
        openValves &= ~(1 << currentValve.Id);
    }

    // Move
    foreach (var tunnel in currentValve.Tunnels)
    {
        FindSolution(tunnel, totalPressure, timeLeft, openValves);
    }
}

Console.WriteLine(bestPressure);

class Valve
{
    public Valve(int id, string name, int pressure, string[] tunnels)
    {
        Id = id;
        Name = name;
        Pressure = pressure;
        Tunnels = tunnels;
    }

    public int Id { get; }

    public string Name { get; }

    public int Pressure { get; }

    public string[] Tunnels { get; }
}