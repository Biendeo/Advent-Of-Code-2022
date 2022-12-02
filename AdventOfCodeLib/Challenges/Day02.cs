namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 2, Name = "Rock Paper Scissors")]
public class Day02 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] strategyGuide) => strategyGuide.Sum(game => {
		Result result;
		int opponent = game[0] - 'A';
		int player = game[2] - 'X';
		if (opponent == player) {
			result = Result.Draw;
		} else if (player - opponent == 1 || player - opponent == -2) {
			result = Result.Win;
		} else {
			result = Result.Loss;
		}
		return (result == Result.Win ? 6 : (result == Result.Draw ? 3 : 0)) + player + 1;
	});

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] strategyGuide) => strategyGuide.Sum(game => {
		int opponent = game[0] - 'A';
		Result result = (Result)(game[2] - 'X');
		return (result == Result.Win ? 6 + ((opponent + 1) % 3) : (result == Result.Draw ? 3 + (opponent % 3) : ((opponent + 2) % 3))) + 1;
	});

	private enum Result {
		Loss,
		Draw,
		Win
	}
}
