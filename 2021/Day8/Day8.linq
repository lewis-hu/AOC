<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.FSharpExtensions</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 8;
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
	var puzzleList = inputList
						.Select(line => line.Split(" | "))
						.Select(l => (input:l.First(),output: l.Last())).ToList();
	var outputList = puzzleList.SelectMany(l => l.output.Split(" ")).Dump();

	return outputList.Where(l => new[] { 2, 3, 4, 7}.Contains(l.Length)).Count().Dump();;

}

public static async Task<long> Part2()
{
	var input = await GetTestInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

	return default;

}
