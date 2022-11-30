namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 1, Name = "Sonar Sweep")]
public class Day01 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines.Select(x => int.Parse(x)).ToList()).ToString();

	public int PartOne(List<int> measurements) => CalculateIncreases(measurements, 1);

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines.Select(x => int.Parse(x)).ToList()).ToString();

	public int PartTwo(List<int> measurements) => CalculateIncreases(measurements, 3);

	private int CalculateIncreases(List<int> measurements, int windowSize) => measurements.Zip(measurements.Skip(windowSize), (x, y) => x < y).Count(b => b);
}
