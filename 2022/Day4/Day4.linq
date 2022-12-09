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

public static IEnumerable<int> GetNumberListFor(string[] section)
{
	int start = int.Parse(section[0].ToString());
	int end = int.Parse(section[1].ToString());
	return Enumerable.Range(start, end - start + 1);
}
public static async Task<long> Part1(int dayNumber)
{
	var input = await GetPuzzleInputAsync(dayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
	//inputList.Dump();
	return inputList.Select(i => 
	{
		var sectionPair = i.Split(',').ToList().Select(section => section.Split('-')).ToList();
		var firstPairRange = GetNumberListFor(sectionPair[0]);
		var secondPairRange = GetNumberListFor(sectionPair[1]);
		var subset = firstPairRange.Intersect(secondPairRange);
		var isFullyContained = subset.Count() == firstPairRange.Count() || subset.Count() == secondPairRange.Count();
		return (isFullyContained) ? 1 : 0;
	}).Sum();
	
}

public static async Task<long> Part2(int dayNumber)
{
	var input = await GetPuzzleInputAsync(dayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
	return inputList.Select(i =>
	{
		var sectionPair = i.Split(',').ToList().Select(section => section.Split('-')).ToList();
		var firstPairRange = GetNumberListFor(sectionPair[0]);
		var secondPairRange = GetNumberListFor(sectionPair[1]);
		var subset = firstPairRange.Intersect(secondPairRange);
		var isOverlap = subset.Count() > 0;
		return (isOverlap) ? 1 : 0;
	}).Sum();
}

