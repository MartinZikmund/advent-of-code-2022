var line = File.ReadAllText("input.txt");

var window = new HashSet<char>();
for (int i = 3; i < line.Length; i++)
{
    window.Clear();
	var isValid = true;
	for (int previousCharacter = 0; previousCharacter < 4; previousCharacter++)
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