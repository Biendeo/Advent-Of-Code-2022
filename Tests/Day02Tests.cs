namespace Tests;

public class Day02Tests {
	private const string TestInput = """
		A Y
		B X
		C Z
		""";

	[Theory]
	[InlineData(TestInput, 15)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day02 challenge = new();
		string[] strategyGuide = input.Trim().Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(strategyGuide));
	}

	[Theory]
	[InlineData(TestInput, 12)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day02 challenge = new();
		string[] strategyGuide = input.Trim().Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(strategyGuide));
	}
}
