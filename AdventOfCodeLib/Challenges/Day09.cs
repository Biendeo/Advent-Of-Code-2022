namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 9, Name = "Rope Bridge")]
public class Day09 : IDayChallenge {
	private delegate void ActDirection(ref int headX, ref int headY);

	private static Dictionary<char, ActDirection> directionActions = new() {
		{ 'U', (ref int headX, ref int headY) => ++headY },
		{ 'R', (ref int headX, ref int headY) => ++headX },
		{ 'D', (ref int headX, ref int headY) => --headY },
		{ 'L', (ref int headX, ref int headY) => --headX }
	};

	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] motions) => Solve(motions, 2);

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] motions) => Solve(motions, 10);

	private int Solve(string[] motions, int numKnots) {
		(int x, int y)[] ropePoints = new (int x, int y)[numKnots];
		for (int i = 0; i < numKnots; ++i) {
			ropePoints[i] = (0, 0);
		}
		HashSet<(int x, int y)> seenTailLocations = new() {
			(0, 0)
		};
		foreach (string motion in motions) {
			for (int i = 0; i < int.Parse(motion[2..]); ++i) {
				directionActions[motion[0]](ref ropePoints[0].x, ref ropePoints[0].y);
				for (int j = 0; j < numKnots - 1; ++j) {
					MoveRopePoint(ref ropePoints[j].x, ref ropePoints[j].y, ref ropePoints[j + 1].x, ref ropePoints[j + 1].y);
				}
				seenTailLocations.Add(ropePoints.Last());
			}
		}
		return seenTailLocations.Count;
	}

	private void MoveRopePoint(ref int headX, ref int headY, ref int tailX, ref int tailY) {
		if (Math.Abs(headX - tailX) >= 2 || Math.Abs(headY - tailY) >= 2) {
			if (headX == tailX && headY > tailY) {
				++tailY;
			} else if (headX > tailX && headY == tailY) {
				++tailX;
			} else if (headX == tailX && headY < tailY) {
				--tailY;
			} else if (headX < tailX && headY == tailY) {
				--tailX;
			} else if (headX > tailX && headY > tailY) {
				++tailX;
				++tailY;
			} else if (headX > tailX && headY < tailY) {
				++tailX;
				--tailY;
			} else if (headX < tailX && headY > tailY) {
				--tailX;
				++tailY;
			} else if (headX < tailX && headY < tailY) {
				--tailX;
				--tailY;
			}
		}
	}
}
