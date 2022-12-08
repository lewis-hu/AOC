<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../../shared.linq"
const bool IS_TESTING = false;
const int DayNumber = 3;

static async Task Main()
{
	int dayNumber = int.Parse(Path.GetFileNameWithoutExtension(Util.CurrentQueryPath).Last().ToString());
	await Part1(dayNumber).Dump("Part1");
	await Part2(dayNumber).Dump("Part2");
}

public static async Task<long> Part1(int dayNumber)
{
	var input = await GetPuzzleInputAsync(dayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
	return inputList
		.Select(line =>
		{
			var halves = line.Chunk(line.Length / 2).Select(chunk => new string(chunk)).ToList();
			var commonChar= halves[0].Intersect(halves[1]).FirstOrDefault();
			return CharToNumber(commonChar);
		}).Sum();
		
}
private static int CharToNumber(char input) 
{
		return char.IsUpper(input) 
				? (((int) input ) - 38 )
				: (((int) input )-96 );
}
public static async Task<long> Part2(int dayNumber)
{
	var input = await GetPuzzleInputAsync(dayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
	return inputList.
		Chunk(3)
		.Select(group =>
		{
			return CharToNumber(group[0].Intersect(group[1]).Intersect(group[2]).First());
		}).Sum();
}

