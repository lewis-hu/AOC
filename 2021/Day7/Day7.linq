<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.FSharpExtensions</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 7;
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
	var crabList = inputList.First().Split(",").Select(s => int.Parse(s)).ToList();
	var minRange = Enumerable.Range(0, crabList.Max(l => l))
				.Select(c => (Position: c, Fuel: crabList.Sum(l => Math.Abs(c - l))))
				.Select(m => (Key:m.Fuel,MinRange:m))
				.Min().MinRange;
	return minRange.Fuel;
}

public static async Task<long> Part2()
{
	var input = await GetPuzzleInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
	var crabList = inputList.First().Split(",").Select(s => int.Parse(s)).ToList();
	var minRange = Enumerable.Range(0, crabList.Max(l => l))
				.Select(c => (Position: c, Fuel: crabList.Sum(l => GetFuelUsage(c,l))))
				.Select(m => (Key: m.Fuel, MinRange: m))
				.Min().MinRange;
	return minRange.Fuel;

}

static int GetFuelUsage(int position, int targetPosition)
{
	var diff = Math.Abs(position - targetPosition);
	var cumulativeSum = 0;
	return Enumerable.Range(0,diff).Sum(e => cumulativeSum += 1);
}
