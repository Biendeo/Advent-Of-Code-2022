namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 1, Name = "Calorie Counting")]
public class Day01 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] calories) => GetElfCaloriesFromInput(calories).Max();

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] calories) => GetElfCaloriesFromInput(calories).OrderByDescending(x => x).Take(3).Sum();

	private List<int> GetElfCaloriesFromInput(string[] calories) {
		List<int> elfCalories = new();
		int currentElf = 0;

		foreach (string calorieReading in calories) {
			if (string.IsNullOrEmpty(calorieReading)) {
				elfCalories.Add(currentElf);
				currentElf = 0;
			} else {
				currentElf += int.Parse(calorieReading);
			}
		}

		elfCalories.Add(currentElf);
		return elfCalories;
	}
}
