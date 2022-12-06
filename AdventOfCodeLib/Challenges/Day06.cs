namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 6, Name = "Tuning Trouble", Ignore = true)]
public class Day06 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines.Single()).ToString();

	public int PartOne(string datastreamBuffer) => FindMessage(datastreamBuffer, 4);

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines.Single()).ToString();

	public int PartTwo(string datastreamBuffer) => FindMessage(datastreamBuffer, 14);

	private int FindMessage(string datastreamBuffer, int messageLength) => Enumerable.Range(messageLength, datastreamBuffer.Length - messageLength + 1).First(i => new HashSet<char>(datastreamBuffer.Substring(i - messageLength, messageLength)).Count == messageLength);
}
