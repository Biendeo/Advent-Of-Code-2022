namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 2, Name = "Rock Paper Scissors")]
public class Day02 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] strategyGuide) => strategyGuide.Sum(game => {
		return ((game[2] - game[0] + 1) % 3) switch {
			0 => 3,
			1 => 6,
			_ => 0
		} + game[2] - 'W';
	});

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] strategyGuide) => strategyGuide.Sum(game => {
		return game[2] switch {
			'Z' => ((game[0] + 2) % 3) + 6,
			'Y' => ((game[0] + 1) % 3) + 3,
			_   => game[0] % 3,
		} + 1;
	});
}
