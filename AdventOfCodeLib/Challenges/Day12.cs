namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 12, Name = "Hill Climbing Algorithm", PartTwoSingleThreaded = false)]
public class Day12 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] heightmap) {
		(int x, int y) = Helper.GetTwoDimensionalRange(0, heightmap[0].Length - 1, 0, heightmap.Length - 1).Single(c => heightmap[c.y][c.x] == 'S');
		return StepsToGoal(heightmap, x, y);
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] heightmap) {
		IEnumerable<(int x, int y)> startingTiles = Helper.GetTwoDimensionalRange(0, heightmap[0].Length - 1, 0, heightmap.Length - 1).Where(c => heightmap[c.y][c.x] == 'a');
		return startingTiles.AsParallel().Select(start => StepsToGoal(heightmap, start.x, start.y)).Min();
	}

	private static int StepsToGoal(string[] heightmap, int startX, int startY) {
		HashSet<(int x, int y)> seenTiles = new() {
			(startX, startY)
		};
		Queue<(int x, int y, int stepsSoFar)> tilesToSee = new();
		tilesToSee.Enqueue((startX, startY, 0));
		while (tilesToSee.Any()) {
			(int x, int y, int stepsSoFar) = tilesToSee.Dequeue();
			foreach ((int nextX, int nextY) in Helper.GetNeighbors(x, y, 0, heightmap[0].Length - 1, 0, heightmap.Length - 1, false)) {
				if (heightmap[nextY][nextX] == 'E' && (heightmap[y][x] == 'y' || heightmap[y][x] == 'z')) {
					return stepsSoFar + 1;
				}
				if (!seenTiles.Contains((nextX, nextY)) && (heightmap[nextY][nextX] - heightmap[y][x] <= 1 || (heightmap[y][x] == 'S' && heightmap[nextY][nextX] == 'a'))) {
					tilesToSee.Enqueue((nextX, nextY, stepsSoFar + 1));
					seenTiles.Add((nextX, nextY));
				}
			}
		}
		return int.MaxValue;
	}
}
