using System.Text.RegularExpressions;

namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 15, Name = "Beacon Exclusion Zone", PartTwoSingleThreaded = false, Ignore = true)]
public class Day15 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] sensors, int row = 2000000) {
		List<(int sensorX, int sensorY, int beaconX, int beaconY)> locations = ParseInput(sensors);
		HashSet<int> noBeacons = new();
		HashSet<int> yesBeacons = new();
		foreach ((int sensorX, int sensorY, int beaconX, int beaconY) in locations) {
			int manhattenDistance = Math.Abs(beaconX - sensorX) + Math.Abs(beaconY - sensorY);
			for (int x = sensorX - (manhattenDistance - Math.Abs(row - sensorY)); x <= sensorX + (manhattenDistance - Math.Abs(row - sensorY)); ++x) {
				if (x == beaconX && beaconY == row) {
					yesBeacons.Add(x);
				} else if (!yesBeacons.Contains(x)) {
					noBeacons.Add(x);
				}
			}
		}
		return noBeacons.Count;
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public long PartTwo(string[] sensors, int maxX = 4000000, int maxY = 4000000) {
		List<(int sensorX, int sensorY, int beaconX, int beaconY)> locations = ParseInput(sensors);
		(int x, int y) = locations.AsParallel().SelectMany(location => {
			int manhattenDistanceToSensor = Math.Abs(location.beaconX - location.sensorX) + Math.Abs(location.beaconY - location.sensorY);
			return GetManhattenNeighbours(location.sensorX, location.sensorY, manhattenDistanceToSensor + 1);
		}).Where(c => c.x >= 0 && c.x <= maxX && c.y >= 0 && c.y <= maxY).Distinct().Single(c => {
			return locations.All(location => {
				int manhattenDistanceToPoint = Math.Abs(c.x - location.sensorX) + Math.Abs(c.y - location.sensorY);
				int manhattenDistanceToSensor = Math.Abs(location.beaconX - location.sensorX) + Math.Abs(location.beaconY - location.sensorY);
				return manhattenDistanceToPoint > manhattenDistanceToSensor;
			});
		});
		return x * 4000000L + y;
	}

	private static List<(int sensorX, int sensorY, int beaconX, int beaconY)> ParseInput(string[] sensors) => sensors.Select(s => {
		Match match = Regex.Match(s, @"^Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+).*$");
		return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
	}).ToList();

	private static IEnumerable<(int x, int y)> GetManhattenNeighbours(int x, int y, int distance) {
		for (int i = 0; i < distance; ++i) {
			yield return (x - distance + i, y - i);
			yield return (x + i, y - distance + i);
			yield return (x + distance - i, y + i);
			yield return (x - i, y + distance - i);
		}
	}
}
