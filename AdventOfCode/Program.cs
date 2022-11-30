using AdventOfCodeLib;
using System.Diagnostics;

static ChallengeResult RunSolution(IDayChallenge dayChallenge, int part, string[] inputLines) {
	Stopwatch stopwatch = new();
	stopwatch.Start();
	string result = part == 1 ? dayChallenge.PartOneFromInput(inputLines) : dayChallenge.PartTwoFromInput(inputLines);
	stopwatch.Stop();
	return new ChallengeResult(dayChallenge, part, result, stopwatch.ElapsedTicks);
}

static IDayChallenge[] GetAllDayChallenges() => typeof(IDayChallenge).Assembly.GetTypes()
	.Where(t => t.GetInterfaces().Contains(typeof(IDayChallenge)))
	.Select(t => (IDayChallenge)t.GetConstructors().Single().Invoke(Array.Empty<object>()))
	.ToArray();

string[] GetDayInput(HttpClient httpClient, int day) {
	if (!Directory.Exists("input")) {
		Directory.CreateDirectory("input");
	}
	string expectedFilePath = Path.Join("input", $"{day}.txt");
	if (!File.Exists(expectedFilePath)) {
		string[] inputLines = httpClient.GetStringAsync($"/2021/day/{day}/input").Result.Trim().Split('\n');
		File.WriteAllLines(expectedFilePath, inputLines);
		return inputLines;
	} else {
		return File.ReadAllLines(expectedFilePath);
	}
}

string? session = Environment.GetEnvironmentVariable("AdventOfCodeSession");
if (session == null) {
	Console.Error.WriteLine("Please set the \"AdventOfCodeSession\" environment variable from your cookie");
	Environment.Exit(1);
}

IDayChallenge[] dayChallenges = GetAllDayChallenges();

List<Func<ChallengeResult>> singleThreadedChallenges = new();
List<Func<ChallengeResult>> multiThreadedChallenges = new();

{
	using HttpClient httpClient = new();
	httpClient.BaseAddress = new(@"https://adventofcode.com/");
	httpClient.DefaultRequestHeaders.Add("cookie", $"session={session}");

	foreach (IDayChallenge dayChallenge in dayChallenges.Where(dc => !dc.IsIgnored)) {
		Console.WriteLine($"Getting input for day {dayChallenge.Day}...");
		string[] inputLines = GetDayInput(httpClient, dayChallenge.Day);
		ChallengeResult partOneTask() => RunSolution(dayChallenge, 1, inputLines);
		if (dayChallenge.IsPartOneSingleThreaded) {
			singleThreadedChallenges.Add(partOneTask);
		} else {
			multiThreadedChallenges.Add(partOneTask);
		}
		ChallengeResult partTwoTask() => RunSolution(dayChallenge, 2, inputLines);
		if (dayChallenge.IsPartTwoSingleThreaded) {
			singleThreadedChallenges.Add(partTwoTask);
		} else {
			multiThreadedChallenges.Add(partTwoTask);
		}
	}
}

Console.WriteLine($"Running challenges...");
Console.WriteLine();

Stopwatch stopwatch = new();
stopwatch.Start();
IEnumerable<ChallengeResult> singleThreadedResults = singleThreadedChallenges.AsParallel().Select(f => f());
IEnumerable<ChallengeResult> multiThreadedResults = multiThreadedChallenges.Select(f => f());
List<ChallengeResult> allResults = singleThreadedResults.Concat(multiThreadedResults).ToList();
stopwatch.Stop();

foreach (ChallengeResult challengeResult in allResults.OrderBy(r => r.DayChallenge.Day * 2 + r.Part)) {
	Console.WriteLine($"""
		Day {challengeResult.DayChallenge.Day} - Part {challengeResult.Part} - {challengeResult.DayChallenge.Name}
		{challengeResult.Result}
		Completed in {challengeResult.Ticks / (Stopwatch.Frequency / 1000000)}μs

		""");
}

Console.WriteLine($"Total time {stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000)}μs");

internal record ChallengeResult(IDayChallenge DayChallenge, int Part, string Result, long Ticks);
