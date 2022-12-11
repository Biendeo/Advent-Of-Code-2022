namespace Tests;

public class Day11Tests {
	private const string TestInput = """
		Monkey 0:
		  Starting items: 79, 98
		  Operation: new = old * 19
		  Test: divisible by 23
		    If true: throw to monkey 2
		    If false: throw to monkey 3

		Monkey 1:
		  Starting items: 54, 65, 75, 74
		  Operation: new = old + 6
		  Test: divisible by 19
		    If true: throw to monkey 2
		    If false: throw to monkey 0

		Monkey 2:
		  Starting items: 79, 60, 97
		  Operation: new = old * old
		  Test: divisible by 13
		    If true: throw to monkey 1
		    If false: throw to monkey 3

		Monkey 3:
		  Starting items: 74
		  Operation: new = old + 3
		  Test: divisible by 17
		    If true: throw to monkey 0
		    If false: throw to monkey 1
		""";

	[Theory]
	[InlineData(TestInput, 10605L)]
	public void PartOneSucceeds(string input, long expectedResult) {
		Day11 challenge = new();
		string[] monkeyInput = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(monkeyInput));
	}

	[Theory]
	[InlineData(TestInput, 2713310158L)]
	public void PartTwoSucceeds(string input, long expectedResult) {
		Day11 challenge = new();
		string[] monkeyInput = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(monkeyInput));
	}
}
