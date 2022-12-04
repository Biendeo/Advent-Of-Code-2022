namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 4, Name = "Camp Cleanup")]
public class Day04 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] assignments) {
		return assignments.Count(assignment => {
			int[][] sections = assignment.Split(',').Select(elf => elf.Split('-').Select(section => int.Parse(section)).ToArray()).ToArray();
			return (sections[0][0] <= sections[1][0] && sections[0][1] >= sections[1][1]) || (sections[1][0] <= sections[0][0] && sections[1][1] >= sections[0][1]);
		});
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] assignments) {
		return assignments.Count(assignment => {
			int[][] sections = assignment.Split(',').Select(elf => elf.Split('-').Select(section => int.Parse(section)).ToArray()).ToArray();
			return (sections[0][0] <= sections[1][0] && sections[0][1] >= sections[1][0]) || (sections[0][0] <= sections[1][1] && sections[0][1] >= sections[1][1]) || (sections[1][0] <= sections[0][0] && sections[1][1] >= sections[0][0]) || (sections[1][0] <= sections[0][1] && sections[1][1] >= sections[0][1]);
		});
	}
}
