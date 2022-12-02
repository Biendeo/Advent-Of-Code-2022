using AdventOfCodeLib;
using System.Diagnostics;

internal class Program {
	public static void Main(string[] args) {
		string? session = Environment.GetEnvironmentVariable("AdventOfCodeSession");
		if (session == null) {
			Console.Error.WriteLine("Please set the \"AdventOfCodeSession\" environment variable from your cookie");
			Environment.Exit(1);
		}

		if (args.Contains("--benchmark")) {
			RunAllBenchmarks(session, int.Parse(args[Array.IndexOf(args, "--benchmark") + 1]));
		} else {
			RunAllChallenges(session);
		}
	}

	static ChallengeResult RunSolution(IDayChallenge dayChallenge, int part, string[] inputLines) {
		Stopwatch stopwatch = new();
		stopwatch.Start();
		string result = part == 1 ? dayChallenge.PartOneFromInput(inputLines) : dayChallenge.PartTwoFromInput(inputLines);
		stopwatch.Stop();
		return new ChallengeResult(dayChallenge, part, result, stopwatch.ElapsedTicks);
	}

	static BenchmarkResult BenchmarkSolution(IDayChallenge dayChallenge, int part, string[] inputLines, int iterations) {
		long[] tickResults = new long[iterations];
		int index = 0;
		Enumerable.Range(0, iterations).AsParallel().ForAll((_) => {
			Stopwatch stopwatch = new();
			stopwatch.Start();
			if (part == 1)
				dayChallenge.PartOneFromInput(inputLines);
			else
				dayChallenge.PartTwoFromInput(inputLines);
			stopwatch.Stop();
			lock (tickResults) {
				tickResults[index] = stopwatch.ElapsedTicks;
				++index;
			}
		});
		long[] tickResultsSorted = tickResults.Order().ToArray();
		return new BenchmarkResult(dayChallenge, part, tickResults.Sum(), tickResultsSorted.First(), tickResultsSorted[iterations / 2], tickResultsSorted.Last(), iterations);
	}

	static IDayChallenge[] GetAllDayChallenges() => typeof(IDayChallenge).Assembly.GetTypes()
		.Where(t => t.GetInterfaces().Contains(typeof(IDayChallenge)))
		.Select(t => (IDayChallenge)t.GetConstructors().Single().Invoke(Array.Empty<object>()))
		.ToArray();

	static HttpClient GetAdventOfCodeClient(string session) {
		HttpClient httpClient = new();
		httpClient.BaseAddress = new(@"https://adventofcode.com/");
		httpClient.DefaultRequestHeaders.Add("cookie", $"session={session}");
		return httpClient;
	}

	static string[] GetDayInput(HttpClient httpClient, int day) {
		if (!Directory.Exists("input")) {
			Directory.CreateDirectory("input");
		}
		string expectedFilePath = Path.Join("input", $"{day}.txt");
		if (!File.Exists(expectedFilePath)) {
			string[] inputLines = httpClient.GetStringAsync($"/2022/day/{day}/input").Result.Trim().Split('\n');
			File.WriteAllLines(expectedFilePath, inputLines);
			return inputLines;
		} else {
			return File.ReadAllLines(expectedFilePath);
		}
	}

	static (List<Func<ChallengeResult>> singleThreadedChallenges, List<Func<ChallengeResult>> multiThreadedChallenges) AggregateChallengesByThreads(string session) {
		IDayChallenge[] dayChallenges = GetAllDayChallenges();

		List<Func<ChallengeResult>> singleThreadedChallenges = new();
		List<Func<ChallengeResult>> multiThreadedChallenges = new();

		using HttpClient httpClient = GetAdventOfCodeClient(session);

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

		return (singleThreadedChallenges, multiThreadedChallenges);
	}

	static void RunAllChallenges(string session) {
		(List<Func<ChallengeResult>> singleThreadedChallenges, List<Func<ChallengeResult>> multiThreadedChallenges) = AggregateChallengesByThreads(session);

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
	}

	static void RunAllBenchmarks(string session, int iterations) {
		IDayChallenge[] dayChallenges = GetAllDayChallenges();

		Console.WriteLine($"Running benchmark...");
		Console.WriteLine();

		using HttpClient httpClient = GetAdventOfCodeClient(session);

		foreach (IDayChallenge dayChallenge in dayChallenges) {
			Console.WriteLine($"Getting input for day {dayChallenge.Day}...");
			string[] inputLines = GetDayInput(httpClient, dayChallenge.Day);

			for (int i = 1; i <= 2; ++i) {
				Console.WriteLine($"Day {dayChallenge.Day} - Part {i} - {dayChallenge.Name}");
				BenchmarkResult benchmarkResult = BenchmarkSolution(dayChallenge, i, inputLines, iterations);
				Console.WriteLine($"""
					Completed {benchmarkResult.Iterations} in {benchmarkResult.TotalTicks / (Stopwatch.Frequency / 1000000)}μs
					Average   {benchmarkResult.TotalTicks / (Stopwatch.Frequency / 1000000.0) / benchmarkResult.Iterations:0.00000}μs
					Min       {benchmarkResult.MinTicks / (Stopwatch.Frequency / 1000000)}μs
					Median    {benchmarkResult.MedianTicks / (Stopwatch.Frequency / 1000000)}μs
					Max       {benchmarkResult.MaxTicks / (Stopwatch.Frequency / 1000000)}μs
				
					""");
			}
		}
	}
}

internal record ChallengeResult(IDayChallenge DayChallenge, int Part, string Result, long Ticks);
internal record BenchmarkResult(IDayChallenge DayChallenge, int Part, long TotalTicks, long MinTicks, long MedianTicks, long MaxTicks, int Iterations);
