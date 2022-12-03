namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 3, Name = "Rucksack Reorganization")]
public class Day03 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] contents) => contents.Sum(rucksack => {
		string firstHalf = rucksack[..(rucksack.Length / 2)];
		string secondHalf = rucksack[(rucksack.Length / 2)..];
		char commonElement = new HashSet<char>(firstHalf).Intersect(new HashSet<char>(secondHalf)).Single();
		return ItemPriority(commonElement);
	});

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] contents) => contents.Chunk(3).Select(group => new HashSet<char>(group.First()).Intersect(group.Skip(1).Take(1).Single()).Intersect(group.Last()).Single()).Sum(ItemPriority);

	private static int ItemPriority(char item) => item <= 'Z' ? item - 'A' + 27 : item - 'a' + 1;
}
