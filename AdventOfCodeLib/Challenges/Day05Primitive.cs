using System.Text;

namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 5, Name = "Supply Stacks", Attempt = 2)]
public class Day05Primitive : IDayChallenge {
	private unsafe struct Stack {
		private fixed char piles[256];
		private int count = 0;
		public Stack() { }
		public void Push(char c) { piles[count++] = c; }
		public char Pop() { return piles[--count]; }
	}

	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public string PartOne(string[] crates) {
		Span<Stack> stacks = stackalloc Stack[(crates[0].Length + 1) / 4];
		int blankLineIndex = 0;
		while (crates[blankLineIndex] != string.Empty) {
			++blankLineIndex;
		}
		for (int i = blankLineIndex - 2; i >= 0; --i) {
			for (int j = 0; j < stacks.Length; ++j) {
				if (crates[i][4 * j + 1] != ' ') {
					stacks[j].Push(crates[i][4 * j + 1]);
				}
			}
		}
		for (int i = blankLineIndex + 1; i < crates.Length; ++i) {
			int amount, sourceStack, destStack;
			if (crates[i].Length == 18) {
				amount = crates[i][5] - '0';
				sourceStack = crates[i][12] - '1';
				destStack = crates[i][17] - '1';
			} else {
				amount = (crates[i][5] - '0') * 10 + crates[i][6] - '0';
				sourceStack = crates[i][13] - '1';
				destStack = crates[i][18] - '1';
			}
			for (int j = 0; j < amount; ++j) {
				stacks[destStack].Push(stacks[sourceStack].Pop());
			}
		}
		StringBuilder sb = new(stacks.Length);
		for (int i = 0; i < stacks.Length; ++i) {
			sb.Append(stacks[i].Pop());
		}
		return sb.ToString();
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public string PartTwo(string[] crates) {
		Span<Stack> stacks = stackalloc Stack[(crates[0].Length + 1) / 4];
		int blankLineIndex = 0;
		while (crates[blankLineIndex] != string.Empty) {
			++blankLineIndex;
		}
		for (int i = blankLineIndex - 2; i >= 0; --i) {
			for (int j = 0; j < stacks.Length; ++j) {
				if (crates[i][4 * j + 1] != ' ') {
					stacks[j].Push(crates[i][4 * j + 1]);
				}
			}
		}
		for (int i = blankLineIndex + 1; i < crates.Length; ++i) {
			int amount, sourceStack, destStack;
			if (crates[i].Length == 18) {
				amount = crates[i][5] - '0';
				sourceStack = crates[i][12] - '1';
				destStack = crates[i][17] - '1';
			} else {
				amount = (crates[i][5] - '0') * 10 + crates[i][6] - '0';
				sourceStack = crates[i][13] - '1';
				destStack = crates[i][18] - '1';
			}
			Stack tempStack = new();
			for (int j = 0; j < amount; ++j) {
				tempStack.Push(stacks[sourceStack].Pop());
			}
			for (int j = 0; j < amount; ++j) {
				stacks[destStack].Push(tempStack.Pop());
			}
		}
		StringBuilder sb = new(stacks.Length);
		for (int i = 0; i < stacks.Length; ++i) {
			sb.Append(stacks[i].Pop());
		}
		return sb.ToString();
	}
}
