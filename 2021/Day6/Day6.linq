<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.FSharpExtensions</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 6;
static async Task Main()
{
	await Part1().Dump("Part1");
	await Part2().Dump("Part2");
}

public static async Task<long> Part1()
{
	var input = await GetPuzzleInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
	var fishList = inputList.First().Split(",").Select(s=> int.Parse(s)).ToList();
	return CalculateFishAfterNumberOfDays(80,fishList);

}

public static async Task<long> Part2()
{
	var input = await GetPuzzleInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
	var fishList = inputList.First().Split(",").Select(s => int.Parse(s)).ToList();
	return CalculateFishAfterNumberOfDays(256, fishList);

}

public static long CalculateFishAfterNumberOfDays(int days,List<int> initialFishList)
{
	var arr = new long[9];
	foreach (var fishAge in initialFishList)
	{
		arr[fishAge] += 1;
	}
	for (var day = 1; day <= days; day++)
	{
		var dayZeroCount = arr[0];
		for (var bucket = 1; bucket < arr.Length; bucket++)
		{
			arr[bucket - 1] = arr[bucket];
		}
		arr[6] += dayZeroCount;
		arr[8] = dayZeroCount;

	}
	return arr.Sum();
}

