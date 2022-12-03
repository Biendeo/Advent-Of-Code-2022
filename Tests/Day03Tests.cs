namespace Tests;

public class Day03Tests {
	private const string TestInput = """
		vJrwpWtwJgWrhcsFMMfFFhFp
		jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
		PmmdzqPrVvPwwTWBwg
		wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
		ttgJtRGJQctTZtZT
		CrZsJsPPZsGzwwsLwLmpwMDw
		""";

	[Theory]
	[InlineData(TestInput, 157)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day03 challenge = new();
		string[] contents = input.Trim().Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(contents));
	}

	[Theory]
	[InlineData(TestInput, 70)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day03 challenge = new();
		string[] contents = input.Trim().Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(contents));
	}
}
