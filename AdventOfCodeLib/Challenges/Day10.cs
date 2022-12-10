using System.Text;

namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 10, Name = "Cathode-Ray Tube")]
public class Day10 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] program) {
		int cycles = 1;
		int register = 1;
		int[] registerAtCycle = new int[300];
		registerAtCycle[1] = register;
		foreach (string instruction in program) {
			if (instruction.StartsWith("noop")) {
				registerAtCycle[++cycles] = register;
			} else {
				int value = int.Parse(instruction[5..]);
				registerAtCycle[++cycles] = register;
				register += value;
				registerAtCycle[++cycles] = register;
			}
			if (cycles >= 220) {
				break;
			}
		}
		return 20 * registerAtCycle[20] + 60 * registerAtCycle[60] + 100 * registerAtCycle[100] + 140 * registerAtCycle[140] + 180 * registerAtCycle[180] + 220 * registerAtCycle[220];
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public string PartTwo(string[] program) {
		int cycles = 1;
		int register = 1;
		StringBuilder sb = new();
		foreach (string instruction in program) {
			if (instruction.StartsWith("noop")) {
				DetectPixel(sb, cycles++, register);
			} else {
				DetectPixel(sb, cycles++, register);
				if (cycles > 240) {
					break;
				}
				DetectPixel(sb, cycles++, register);
				int value = int.Parse(instruction[5..]);
				register += value;
			}
			if (cycles > 240) {
				break;
			}
		}
		return sb.ToString();
	}

	private void DetectPixel(StringBuilder sb, int cycles, int register) {
		int drawnHorizontalPosition = (cycles - 1) % 40;
		if (drawnHorizontalPosition <= register + 1 && drawnHorizontalPosition >= register - 1) {
			sb.Append('#');
		} else {
			sb.Append('.');
		}
		if (cycles % 40 == 0 && cycles != 240) {
			sb.AppendLine();
		}
	}
}
