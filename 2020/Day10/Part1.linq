<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../shared.linq"
static async Task Main()
{
	const int DayNumber = 10;
	
	var input = await GetPuzzleInputAsync(DayNumber);

//	var input = await GetTestInputAsync(DayNumber);
	
	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	inputList.Dump();
	
}
