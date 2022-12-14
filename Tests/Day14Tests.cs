namespace Tests;

public class Day14Tests {
	private const string TestInput = """
		498,4 -> 498,6 -> 496,6
		503,4 -> 502,4 -> 502,9 -> 494,9
		""";

	[Theory]
	[InlineData(TestInput, 24)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day14 challenge = new();
		string[] rocks = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(rocks));
	}

	[Theory]
	[InlineData(TestInput, 93)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day14 challenge = new();
		string[] rocks = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(rocks));
	}
}
