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

Dictionary<(int, int, int, long), int> bestPressures = new Dictionary<(int, int, int, long), int>();
Dictionary<(int, int, int, long), int> bestPressuresFromConfiguration = new Dictionary<(int, int, int, long), int>();

long bestSolutionSoFar = 0;

long bestSolution = FindSolution("AA", "AA", 0, 26, 0);

int FindSolution(string ourPosition, string elephantPosition, int totalPressure, int timeLeft, long openValves)
{
    var ourValve = valves[ourPosition];
    var elephantValve = valves[elephantPosition];

    var minId = Math.Min(ourValve.Id, elephantValve.Id);
    var maxId = Math.Max(ourValve.Id, elephantValve.Id);

    if (bestPressures.TryGetValue((minId, maxId, timeLeft, openValves), out var best) &&
        best >= totalPressure)
    {
        return 0;
    }

    bestPressures[(minId, maxId, timeLeft, openValves)] = totalPressure;

    timeLeft--;

    if (bestPressuresFromConfiguration.TryGetValue((minId, maxId, timeLeft, openValves), out var bestPressureFromConfiguration))
    {
        totalPressure += bestPressureFromConfiguration;
        timeLeft = 0;
    }    

    if (timeLeft == 0)
    {
        bestSolutionSoFar = Math.Max(totalPressure, bestSolutionSoFar);
        return totalPressure;
    }

    var weCanOpen = ourValve.Pressure > 0 && (openValves & (1L << ourValve.Id)) == 0;
    var elephantCanOpen = elephantValve.Pressure > 0 && (openValves & (1L << elephantValve.Id)) == 0 && ourPosition != elephantPosition;

    var bestForCurrentConfiguration = 0;

    // Both open
    if (weCanOpen && elephantCanOpen)
    {
        openValves |= 1L << ourValve.Id;
        openValves |= 1L << elephantValve.Id;
        bestForCurrentConfiguration = Math.Max(
            bestForCurrentConfiguration, 
            FindSolution(ourPosition, elephantPosition, totalPressure + ourValve.Pressure * timeLeft + elephantValve.Pressure * timeLeft, timeLeft, openValves));
        openValves &= ~(1L << elephantValve.Id);
        openValves &= ~(1L << ourValve.Id);
    }

    // Only we open
    if (weCanOpen)
    {
        openValves |= 1L << ourValve.Id;
        foreach (var elephantTunnel in elephantValve.Tunnels)
        {
            bestForCurrentConfiguration = Math.Max(
                bestForCurrentConfiguration,
                FindSolution(ourPosition, elephantTunnel, totalPressure + ourValve.Pressure * timeLeft, timeLeft, openValves));
        }
        openValves &= ~(1L << ourValve.Id);
    }

    // Only elephant opens
    if (elephantCanOpen)
    {
        openValves |= 1L << elephantValve.Id;
        foreach (var ourTunnel in ourValve.Tunnels)
        {
            bestForCurrentConfiguration = Math.Max(
                bestForCurrentConfiguration,
                FindSolution(ourTunnel, elephantPosition, totalPressure + elephantValve.Pressure * timeLeft, timeLeft, openValves));
        }
        openValves &= ~(1L << elephantValve.Id);
    }

    // Move
    foreach (var ourTunnel in ourValve.Tunnels)
    {
        foreach (var elephantTunnel in elephantValve.Tunnels)
        {
            bestForCurrentConfiguration = Math.Max(
                bestForCurrentConfiguration, 
                FindSolution(ourTunnel, elephantTunnel, totalPressure, timeLeft, openValves));
        }
    }

    var bestFromHere = bestForCurrentConfiguration - totalPressure;

    if (!bestPressuresFromConfiguration.TryGetValue((minId, maxId, timeLeft, openValves), out bestPressureFromConfiguration) ||
        bestFromHere > bestPressureFromConfiguration)
    {
        bestPressuresFromConfiguration[(minId, maxId, timeLeft, openValves)] = bestFromHere;
    }
    return bestForCurrentConfiguration;
}

Console.WriteLine(bestSolution);

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