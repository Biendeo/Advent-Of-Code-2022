using System.Reflection;

namespace AdventOfCodeLib;

internal static class Helper {
	/// <summary>
	/// Returns an enumerable of (x, y) pairs where startX <= x <= endX and startY <= y <= endY.
	/// </summary>
	internal static IEnumerable<(int x, int y)> GetTwoDimensionalRange(int startX, int endX, int startY, int endY) => Enumerable.Range(startX, endX - startX + 1).SelectMany(x => Enumerable.Range(startY, endY - startY + 1).Select(y => (x, y)));

	/// <summary>
	/// Returns an enumerable of (x, y, z) triples where startX <= x <= endX, startY <= y <= endY, and startZ <= z <= endZ.
	/// </summary>
	internal static IEnumerable<(int x, int y, int z)> GetThreeDimensionalRange(int startX, int endX, int startY, int endY, int startZ, int endZ) => Enumerable.Range(startX, endX - startX + 1).SelectMany(x => Enumerable.Range(startY, endY - startY + 1).SelectMany(y => Enumerable.Range(startZ, endZ - startZ + 1).Select(z => (x, y, z))));

	/// <summary>
	/// Returns an enumerable of (a, b) pairs where each pair is one Cartesian value away from the given (x, y). If <paramref name="diagonals"/> is true, then diagonal values of two Cartesian steps is also included (e.g. (x - 1, y - 1)).
	/// The returned values will only include coordinates (a, b) within the bounds minX <= a <= maxX and minY <= b <= maxY.
	/// </summary>
	internal static IEnumerable<(int x, int y)> GetNeighbors(int x, int y, int minX, int maxX, int minY, int maxY, bool diagonals) {
		if (diagonals && x > minX && y > minY) yield return (x - 1, y - 1);
		if (y > minY) yield return (x, y - 1);
		if (diagonals && x < maxX && y > minY) yield return (x + 1, y - 1);
		if (x > minX) yield return (x - 1, y);
		if (x < maxX) yield return (x + 1, y);
		if (diagonals && x > minX && y < maxY) yield return (x - 1, y + 1);
		if (y < maxY) yield return (x, y + 1);
		if (diagonals && x < maxX && y < maxY) yield return (x + 1, y + 1);
	}

	/// <summary>
	/// Returns a custom attribute if exists, or throws a <see cref="Exception"/> if not present.
	/// </summary>
	internal static T GetGuaranteedCustomAttribute<T>(this Type type) where T : Attribute => type.GetCustomAttribute<T>() ?? throw new Exception($"Custom attribute {typeof(T)} was not found on type {type}");

	/// <summary>
	/// Returns an enumerable where each element is <paramref name="chunkSize"/> elements of <paramref name="source"/> at a time.
	/// </summary>
	internal static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize) {
		while (source.Any()) {
			yield return source.Take(chunkSize);
			source = source.Skip(chunkSize);
		}
	}

	/// <summary>
	/// Returns an enumerable of <paramref name="groups"/> enumerables dividing <paramref name="source"/> evenly.
	/// </summary>
	internal static IEnumerable<IEnumerable<T>> Divide<T>(this IEnumerable<T> source, int groups) {
		int itemSize = source.Count() / groups;
		while (source.Any()) {
			yield return source.Take(itemSize);
			source = source.Skip(itemSize);
		}
	}

	/// <summary>
	/// Returns an enumerable of <typeparamref name="T"/> that contains only values common to all elements in <paramref name="source"/>.
	/// </summary>
	internal static IEnumerable<T> IntersectAll<T>(this IEnumerable<IEnumerable<T>> source) {
		if (!source.Any())
			return Enumerable.Empty<T>();
		IEnumerable<T> set = new HashSet<T>(source.First());
		foreach (IEnumerable<T> item in source.Skip(1)) {
			set = set.Intersect(item);
		}
		return set;
	}

	internal static T IfElse<T>(this T t, Func<T, bool> f, T e) => f(t) ? t : e;
}
