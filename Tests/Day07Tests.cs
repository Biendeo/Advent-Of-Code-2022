namespace Tests;

public class Day07Tests {
	private const string TestInput = """
		$ cd /
		$ ls
		dir a
		14848514 b.txt
		8504156 c.dat
		dir d
		$ cd a
		$ ls
		dir e
		29116 f
		2557 g
		62596 h.lst
		$ cd e
		$ ls
		584 i
		$ cd ..
		$ cd ..
		$ cd d
		$ ls
		4060174 j
		8033020 d.log
		5626152 d.ext
		7214296 k
		""";

	[Theory]
	[InlineData(TestInput, 95437)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day07 challenge = new();
		string[] filesystem = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(filesystem));
	}

	[Theory]
	[InlineData(TestInput, 24933642)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day07 challenge = new();
		string[] filesystem = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(filesystem));
	}
}
