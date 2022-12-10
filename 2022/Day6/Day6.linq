<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../../shared.linq"
const bool IS_TESTING = false;

static async Task Main()
{
	int dayNumber = GetDayNumberUsingFileName();
	await Part1(dayNumber).Dump("Part1");
	await Part2(dayNumber).Dump("Part2");
}
public static async Task<int> Part1(int dayNumber)
{
	var input = await GetPuzzleInputAsync(dayNumber, IS_TESTING);
	input.Dump();
	var headerSize = 4;
	for(var i=0; i<input.Length; i++)
	{
		var takeFour = input.Skip(i).Take(headerSize);
		if(takeFour.Distinct().Count() == headerSize)
		{
			return i + headerSize;
		}
	}
	return 0;
}

public static async Task<int> Part2(int dayNumber)
{
	var input = await GetPuzzleInputAsync(dayNumber, IS_TESTING);
	input.Dump();
	var headerSize = 14;
	for (var i = 0; i < input.Length; i++)
	{
		var takeFour = input.Skip(i).Take(headerSize);
		if (takeFour.Distinct().Count() == headerSize)
		{
			return i + headerSize;
		}
	}
	return 0;
}