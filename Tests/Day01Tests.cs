namespace Tests;

public class Day01Tests {
	private const string TestInput = """
		1000
		2000
		3000

		4000

		5000
		6000

		7000
		8000
		9000

		10000
		""";

	[Theory]
	[InlineData(TestInput, 24000)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day01 challenge = new();
		string[] calories = input.Trim().Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(calories));
	}

	[Theory]
	[InlineData(TestInput, 45000)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day01 challenge = new();
		string[] calories = input.Trim().Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(calories));
	}
}
