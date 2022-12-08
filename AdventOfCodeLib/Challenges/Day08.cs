namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 8, Name = "Treetop Tree House")]
public class Day08 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] heightmap) => Helper.GetTwoDimensionalRange(0, heightmap[0].Length - 1, 0, heightmap.Length - 1).Count(c => {
		bool blockedLeft = false, blockedUp = false, blockedRight = false, blockedDown = false;

		for (int i = c.x - 1; !blockedLeft && i >= 0; --i) {
			blockedLeft |= heightmap[c.x][c.y] <= heightmap[i][c.y];
		}
		for (int j = c.y - 1; !blockedUp && j >= 0; --j) {
			blockedUp |= heightmap[c.x][c.y] <= heightmap[c.x][j];
		}
		for (int i = c.x + 1; !blockedRight && i < heightmap[0].Length; ++i) {
			blockedRight |= heightmap[c.x][c.y] <= heightmap[i][c.y];
		}
		for (int j = c.y + 1; !blockedDown && j < heightmap.Length; ++j) {
			blockedDown |= heightmap[c.x][c.y] <= heightmap[c.x][j];
		}

		return !(blockedLeft && blockedUp && blockedRight && blockedDown);
	});

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] heightmap) => Helper.GetTwoDimensionalRange(1, heightmap[0].Length - 2, 1, heightmap.Length - 2).Select(c => {
		int left = c.x - 1, up = c.y - 1, right = c.x + 1, down = c.y + 1;

		while (heightmap[c.x][c.y] > heightmap[left][c.y] && left > 0) {
			--left;
		}
		while (heightmap[c.x][c.y] > heightmap[c.x][up] && up > 0) {
			--up;
		}
		while (heightmap[c.x][c.y] > heightmap[right][c.y] && right < heightmap[0].Length - 1) {
			++right;
		}
		while (heightmap[c.x][c.y] > heightmap[c.x][down] && down < heightmap.Length - 1) {
			++down;
		}

		return (c.x - left) * (c.y - up) * (right - c.x) * (down - c.y);
	}).Max();
}
