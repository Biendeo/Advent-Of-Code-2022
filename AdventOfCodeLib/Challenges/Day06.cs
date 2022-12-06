namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 6, Name = "Tuning Trouble")]
public class Day06 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(string.Join(string.Empty, inputLines)).ToString();

	public int PartOne(string datastreamBuffer) {
		for (int i = 0; i < datastreamBuffer.Length - 3; ++i) {
			if (new HashSet<char>(datastreamBuffer.Substring(i, 4)).Count == 4) {
				return i + 4;
			}
		}
		return -1;
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(string.Join(string.Empty, inputLines)).ToString();

	public int PartTwo(string datastreamBuffer) {
		for (int i = 0; i < datastreamBuffer.Length - 13; ++i) {
			if (new HashSet<char>(datastreamBuffer.Substring(i, 14)).Count == 14) {
				return i + 14;
			}
		}
		return -1;
	}
}
