using System.Text.RegularExpressions;

namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 16, Name = "Proboscidea Volcanium", Ignore = true)]
public class Day16 : IDayChallenge {
	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] scan) {
		Dictionary<string, (int flowRate, List<string> tunnels)> valves = ParseInput(scan);
		List<string> valveIndexes = valves.Keys.ToList();
		Queue<(string location, UInt128 openValves, int minutes, int score)> queue = new();
		Dictionary<(string location, UInt128 openValves), (int score, int minutes)> seenStates = new();
		queue.Enqueue(("AA", 0, 0, 0));
		int maxMinutes = 0;
		int maxScore = 0;
		while (queue.Count > 0) {
			(string location, UInt128 openValves, int minutes, int score) = queue.Dequeue();
			maxScore = Math.Max(maxScore, score);
			if (minutes == 30) {
				continue;
			}
			++minutes;
			if (minutes > maxMinutes) {
				Console.WriteLine($"Up to minute {minutes}!");
				maxMinutes = minutes;
			}
			UInt128 locationBitmask = new UInt128(0ul, 1ul) << (valveIndexes.IndexOf(location));
			if (valves[location].flowRate > 0 && (openValves & locationBitmask) == 0) {
				bool shouldUpdate = false;
				int newScore = score + valves[location].flowRate * (30 - minutes);
				if (seenStates.TryGetValue((location, openValves | locationBitmask), out (int score, int minutes) oldState)) {
					if (oldState.score < newScore) {
						shouldUpdate = true;
						seenStates.Remove((location, openValves | locationBitmask));
					}
				} else {
					shouldUpdate = true;
				}
				if (shouldUpdate) {
					queue.Enqueue((location, openValves | locationBitmask, minutes, newScore));
					seenStates.Add((location, openValves | locationBitmask), (newScore, minutes));
				}
			}
			foreach (string tunnel in valves[location].tunnels) {
				bool shouldUpdate = false;
				if (seenStates.TryGetValue((tunnel, openValves), out (int score, int minutes) oldState)) {
					if (oldState.score < score) {
						shouldUpdate = true;
						seenStates.Remove((tunnel, openValves));
					}
				} else {
					shouldUpdate = true;
				}
				if (shouldUpdate) {
					queue.Enqueue((tunnel, openValves, minutes, score));
					seenStates.Add((tunnel, openValves), (score, minutes));
				}
			}
		}

		return maxScore;
	}

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] scan) {
		Dictionary<string, (int flowRate, List<string> tunnels)> valves = ParseInput(scan);
		List<string> valveIndexes = valves.Keys.ToList();
		Queue<(string location, string elephantLocation, UInt128 openValves, int minutes, int score)> queue = new();
		Dictionary<(string location, string elephantLocation, UInt128 openValves), (int score, int minutes)> seenStates = new();
		queue.Enqueue(("AA", "AA", 0, 0, 0));
		int maxMinutes = 0;
		int maxScore = 0;
		while (queue.Count > 0) {
			(string location, string elephantLocation, UInt128 openValves, int minutes, int score) = queue.Dequeue();
			maxScore = Math.Max(maxScore, score);
			if (minutes == 26) {
				continue;
			}
			++minutes;
			if (minutes > maxMinutes) {
				Console.WriteLine($"Up to minute {minutes}!");
				maxMinutes = minutes;
			}
			List<(string newLocation, UInt128 openValves)> myMoves = FindConnectingStates(valves, valveIndexes, location, openValves);
			List<(string newLocation, UInt128 openValves)> elephantMoves = FindConnectingStates(valves, valveIndexes, elephantLocation, openValves);
			foreach ((string myNewLocation, UInt128 myOpenValves) in myMoves) {
				foreach ((string elephantNewLocation, UInt128 elephantOpenValves) in elephantMoves) {
					if (location == myNewLocation && location == elephantLocation && location == elephantNewLocation) { // Ignore handling if both me and the elephant open the same valve.
						continue;
					}
					UInt128 newOpenValves = openValves | myOpenValves | elephantOpenValves;
					int newScore = score;
					if (location == myNewLocation) {
						newScore += valves[location].flowRate * (26 - minutes);
					}
					if (elephantLocation == elephantNewLocation) {
						newScore += valves[elephantLocation].flowRate * (26 - minutes);
					}
					bool shouldUpdate = false;
					if (seenStates.TryGetValue((myNewLocation, elephantNewLocation, newOpenValves), out (int score, int minutes) oldState)) {
						if (oldState.score < score) {
							shouldUpdate = true;
							seenStates.Remove((myNewLocation, elephantNewLocation, newOpenValves));
						}
					} else {
						shouldUpdate = true;
					}
					if (shouldUpdate) {
						queue.Enqueue((myNewLocation, elephantNewLocation, newOpenValves, minutes, newScore));
						seenStates.Add((myNewLocation, elephantNewLocation, newOpenValves), (newScore, minutes));
					}
				}
			}
		}

		return maxScore;
	}

	private static List<(string newLocation, UInt128 openValves)> FindConnectingStates(Dictionary<string, (int flowRate, List<string> tunnels)> valves, List<string> valveIndexes, string location, UInt128 openValves) {
		List<(string newLocation, UInt128 openValves)> foundStates = new();
		UInt128 locationBitmask = new UInt128(0ul, 1ul) << (valveIndexes.IndexOf(location));
		if (valves[location].flowRate > 0 && (openValves & locationBitmask) == 0) {
			foundStates.Add((location, openValves | locationBitmask));
		}
		foreach (string tunnel in valves[location].tunnels) {
			foundStates.Add((tunnel, openValves));
		}
		return foundStates;
	}

	private static Dictionary<string, (int flowRate, List<string> tunnels)> ParseInput(string[] scan) => scan.Select(s => {
		Match match = Regex.Match(s, @"^Valve ([A-Z]+) has flow rate=(\d+); tunnels? leads? to valves? (.+)$");
		return new KeyValuePair<string, (int flowRate, List<string> tunnels)>(match.Groups[1].Value, (int.Parse(match.Groups[2].Value), match.Groups[3].Value.Split(", ").ToList()));
	}).ToDictionary(x => x.Key, x => x.Value);
}
