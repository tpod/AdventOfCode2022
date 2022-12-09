public class Day7
{
    public static int Part2()
    {
        var root = ReadDirectory();

        var totalDiskSpace = 70000000;
        var totalUsedSpace = root.Size;
        var totalAvailableSpace = totalDiskSpace - totalUsedSpace;
        var neededDiskSpace = 30000000;
        
        var minDiskSpaceToDelete = neededDiskSpace - totalAvailableSpace;


        return DirectoriesWithSizesMoreThanDiskSpaceToDelete(root, minDiskSpaceToDelete).MinBy(x => x);
    }

    private static List<int> DirectoriesWithSizesMoreThanDiskSpaceToDelete(Directory directory, int minDiskSpaceToDelete)
    {
        var directories = new List<int>();
        if (directory.Size > minDiskSpaceToDelete)
        {
            directories.Add(directory.Size);
        }

        foreach (var subDirectory in directory.SubDirectories)
        {
            directories.AddRange(DirectoriesWithSizesMoreThanDiskSpaceToDelete(subDirectory!, minDiskSpaceToDelete));
        }

        return directories;
    }
    
    public static int Part1()
    {
        var root = ReadDirectory();

        return DirectorySizesLessThan100000(root!).Sum();
    }

    private static Directory ReadDirectory()
    {
        Directory? currentDirectory = null!;
        foreach (var line in System.IO.File.ReadLines(@$"{Environment.CurrentDirectory}/input7.txt"))
        {
            if (line.StartsWith("$ cd .."))
            {
                currentDirectory = currentDirectory!.Parent;
            }
            else if (line.StartsWith("$ cd /"))
            {
                currentDirectory = new Directory(line.Replace("$ cd ", "").Trim(),
                    null,
                    new List<Directory?>(),
                    new List<File>());
            }
            else if (line.StartsWith("$ cd"))
            {
                currentDirectory = currentDirectory!.SubDirectories.First(x => x?.Name == line.Replace("$ cd ", "").Trim());
            }
            else if (line.StartsWith("$ ls"))
            {
            }
            else if (line.StartsWith("dir"))
            {
                currentDirectory!.SubDirectories.Add(
                    new Directory(line.Replace("dir ", "").Trim(),
                        currentDirectory,
                        new List<Directory?>(),
                        new List<File>()));
            }
            else
            {
                var file = line.Split(" ");
                currentDirectory!.Files.Add(new File(int.Parse(file[0]), file[1]));
            }
        }

        var root = GetRoot(currentDirectory!);
        CalculateSizes(root);
        return root;
    }

    //Find all of the directories with a total size of at most 100000.
    private static List<int> DirectorySizesLessThan100000(Directory directory)
    {
        var directories = new List<int>();
        if (directory.Size <= 100000)
        {
            directories.Add(directory.Size);
        }

        foreach (var subDirectory in directory.SubDirectories)
        {
            directories.AddRange(DirectorySizesLessThan100000(subDirectory!));
        }

        return directories;
    }

    private static Directory GetRoot(Directory currentDirectory)
    {
        return currentDirectory.Parent is null ? currentDirectory : GetRoot(currentDirectory.Parent);
    }
    
    private static void CalculateSizes(Directory directory)
    {
        foreach (var subDirectory in directory.SubDirectories)
        {
            CalculateSizes(subDirectory!);
        }

        directory.Size = directory.Files.Sum(x => x.Size) + directory.SubDirectories.Sum(x => x!.Size);
    }

    public class Directory
    {
        public Directory(string Name, Directory? Parent, List<Directory?> SubDirectories, List<File> Files, int Size = 0)
        {
            this.Name = Name;
            this.Parent = Parent;
            this.SubDirectories = SubDirectories;
            this.Files = Files;
            this.Size = Size;
        }

        public string Name { get; init; }
        public Directory? Parent { get; init; }
        public List<Directory?> SubDirectories { get; init; }
        public List<File> Files { get; init; }
        public int Size { get; set; }
    }

    public record File(int Size, string Name);
}