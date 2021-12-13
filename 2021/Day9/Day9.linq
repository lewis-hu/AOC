<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.FSharpExtensions</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 9;
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
						.Select(line => line.Select(c => int.Parse(c.ToString())).ToList())
						.ToList();

	var minRow = 0;
	var maxRow = puzzleList.Count() - 1;
	var maxCol = puzzleList[minRow].Count() - 1;
	var lowPoints = new List<int>();
	for (var row = 0; row < puzzleList.Count(); row++)
	{
		for (var col = 0; col < puzzleList[row].Count(); col++)
		{
			int? up = row > 0 ? puzzleList[row - 1][col] : null;
			int? down = row < maxRow ? puzzleList[row + 1][col] : null;
			int? left = col > 0 ? puzzleList[row][col - 1] : null;
			int? right = col < maxCol ? puzzleList[row][col + 1] : null;
			var point = puzzleList[row][col];
			var isLowPoint = (up == null || point < up )
								&& ( down == null || point < down )
								&& ( left == null || point < left )
								&& ( right == null || point < right);
			if(isLowPoint)
			{
				lowPoints.Add(point);
			}

		}
	}
	lowPoints.Dump();

	return lowPoints.Sum(l => l + 1);

}

public static async Task<long> Part2()
{
	var input = await GetTestInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

	return default;

}
