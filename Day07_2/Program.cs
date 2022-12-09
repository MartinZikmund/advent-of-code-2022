var cliOutput = System.IO.File.ReadAllLines("input.txt");

Directory rootDirectory = new Directory("/", null);

var currentDirectory = rootDirectory;

foreach (var line in cliOutput)
{
    var lineSplit = line.Split(" ");
    if (line.StartsWith("$"))
    {
        // Command
        if (lineSplit[1] == "cd")
        {
            if (lineSplit[2] == "/")
            {
                currentDirectory = rootDirectory;
            }
            else if (lineSplit[2] == "..")
            {
                currentDirectory = currentDirectory.Parent!;
            }
            else
            {
                var targetName = lineSplit[2];
                if (currentDirectory.Directories.FirstOrDefault(d => d.Name == targetName) is not { } targetDirectory)
                {
                    var newDirectory = new Directory(targetName, currentDirectory);
                    currentDirectory.Directories.Add(newDirectory);
                    targetDirectory = newDirectory;
                }
                currentDirectory = targetDirectory;
            }
        }
    }
    else
    {
        if (lineSplit[0] == "dir")
        {
            // Subdirectory
            if (!currentDirectory.ContainsDirectory(lineSplit[1]))
            {
                var newDirectory = new Directory(lineSplit[1], currentDirectory);
                currentDirectory.Directories.Add(newDirectory);
            }
        }
        else
        {
            if (!currentDirectory.ContainsFile(lineSplit[1]))
            {
                var newFile = new File(lineSplit[1], long.Parse(lineSplit[0]));
                currentDirectory.Files.Add(newFile);
            }
        }
    }
}

long totalDiskSize = 70000000;
long requiredDiskSize = 30000000;

var usedSize = rootDirectory.TotalSize;

var unusedSize = totalDiskSize - usedSize;
var missingSize = requiredDiskSize - unusedSize;

Directory bestDirectory = rootDirectory;

TraverseDirectories(rootDirectory);

Console.WriteLine(bestDirectory.TotalSize);

void TraverseDirectories(Directory source)
{
    if (missingSize <= source.TotalSize &&
         source.TotalSize < bestDirectory.TotalSize)
    {
        bestDirectory = source;
    }
    
    foreach (var directory in source.Directories)
    {
        TraverseDirectories(directory);
    }
}

class Directory
{
    public Directory(string name, Directory? parent)
    {
        Name = name;
        Parent = parent;
    }

    public string Name { get; set; }

    public Directory? Parent { get; set; }

    public List<Directory> Directories { get; } = new List<Directory>();

    public List<File> Files { get; } = new List<File>();

    public bool ContainsDirectory(string directoryName) => Directories.Any(d => d.Name == directoryName);

    public bool ContainsFile(string fileName) => Files.Any(f => f.Name == fileName);

    private long? _totalSize = null;
    
    public long TotalSize
    {
        get
        {
            if (_totalSize is not null)
            {
                return _totalSize.Value;
            }
            
            long size = 0;

            foreach (var file in Files)
            {
                size += file.Size;
            }

            foreach (var directory in Directories)
            {
                size += directory.TotalSize;
            }

            _totalSize = size;
            return _totalSize.Value;
        }
    }
}

class File
{
    public File(string name, long size)
    {
        Name = name;
        Size = size;
    }
    
    public string Name { get; set; }

    public long Size { get; set; }
}