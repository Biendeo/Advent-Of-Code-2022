namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 14, Name = "Regolith Reservoir")]
public class Day14 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] rocks) {
		HashSet<(int x, int y)> plottedRocks = PlotRocks(rocks);
		int voidY = plottedRocks.Select(rock => rock.y).Max() + 1;
		bool foundVoid = false;
		int droppedSand = 0;
		while (!foundVoid) {
			int sandX = 500, sandY = 0;
			bool sandStuck = false;
			while (!sandStuck) {
				sandStuck = true;
				foreach (int newX in new[] { sandX, sandX - 1, sandX + 1 }) {
					if (!plottedRocks.Contains((newX, sandY + 1))) {
						sandStuck = false;
						sandX = newX;
						++sandY;
						break;
					}
				}
				if (sandY == voidY) {
					goto done;
				}
			}
			plottedRocks.Add((sandX, sandY));
			++droppedSand;
		}
		done:
		return droppedSand;
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] rocks) {
		HashSet<(int x, int y)> plottedRocks = PlotRocks(rocks);
		int floor = plottedRocks.Select(rock => rock.y).Max() + 2;
		int droppedSand = 0;
		while (!plottedRocks.Contains((500, 0))) {
			int sandX = 500, sandY = 0;
			bool sandStuck = false;
			while (!sandStuck) {
				sandStuck = true;
				if (sandY == floor - 1) {
					break;
				}
				foreach (int newX in new[] { sandX, sandX - 1, sandX + 1 }) {
					if (!plottedRocks.Contains((newX, sandY + 1))) {
						sandStuck = false;
						sandX = newX;
						++sandY;
						break;
					}
				}
			}
			plottedRocks.Add((sandX, sandY));
			++droppedSand;
		}
		return droppedSand;
	}

	private static HashSet<(int x, int y)> PlotRocks(string[] rocks) {
		HashSet<(int x, int y)> plottedRocks = new();

		foreach (string rock in rocks) {
			string[] splitRockInput = rock.Split(" -> ");
			for (int i = 0; i < splitRockInput.Length - 1; ++i) {
				int x1 = int.Parse(splitRockInput[i].Split(',')[0]);
				int y1 = int.Parse(splitRockInput[i].Split(',')[1]);
				int x2 = int.Parse(splitRockInput[i + 1].Split(',')[0]);
				int y2 = int.Parse(splitRockInput[i + 1].Split(',')[1]);

				IEnumerable<(int x, int y)> intermediateCoords;
				if (x2 < x1) {
					intermediateCoords = Helper.GetTwoDimensionalRange(x2, x1, y1, y2);
				} else if (y2 < y1) {
					intermediateCoords = Helper.GetTwoDimensionalRange(x1, x2, y2, y1);
				} else {
					intermediateCoords = Helper.GetTwoDimensionalRange(x1, x2, y1, y2);
				}
				foreach ((int x, int y) in intermediateCoords) {
					plottedRocks.Add((x, y));
				}
			}
		}

		return plottedRocks;
	}
}
