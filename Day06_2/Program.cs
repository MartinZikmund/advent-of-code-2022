var line = File.ReadAllText("input.txt");

var windowSize = 14;

var window = new HashSet<char>();
for (int i = windowSize - 1; i < line.Length; i++)
{
    window.Clear();
	var isValid = true;
	for (int previousCharacter = 0; previousCharacter < windowSize; previousCharacter++)
	{
		var character = line[i - previousCharacter];
        if (window.Contains(character))
		{
			isValid = false;
			break;
		}
		else
		{
			window.Add(character);
		}
	}
    if (isValid)
	{
		Console.WriteLine(i + 1);
		break;
	}
}