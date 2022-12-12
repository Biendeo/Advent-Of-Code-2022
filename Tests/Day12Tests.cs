namespace Tests;

public class Day12Tests {
	private const string TestInput = """
		Sabqponm
		abcryxxl
		accszExk
		acctuvwj
		abdefghi
		""";

	[Theory]
	[InlineData(TestInput, 31)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day12 challenge = new();
		string[] heightmap = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(heightmap));
	}

	[Theory]
	[InlineData(TestInput, 29)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day12 challenge = new();
		string[] heightmap = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(heightmap));
	}
}
