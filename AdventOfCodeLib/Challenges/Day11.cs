using System.Numerics;
using System.Text;

namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 11, Name = "Monkey in the Middle")]
public class Day11 : IDayChallenge {
	private enum Operator {
		Add,
		Multiply
	}

	private const int OPERATOR_VALUE_OLD = 0;

	private class Monkey {
		public required int MonkeyIndex { get; init; }
		public required List<long> Items { get; init; }
		public required Operator Operator { get; init; }
		public required int OperationValue { get; init; }
		public required int TestDivisible { get; init; }
		public required int TrueDestination { get; init; }
		public required int FalseDestination { get; init; }
		public long ItemsChecked { get; set; } = 0L;
	}

	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public long PartOne(string[] monkeyInput) => RunRounds(ParseInput(monkeyInput), 20, true);

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public long PartTwo(string[] monkeyInput) => RunRounds(ParseInput(monkeyInput), 10000, false);

	private static List<Monkey> ParseInput(string[] monkeyInput) {
		List<Monkey> monkeys = new();

		for (int monkeyIndex = 0; monkeyIndex < (monkeyInput.Length + 1) / 7; ++monkeyIndex) {
			monkeys.Add(new() {
				MonkeyIndex = monkeyIndex,
				Items = monkeyInput[monkeyIndex * 7 + 1][18..].Split(", ").Select(x => long.Parse(x)).ToList(),
				Operator = monkeyInput[monkeyIndex * 7 + 2].Contains('*') ? Operator.Multiply : Operator.Add,
				OperationValue = monkeyInput[monkeyIndex * 7 + 2][25..] == "old" ? OPERATOR_VALUE_OLD : int.Parse(monkeyInput[monkeyIndex * 7 + 2][25..]),
				TestDivisible = int.Parse(monkeyInput[monkeyIndex * 7 + 3][21..]),
				TrueDestination = int.Parse(monkeyInput[monkeyIndex * 7 + 4][29..]),
				FalseDestination = int.Parse(monkeyInput[monkeyIndex * 7 + 5][30..])
			});
		}

		return monkeys;
	}

	private static long RunRounds(List<Monkey> monkeys, int rounds, bool divideByThree) {
		long lcm = monkeys.Select(m => m.TestDivisible).Aggregate((a, x) => a * x);
		for (int i = 0; i < rounds; ++i) {
			foreach (Monkey monkey in monkeys) {
				while (monkey.Items.Any()) {
					long currentItem = monkey.Items[0];
					monkey.Items.RemoveAt(0);
					if (monkey.Operator == Operator.Add) {
						currentItem += monkey.OperationValue == OPERATOR_VALUE_OLD ? currentItem : monkey.OperationValue;
					} else {
						currentItem *= monkey.OperationValue == OPERATOR_VALUE_OLD ? currentItem : monkey.OperationValue;
					}
					if (divideByThree) {
						currentItem /= 3;
					}
					currentItem %= lcm;
					if (currentItem % monkey.TestDivisible == 0) {
						monkeys[monkey.TrueDestination].Items.Add(currentItem);
					} else {
						monkeys[monkey.FalseDestination].Items.Add(currentItem);
					}
					++monkey.ItemsChecked;
				}
			}
		}

		return monkeys.Select(m => m.ItemsChecked).OrderByDescending(x => x).Take(2).Aggregate((a, x) => a * x);
	}
}
