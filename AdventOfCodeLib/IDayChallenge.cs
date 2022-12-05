namespace AdventOfCodeLib;

/// <summary>
/// Defines the operations a day's challenges would have. All <see cref="IDayChallenge"/> implementations must have a <see cref="DayDetailsAttribute"/>.
/// </summary>
public interface IDayChallenge {
	/// <summary>
	/// Returns a string representation of the solution to part 1 given an array of string lines from its input.
	/// </summary>
	public string PartOneFromInput(string[] inputLines);
	/// <summary>
	/// Returns a string representation of the solution to part 2 given an array of string lines from its input.
	/// </summary>
	public string PartTwoFromInput(string[] inputLines);

	/// <summary>
	/// The day of this challenge.
	/// </summary>
	public int Day => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().Day;

	/// <summary>
	/// The attempt of this challenge.
	/// </summary>
	public int Attempt => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().Attempt;
	/// <summary>
	/// The name of this challenge.
	/// </summary>
	public string Name => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().Name;
	/// <summary>
	/// Whether this challenge should be ignored, when run in bulk (e.g. it takes too long to execute in sequence).
	/// </summary>
	public bool IsIgnored => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().Ignore;
	/// <summary>
	/// Whether part 1 only requires a single thread to execute.
	/// </summary>
	public bool IsPartOneSingleThreaded => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().PartOneSingleThreaded;
	/// <summary>
	/// Whether part 2 only requires a single thread to execute.
	/// </summary>
	public bool IsPartTwoSingleThreaded => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().PartTwoSingleThreaded;
}
