<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../../shared.linq"
static async Task Main()
{
	await Part1().Dump("Part1");
	await Part2().Dump("Part2");
}

public static async Task<int> Part1()
{
	const int DayNumber = 1;

	var input = await GetPuzzleInputAsync(DayNumber);

	//var input = await GetTestInputAsync(DayNumber);

	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	int increaseCount = 0;
	for (int i = 1; i < inputList.Count(); i++)
	{
		//inputList[i].Dump();
		if (string.IsNullOrEmpty(inputList[i])) continue;
		if (int.Parse(inputList[i]) - int.Parse(inputList[i - 1]) > 0)
		{
			increaseCount++;
		}
	}
	return increaseCount;
}

public static async Task<int> Part2()
{
	const int DayNumber = 1;

	var input = await GetPuzzleInputAsync(DayNumber);

	//var input = await GetTestInputAsync(DayNumber);

	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	int increaseCount = 0;
	var windowList = new List<int>();
	for (int i = 0; i < inputList.Count() - 2; i++)
	{
		var sumWindow = 0;
		//inputList[i].Dump();
		if (string.IsNullOrEmpty(inputList[i])) continue;
		for (int j = 0; j < 3; j++)
		{
			if (string.IsNullOrEmpty(inputList[i+j])) break;
			sumWindow += int.Parse(inputList[i + j]);
		}
		windowList.Add(sumWindow);
	}
	//windowList.Dump();
	//if (string.IsNullOrEmpty(inputList[i])) continue;
	for (int k = 1; k < windowList.Count(); k++)
	{
		if(windowList[k] - windowList[k - 1] > 0)
		{
			increaseCount++;
		}
	}
	return increaseCount;
}