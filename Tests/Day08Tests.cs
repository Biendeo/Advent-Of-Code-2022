namespace Tests;

public class Day08Tests {
	private const string TestInput = """
		30373
		25512
		65332
		33549
		35390
		""";

	[Theory]
	[InlineData(TestInput, 21)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day08 challenge = new();
		string[] heightmap = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(heightmap));
	}

	[Theory]
	[InlineData(TestInput, 8)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day08 challenge = new();
		string[] heightmap = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(heightmap));
	}
}
