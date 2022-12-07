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
	await Part1().Dump("Part1");
	await Part2().Dump("Part2");
}

public static async Task<long> Part1()
{
	var score = 0;
	var input = await GetPuzzleInputAsync(DayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	inputList.Dump();
	return score;
}
public static async Task<long> Part2()
{
	var score = 0;
	return score;
}

