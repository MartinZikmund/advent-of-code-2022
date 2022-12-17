using var stream = File.OpenRead("input.txt");
using var reader = new StreamReader(stream);

var packets = new List<Packet>();

while (!reader.EndOfStream)
{
    var packet1Line = reader.ReadLine();
    var packet2Line = reader.ReadLine();
    reader.ReadLine();

    var packet1 = Packet.Parse(packet1Line!);
    var packet2 = Packet.Parse(packet2Line!);

    packets.Add(packet1);
    packets.Add(packet2);
}

var divider1 = Packet.Parse("[[2]]");
var divider2 = Packet.Parse("[[6]]");
packets.Add(divider1);
packets.Add(divider2);

packets.Sort();

var divider1Position = packets.IndexOf(divider1) + 1;
var divider2Position = packets.IndexOf(divider2) + 1;

Console.WriteLine(divider1Position * divider2Position);

abstract class Packet : IComparable<Packet>
{
    public static Packet Parse(string input)
    {
        var start = 0;
        return ParseInner(input, ref start);
    }

    private static Packet ParseInner(string input, ref int currentPosition)
    {
        if (char.IsDigit(input[currentPosition]))
        {
            var start = currentPosition;
            while (char.IsDigit(input[currentPosition]))
            {
                currentPosition++;
            }
            var number = int.Parse(input.Substring(start, currentPosition - start));
            return new ValuePacket(number);
        }

        var listPacket = new ListPacket();
        currentPosition++;
        while (input[currentPosition] != ']')
        {
            if (input[currentPosition] == ',')
            {
                currentPosition++;
            }

            var item = ParseInner(input, ref currentPosition);
            listPacket.Packets.Add(item);
        }

        currentPosition++;
        return listPacket;
    }

    public int CompareTo(Packet? other)
    {
        if (other is null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        if (this is ValuePacket a && other is ValuePacket b)
        {
            return a.Value.CompareTo(b.Value);
        }

        var thisAsList = this is ListPacket list ? list : new ListPacket() { Packets = { this } };
        var otherAsList = other is ListPacket otherList ? otherList : new ListPacket() { Packets = { other } };

        var listIndex = 0;

        while (
            listIndex < thisAsList.Packets.Count &&
            listIndex < otherAsList.Packets.Count)
        {
            var firstItem = thisAsList.Packets[listIndex];
            var secondItem = otherAsList.Packets[listIndex];
            var comparison = firstItem.CompareTo(secondItem);
            if (comparison != 0)
            {
                return comparison;
            }

            listIndex++;
        }

        return thisAsList.Packets.Count.CompareTo(otherAsList.Packets.Count);
    }
}

class ListPacket : Packet
{
    public List<Packet> Packets { get; } = new();
}

class ValuePacket : Packet
{
    public ValuePacket(int value)
    {
        Value = value;
    }

    public int Value { get; }
}