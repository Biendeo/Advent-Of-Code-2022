namespace Tests;

public class Day06Tests {
	[Theory]
	[InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
	[InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
	[InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
	[InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
	[InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day06 challenge = new();
		Assert.Equal(expectedResult, challenge.PartOne(input));
	}

	[Theory]
	[InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
	[InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
	[InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
	[InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
	[InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day06 challenge = new();
		Assert.Equal(expectedResult, challenge.PartTwo(input));
	}

	[Theory]
	[InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
	[InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
	[InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
	[InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
	[InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
	public void PartOnePrimitiveSucceeds(string input, int expectedResult) {
		Day06Primitive challenge = new();
		Assert.Equal(expectedResult, challenge.PartOne(input));
	}

	[Theory]
	[InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
	[InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
	[InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
	[InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
	[InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
	public void PartTwoPrimitiveSucceeds(string input, int expectedResult) {
		Day06Primitive challenge = new();
		Assert.Equal(expectedResult, challenge.PartTwo(input));
	}
}
