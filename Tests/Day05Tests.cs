namespace Tests;

public class Day05Tests {
	private const string TestInput = """
		    [D]    
		[N] [C]    
		[Z] [M] [P]
		 1   2   3 

		move 1 from 2 to 1
		move 3 from 1 to 3
		move 2 from 2 to 1
		move 1 from 1 to 2
		""";

	[Theory]
	[InlineData(TestInput, "CMZ")]
	public void PartOneSucceeds(string input, string expectedResult) {
		Day05 challenge = new();
		string[] crates = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(crates));
	}

	[Theory]
	[InlineData(TestInput, "MCD")]
	public void PartTwoSucceeds(string input, string expectedResult) {
		Day05 challenge = new();
		string[] crates = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(crates));
	}

	[Theory]
	[InlineData(TestInput, "CMZ")]
	public void PartOnePrimitiveSucceeds(string input, string expectedResult) {
		Day05Primitive challenge = new();
		string[] crates = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(crates));
	}

	[Theory]
	[InlineData(TestInput, "MCD")]
	public void PartTwoPrimitiveSucceeds(string input, string expectedResult) {
		Day05Primitive challenge = new();
		string[] crates = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(crates));
	}
}
