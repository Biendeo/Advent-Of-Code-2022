namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 4, Name = "Camp Cleanup")]
public class Day04 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] assignments) {
		return assignments.Count(assignment => {
			int[][] sections = ParseAssignment(assignment);
			return Enumerable.Range(0, 2).Any(x => sections[x][0] <= sections[1 - x][0] && sections[x][1] >= sections[1 - x][1]);
		});
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] assignments) {
		return assignments.Count(assignment => {
			int[][] sections = ParseAssignment(assignment);
			return Enumerable.Range(0, 2).Any(x => Enumerable.Range(0, 2).Any(y => (sections[x][0] <= sections[1 - x][y] && sections[x][1] >= sections[1 - x][y])));
		});
	}

	private static int[][] ParseAssignment(string assignment) => assignment.Split(',').Select(elf => elf.Split('-').Select(section => int.Parse(section)).ToArray()).ToArray();
}
