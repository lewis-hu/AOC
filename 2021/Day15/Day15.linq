<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.FSharpExtensions</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 15;
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
	var result = GetLowRiskPoint(puzzleList);
	return result;
}

public static async Task<long> Part2()
{
	var input = await GetTestInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

	return default;

}

public static int GetLowRiskPoint(List<List<int>> puzzleList)
{
	if (((puzzleList == null) || (puzzleList.Count() == 0))) return 0;

	int rows = puzzleList.Count();
	int cols = puzzleList.First().Count();
	int[,] temp = new int[rows, cols];
	for (int row = 0; (row < rows); row++)
	{
		for (int col = 0; (col < cols); col++)
		{
			temp[row, col] = (row == 0 && col == 0) ? 0 : puzzleList[row][col];
			if (((row == 0) && (col > 0)))
			{
				temp[0, col] = (temp[0, col] + temp[0, (col - 1)]);
			}
			if (((col == 0) && (row > 0)))
			{
				temp[row, 0] = (temp[row, 0] + temp[(row - 1), 0]);
			}
			if (((row > 0) && (col > 0)))
			{
				temp[row, col] = (temp[row, col] + Math.Min(temp[(row - 1), col], temp[row, (col - 1)]));
			}
		}
	}
	return temp[(rows - 1), (cols - 1)];
}