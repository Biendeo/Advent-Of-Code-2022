namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 7, Name = "No Space Left On Device")]
public class Day07 : IDayChallenge {
	private class Folder {
		public required string Name { get; init; }
		public List<Folder> Folders = new();
		public List<File> Files = new();
		public int Size => Files.Sum(f => f.Size) + Folders.Sum(f => f.Size);
		public int SizeLimited => (Files.Sum(f => f.Size) + Folders.Sum(f => f.Size)).IfElse(s => s <= 100000, 0);
	}

	private class File {
		public required string Name { get; init; }
		public required int Size { get; init; }
	}

	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] filesystem) {
		(_, List<Folder> allFolders) = ParseFilesystem(filesystem);
		return allFolders.Sum(f => f.SizeLimited);
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] filesystem) {
		(Folder root, List<Folder> allFolders) = ParseFilesystem(filesystem);
		int rootSize = root.Size;
		return allFolders.Select(f => f.Size).Where(s => rootSize - s <= 40000000).Min();
	}

	private (Folder root, List<Folder> allFolders) ParseFilesystem(string[] filesystem) {
		Folder root = new() { Name = "/" };
		List<Folder> allFolders = new() { root };
		Stack<Folder> currentFolderPath = new();
		currentFolderPath.Push(root);
		foreach (string line in filesystem) {
			if (line.StartsWith("$")) {
				if (line[2..4] == "cd") {
					if (line[5..] == "/") {
						currentFolderPath.Clear();
						currentFolderPath.Push(root);
					} else if (line[5..] == "..") {
						currentFolderPath.Pop();
					} else {
						Folder currentFolder = currentFolderPath.Peek();
						currentFolderPath.Push(currentFolder.Folders.Single(f => f.Name == line[5..]));
					}
				}
				// ls is implicit.
			} else {
				Folder currentFolder = currentFolderPath.Peek();
				if (line.StartsWith("dir")) {
					Folder newFolder = new() { Name = line[4..] };
					allFolders.Add(newFolder);
					currentFolder.Folders.Add(newFolder);
				} else {
					string[] splitString = line.Split(' ');
					int size = int.Parse(splitString[0]);
					currentFolder.Files.Add(new() { Name = splitString[1], Size = size });
				}
			}
		}
		return (root, allFolders);
	}
}
