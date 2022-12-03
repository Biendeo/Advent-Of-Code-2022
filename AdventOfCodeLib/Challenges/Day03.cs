namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 3, Name = "Rucksack Reorganization")]
public class Day03 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] contents) => contents.Sum(rucksack => ItemPriority(rucksack.Divide(2).IntersectAll().Single()));

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] contents) => contents.Chunk(3).Select(group => group.IntersectAll().Single()).Sum(c => ItemPriority(c));

	private static int ItemPriority(char item) => item <= 'Z' ? item - 'A' + 27 : item - 'a' + 1;
}
