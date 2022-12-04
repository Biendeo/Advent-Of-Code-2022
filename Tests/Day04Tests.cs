namespace Tests;

public class Day04Tests {
	private const string TestInput = """
		2-4,6-8
		2-3,4-5
		5-7,7-9
		2-8,3-7
		6-6,4-6
		2-6,4-8
		""";

	[Theory]
	[InlineData(TestInput, 2)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day04 challenge = new();
		string[] assignments = input.Trim().Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(assignments));
	}

	[Theory]
	[InlineData(TestInput, 4)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day04 challenge = new();
		string[] contents = input.Trim().Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(contents));
	}
}
