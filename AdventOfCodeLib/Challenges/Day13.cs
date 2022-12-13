using System.Text;

namespace AdventOfCodeLib.Challenges;

[DayDetails(Day = 13, Name = "Distress Signal")]
public class Day13 : IDayChallenge {
	public class Packet : IComparable, IComparable<Packet> {
		public List<Packet> Packets { get; init; } = new();
		public int Value { get; init; } = 0;
		public required bool IsList { get; init; } = false;

		public static Packet Parse(string packetString) {
			if (packetString == "[]") {
				return new() {
					Packets = new(),
					IsList = true
				};
			} else if (packetString[0] == '[') {
				return new() {
					Packets = SplitStringToTokens(packetString).Select(Parse).ToList(),
					IsList = true
				};
			} else {
				return new() {
					Value = int.Parse(packetString),
					IsList = false
				};
			}
		}

		public int CompareTo(Packet? packet) {
			if (ReferenceEquals(this, packet)) {
				return 0;
			}
			if (packet is null) {
				return -1;
			}
			if (IsList && !packet.IsList) {
				return CompareTo(new Packet() {
					Packets = new List<Packet>() {
						packet
					},
					IsList = true
				});
			} else if (!IsList && packet.IsList) {
				return new Packet() {
					Packets = new List<Packet>() {
						this
					},
					IsList = true
				}.CompareTo(packet);
			} else if (IsList && packet.IsList) {
				return Packets.Zip(packet.Packets).Select(pair => pair.First.CompareTo(pair.Second)).FirstOrDefault(x => x != 0, Packets.Count.CompareTo(packet.Packets.Count));
			} else {
				return Value.CompareTo(packet.Value);
			}
		}

		public int CompareTo(object? obj) {
			if (ReferenceEquals(this, obj)) {
				return 0;
			}
			if (obj is null) {
				return -1;
			}
			if (obj is Packet packet) {
				return CompareTo(packet);
			} else {
				return -1;
			}
		}

		public static bool operator <=(Packet a, Packet b) => a.CompareTo(b) <= 0;

		public static bool operator >=(Packet a, Packet b) => a.CompareTo(b) >= 0;

		public override bool Equals(object? obj) {
			if (ReferenceEquals(this, obj)) {
				return true;
			}
			if (obj is null) {
				return false;
			}
			if (obj is Packet packet) {
				return CompareTo(packet) == 0;
			} else {
				return false;
			}
		}

		public override int GetHashCode() => base.GetHashCode();
	}

	public string PartOneFromInput(string[] inputLines) => PartOne(inputLines).ToString();

	public int PartOne(string[] packets) => Enumerable.Range(0, (packets.Length + 1) / 3).Sum(i =>  Packet.Parse(packets[3 * i]) <= Packet.Parse(packets[3 * i + 1]) ? i + 1 : 0);

	public string PartTwoFromInput(string[] inputLines) => PartTwo(inputLines).ToString();

	public int PartTwo(string[] packets) {
		Packet twoPacket = Packet.Parse("[[2]]");
		Packet sixPacket = Packet.Parse("[[6]]");
		List<Packet> allPackets = packets.Where(s => !string.IsNullOrWhiteSpace(s)).Select(Packet.Parse).Append(twoPacket).Append(sixPacket).Order().ToList();
		return (allPackets.IndexOf(twoPacket) + 1) * (allPackets.IndexOf(sixPacket) + 1);
	}

	private static IEnumerable<string> SplitStringToTokens(string packetString) {
		StringBuilder sb = new();
		int depth = 1;
		for (int i = 1; i < packetString.Length - 1; ++i) {
			if (packetString[i] == '[')
				++depth;
			else if (packetString[i] == ']')
				--depth;
			if (depth == 1 && packetString[i] == ',') {
				yield return sb.ToString();
				sb.Clear();
			} else {
				sb.Append(packetString[i]);
			}
		}
		yield return sb.ToString();
	}
}
