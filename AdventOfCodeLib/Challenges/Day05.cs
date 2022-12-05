namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 5, Name = "Supply Stacks")]
public class Day05 : IDayChallenge {
	private delegate void StackOperation(List<Stack<char>> stacks, int numCrates, int sourceStack, int destStack);

	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public string PartOne(string[] crates) => FullOperation(crates, (stacks, numCrates, sourceStack, destStack) => {
		for (int i = 0; i < numCrates; ++i) {
			stacks[destStack].Push(stacks[sourceStack].Pop());
		}
	});

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public string PartTwo(string[] crates) => FullOperation(crates, (stacks, numCrates, sourceStack, destStack) => {
		Stack<char> tempStack = new();
		for (int i = 0; i < numCrates; ++i) {
			tempStack.Push(stacks[sourceStack].Pop());
		}
		while (tempStack.Any()) {
			stacks[destStack].Push(tempStack.Pop());
		}
	});

	private static string FullOperation(string[] crates, StackOperation action) {
		List<Stack<char>> stacks = ParseStacks(crates);
		OperateStacks(crates, stacks, action);
		return string.Join(string.Empty, stacks.Select(s => s.First()));
	}

	private static Index InputDivider(string[] crates) => crates.ToList().IndexOf(string.Empty);

	private static List<Stack<char>> ParseStacks(string[] crates) {
		Index inputDivider = InputDivider(crates);
		List<Stack<char>> stacks = new();
		for (int i = 0; i < crates[inputDivider.Value - 1].Max() - '0'; ++i) {
			stacks.Add(new());
		}
		Enumerable.Repeat(new Stack<char>(), crates[inputDivider.Value - 1].Max() - '0').ToList();
		foreach (string s in crates[0..(inputDivider.Value - 1)].Reverse()) {
			for (int i = 0; i < stacks.Count; ++i) {
				char c = s[4 * i + 1];
				if (c != ' ') {
					stacks[i].Push(c);
				}
			}
		}
		return stacks;
	}

	private static void OperateStacks(string[] crates, List<Stack<char>> stacks, StackOperation action) {
		foreach (string s in crates[(InputDivider(crates).Value + 1)..]) {
			string[] splitString = s.Split(' ');
			int numCrates = int.Parse(splitString[1]);
			int sourceStack = int.Parse(splitString[3]) - 1;
			int destStack = int.Parse(splitString[5]) - 1;
			action(stacks, numCrates, sourceStack, destStack);
		}
	}
}
