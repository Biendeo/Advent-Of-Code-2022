namespace Tests;

public class Day13Tests {
	private const string TestInput = """
		[1,1,3,1,1]
		[1,1,5,1,1]

		[[1],[2,3,4]]
		[[1],4]

		[9]
		[[8,7,6]]

		[[4,4],4,4]
		[[4,4],4,4,4]

		[7,7,7,7]
		[7,7,7]

		[]
		[3]

		[[[]]]
		[[]]

		[1,[2,[3,[4,[5,6,7]]]],8,9]
		[1,[2,[3,[4,[5,6,0]]]],8,9]
		""";

	[Theory]
	[InlineData(TestInput, 13)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day13 challenge = new();
		string[] packets = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(packets));
	}

	[Theory]
	[InlineData(TestInput, 140)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day13 challenge = new();
		string[] packets = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(packets));
	}
}
