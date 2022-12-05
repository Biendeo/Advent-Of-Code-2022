namespace AdventOfCodeLib;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DayDetailsAttribute : Attribute {
	public required int Day;
	public int Attempt = 1;
	public required string Name;
	public bool Ignore = false;
	public bool PartOneSingleThreaded = true;
	public bool PartTwoSingleThreaded = true;
}
