using System.Reflection;

namespace AdventOfCodeLib;

/// <summary>
/// Defines the operations a day's challenges would have.
/// </summary>
public interface IDayChallenge {
	/// <summary>
	/// 
	/// </summary>
	/// <param name="inputLines"></param>
	/// <returns></returns>
	public string PartOneFromFile(string[] inputLines);
	public string PartTwoFromFile(string[] inputLines);

	public int Day => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().Day;
	public string Name => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().Name;
	public bool IsIgnored => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().Ignore;
	public bool IsPartOneSingleThreaded => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().PartOneSingleThreaded;
	public bool IsPartTwoSingleThreaded => GetType().GetGuaranteedCustomAttribute<DayDetailsAttribute>().PartTwoSingleThreaded;
}
