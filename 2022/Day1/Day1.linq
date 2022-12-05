<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../../shared.linq"
const bool IS_TESTING = false;
static async Task Main()
{
	await Part1().Dump("Part1");
	await Part2().Dump("Part2");
}

public static async Task<long> Part1()
{
	const int DayNumber = 1;

	var input = await GetPuzzleInputAsync(DayNumber, IS_TESTING);

	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	List<long> elves = new List<long> { 0 };
	int elfIndex = 0;
	foreach (var i in inputList)
	{
		if (string.IsNullOrWhiteSpace(i))
		{
			elves.Add(0);
			elfIndex++;
			continue;
		}
		elves[elfIndex] += int.Parse(i);
	}
	elves.Sort((x, y) => y.CompareTo(x));
	return elves.First();
	
}

public static async Task<long> Part2()
{
	const int DayNumber = 1;

	var input = await GetPuzzleInputAsync(DayNumber, IS_TESTING);

	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	List<long> elves = new List<long> { 0 };
	int elfIndex = 0;
	foreach (var i in inputList)
	{
		if (string.IsNullOrWhiteSpace(i))
		{
			elves.Add(0);
			elfIndex++;
			continue;
		}
		elves[elfIndex] += int.Parse(i);
	}
	elves.Sort((x, y) => y.CompareTo(x));
	return elves.Take(3).Sum();

}