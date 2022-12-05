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
								.Select(c => 
									Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2)
									.PadLeft(4,'0')
								)
								.ToList());
	var binaryString = string.Join(null,puzzleList).Dump();
	var cursorLocation = 0;
	var versionSum = 0;
	while (cursorLocation < binaryString.Length && 
			! binaryString.Substring(cursorLocation,binaryString.Length - cursorLocation).AllZeroes())
	{
		(cursorLocation, var version) = GetNextMessageBit(binaryString, cursorLocation);
		versionSum += version;
	}
	return versionSum;
}

public static class StringExtensions
{
	public static bool AllZeroes(this string str)
	{
		return str.Substring(0, str.Length).Select(x => int.Parse(x.ToString())).Sum() == 0;
	}
}
	
private static (int cursor,int version) GetNextMessageBit(string binaryString, int cursor = 0)
{
	const int LITERAL_TYPE = 4;
	cursor.Dump("Cursor location:");
	var version = binaryString.Substring(cursor, 3);//.Dump("Version");
	cursor += 3;
	var versionDec = Convert.ToInt32(version, 2);//.Dump("VersionDecimal");
	var type = binaryString.Substring(cursor, 3);//.Dump("type");
	cursor += 3;
	var typeDec = Convert.ToInt32(type, 2);//.Dump("TypeDecimal");

	if (typeDec == LITERAL_TYPE)
	{
		while (cursor < binaryString.Length && binaryString[cursor] != '0')
		{
			cursor += 5;
		}
		cursor += 5;
	} 
	else
	{
		var lengthType = binaryString.Substring(cursor, 1);//.Dump("type");
		cursor += 1;
		if (lengthType == "0")
		{
			var payloadLength = binaryString.Substring(cursor, 15);//.Dump("Version");
			cursor += 15;
			var payloadLengthDec = Convert.ToInt32(payloadLength, 2);//.Dump("payloadLengthDec");
			while (cursor < binaryString.Length 
					&& !binaryString.Substring(cursor,binaryString.Length - cursor)
						.AllZeroes()) {
				(cursor, var subVersion) = GetNextMessageBit(binaryString, cursor);
				versionDec += subVersion;
			}
		}
		else
		{
			var numPackets = binaryString.Substring(cursor, 11);//.Dump("Version");
			cursor += 11;
			var numPacketsDec = Convert.ToInt32(numPackets, 2);//.Dump("numPacketsDec");
			for(var n = 0; n < numPacketsDec; n++)
			{
				(cursor, var subVersion) = GetNextMessageBit(binaryString, cursor);
				versionDec += subVersion;
			}
		}
	}
	return (cursor, versionDec);//.Dump("CursorLocation");
}

public static async Task<long> Part2()
{
	var input = await GetTestInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

	return default;

}
