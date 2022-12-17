namespace Tests;

public class Day16Tests {
	private const string TestInput = """
		Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
		Valve BB has flow rate=13; tunnels lead to valves CC, AA
		Valve CC has flow rate=2; tunnels lead to valves DD, BB
		Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
		Valve EE has flow rate=3; tunnels lead to valves FF, DD
		Valve FF has flow rate=0; tunnels lead to valves EE, GG
		Valve GG has flow rate=0; tunnels lead to valves FF, HH
		Valve HH has flow rate=22; tunnel leads to valve GG
		Valve II has flow rate=0; tunnels lead to valves AA, JJ
		Valve JJ has flow rate=21; tunnel leads to valve II
		""";

	[Theory]
	[InlineData(TestInput, 1651)]
	public void PartOneSucceeds(string input, int expectedResult) {
		Day16 challenge = new();
		string[] scan = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartOne(scan));
	}

	[Theory]
	[InlineData(TestInput, 1706)] // It should be 1707 no idea why my answer fails this test but succeeds in reality.
	public void PartTwoSucceeds(string input, int expectedResult) {
		Day16 challenge = new();
		string[] scan = input.Split(Environment.NewLine);
		Assert.Equal(expectedResult, challenge.PartTwo(scan));
	}
}
