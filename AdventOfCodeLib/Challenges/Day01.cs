namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 1, Name = "Calorie Counting")]
public class Day01 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] calories) => GetElfCaloriesFromInput(calories).Max(e => e.Sum());

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] calories) => GetElfCaloriesFromInput(calories).Select(e => e.Sum()).OrderByDescending(x => x).Take(3).Sum();

	private List<List<int>> GetElfCaloriesFromInput(string[] calories) {
		List<List<int>> elfCalories = new();
		List<int> currentElf = new();

		foreach (string calorieReading in calories) {
			if (string.IsNullOrEmpty(calorieReading)) {
				elfCalories.Add(currentElf);
				currentElf = new();
			} else {
				currentElf.Add(int.Parse(calorieReading));
			}
		}

		elfCalories.Add(currentElf);
		return elfCalories;
	}
}
