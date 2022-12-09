namespace Tests;

public class Day09Tests {
	private const string TestInput1 = """
		R 4
		U 4
		L 3
		D 1
		R 4
		D 1
		L 5
		R 2
		""";

	private const string TestInput2 = """
		R 5
		U 8
		L 8
		D 3
		R 17
		D 10
		L 25
		U 20
		""";

	[Theory]
	[InlineData(TestInput1, 13)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day09 challenge = new();
		string[] motions = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(motions));
	}

	[Theory]
	[InlineData(TestInput2, 36)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day09 challenge = new();
		string[] motions = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(motions));
	}
}
