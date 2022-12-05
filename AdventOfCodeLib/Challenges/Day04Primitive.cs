namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 4, Name = "Camp Cleanup", Attempt = 2)]
public class Day04Primitive : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] assignments) {
		int total = 0;
		foreach (string assignment in assignments) {
			(int aLeft, int aRight, int bLeft, int bRight) = ParseAssignment(assignment);
			if ((aLeft <= bLeft && aRight >= bRight) || (bLeft <= aLeft && bRight >= aRight)) {
				++total;
			}
		}
		return total;
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] assignments) {
		int total = 0;
		foreach (string assignment in assignments) {
			(int aLeft, int aRight, int bLeft, int bRight) = ParseAssignment(assignment);
			if ((aLeft <= bLeft && aRight >= bLeft) || (aLeft <= bRight && aRight >= bRight) || (bLeft <= aLeft && bRight >= aLeft) || (bLeft <= aRight && bRight >= aRight)) {
				++total;
			}
		}
		return total;
	}

	private static (int aLeft, int aRight, int bLeft, int bRight) ParseAssignment(string assignment) {
		int aLeft = 0, aRight = 0, bLeft = 0, bRight = 0;
		int workingValue = 0;
		for (int i = 0; i < assignment.Length; ++i) {
			char c = assignment[i];
			switch (c) {
				case ',':
				case '-':
					if (aLeft == 0) {
						aLeft = workingValue;
					} else if (aRight == 0) {
						aRight = workingValue;
					} else if (bLeft == 0) {
						bLeft = workingValue;
					}
					workingValue = 0;
					break;
				default:
					workingValue = workingValue * 10 + (c - '0');
					break;
			}
		}
		bRight = workingValue;
		return (aLeft, aRight, bLeft, bRight);
	}
}
