<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.FSharpExtensions</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 16;
static async Task Main()
{
	await Part1().Dump("Part1");
	await Part2().Dump("Part2");
}

public static async Task<long> Part1()
{
	const int HEXBINSIZE = 4;
	var input = await GetTestInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
							
	var puzzleList = inputList
						.SelectMany(line => line
							.Select(c =>  c.ToPaddedBinary())
							.ToList());
	var binaryString = string.Join(null,puzzleList).Dump();
	var cursorLocation = 0;
	var versionSum = 0;
	
	var packet = new Packet(binaryString,0);
	
	packet.Dump();
	return versionSum;
}

public static class Extensions
{
	public static string ToPaddedBinary(this char c)
	{
		return Convert.ToString(Convert
								.ToInt32(c.ToString(), 16), 2)
								.PadLeft(4, '0');                                           
	}
	public static int BinaryToDecimal(this string str)
	{
		return Convert.ToInt32(str, 2);
	}
}

public class Packet
{
	public Packet(string binaryString, int start)
	{
		var versionPos = start;
		var typePos = start + 3;
		Version = binaryString.Substring(versionPos, 3).BinaryToDecimal();
		Type = binaryString.Substring(typePos, 3).BinaryToDecimal();
		var lengthTypePos = IsLiteral ? null : (int?)typePos + 1;
		if (lengthTypePos != null)
		{
			LengthType = binaryString.Substring(lengthTypePos.Value, 1).BinaryToDecimal();
		}
		if (!IsLiteral)
		{
			Packets.Add(new Packet(binaryString, start + 6));
		}
		else
		{
			var literalValuesPos = typePos + 3;
			GetLiteralValues(binaryString, literalValuesPos);
		}
	}

	void GetLiteralValues(string binaryString, int cursor)
	{
		var isLastLiteral = binaryString.Substring(cursor,1) == "0";
		while(!isLastLiteral)
		{
			LiteralValues.Add(binaryString.Substring(cursor, 4).BinaryToDecimal());
			cursor += 5;
			isLastLiteral = binaryString.Substring(cursor,1) == "0";
		}
		return;
	}

	public int Version { get; set; }
	public int Type { get; set; }
	public int? LengthType { get; set; }
	public bool IsLiteral => Type == 4;
	public List<Packet> Packets { get; set; } = new List<Packet>();
	public List<int> LiteralValues { get; set; }= new List<int>();
	
}
//public class LiteralPacket :Packet 
//{
//	public LiteralPacket(string binaryString, int start): base(binaryString,start)
//	{
//		if(TypeId != 4) {
//			throw new InvalidDataException();
//		}
//	}
//	public int LiteralValue { get; set; }
//
//}
//public class OperatorPacket : Packet
//{
//	public OperatorPacket(string binaryString, int start) : base(binaryString, start)
//	{
//		if (TypeId == 4)
//		{
//			throw new InvalidDataException();
//		}
//
//	}
//	public int LengthTypeId { get; set; }
//	public List<Packet> Packets { get; set; }
//}


private static (int cursor, int version) GetNextMessageBit(string binaryString, int cursor = 0)
{
	return default;
}

public static async Task<long> Part2()
{
	var input = await GetTestInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

	return default;

}
