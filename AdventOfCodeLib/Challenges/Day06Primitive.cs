namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 6, Name = "Tuning Trouble", Attempt = 2)]
public class Day06Primitive : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines.Single()).ToString();

	public int PartOne(string datastreamBuffer) {
		Span<bool> seen = stackalloc bool[256];
		for (int i = 0; i < datastreamBuffer.Length - 3; ++i) {
			seen.Clear();
			for (int j = i; j < i + 4; ++j) {
				if (seen[datastreamBuffer[j]]) {
					goto next;
				}
				seen[datastreamBuffer[j]] = true;
			}
			return i + 4;
			next:
			;
		}
		return -1;
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines.Single()).ToString();

	public int PartTwo(string datastreamBuffer) {
		Span<bool> seen = stackalloc bool[256];
		for (int i = 0; i < datastreamBuffer.Length - 13; ++i) {
			seen.Clear();
			for (int j = i; j < i + 14; ++j) {
				if (seen[datastreamBuffer[j]]) {
					goto next;
				}
				seen[datastreamBuffer[j]] = true;
			}
			return i + 14;
			next:
			;
		}
		return -1;
	}
}
