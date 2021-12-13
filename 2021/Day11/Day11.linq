<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.FSharpExtensions</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 11;
static async Task Main()
{
	await Part1().Dump("Part1");
	await Part2().Dump("Part2");
}

public static async Task<long> Part1()
{
	var input = await GetTestInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
							
	var puzzleList = inputList
						.Select(line => line.Select(c => int.Parse(c.ToString())).ToList())
						.ToList();

	//puzzleList.Dump();
	var steps = 200;
	var maxRow = puzzleList.Count();
	var maxCol = puzzleList.First().Count();
	var flashCount = default(int);
	for(var step=0;step<steps;step++)
	{
		for(var row=0;row<maxRow;row++)
		{
			for(var col=0;col<maxCol;col++)
			{
				CheckFlashFor(row,col,puzzleList, maxRow, maxCol);
			}
		}
		flashCount += puzzleList.Sum(l => l.Count(l => l > 9));//.Dump($"FlashCount step {step}");
		for (var row = 0; row < maxRow; row++)
		{
			for (var col = 0; col < maxCol; col++)
			{
				if(puzzleList[row][col]>=10) puzzleList[row][col] = 0;
			}
		}
	}
	return flashCount;
}

static void CheckFlashFor(int row, int col, List<List<int>> puzzleList, int maxRow, int maxCol)
{
	int? up = row > 0 ? puzzleList[row - 1][col] : null;
	int? down = row < maxRow -1  ? puzzleList[row + 1][col] : null;
	int? left = col > 0 ? puzzleList[row][col - 1] : null;
	int? right = col < maxCol - 1 ? puzzleList[row][col + 1] : null;
	int? upLeft = (up != null && left != null) ? puzzleList[row - 1][col - 1] : null;
	int? upRight = (up != null && right != null) ? puzzleList[row - 1][col + 1] : null;
	int? downLeft = (down != null && left != null) ? puzzleList[row + 1][col - 1] : null;
	int? downRight = (down != null && right != null) ? puzzleList[row + 1][col + 1] : null;
	puzzleList[row][col]++;
	if (puzzleList[row][col] == 10)
	{
		if (up != null) CheckFlashFor(row - 1, col, puzzleList, maxRow, maxCol);
		if (down != null) CheckFlashFor(row + 1, col, puzzleList, maxRow, maxCol);
		if (left != null) CheckFlashFor(row, col - 1, puzzleList, maxRow, maxCol);
		if (right != null) CheckFlashFor(row, col + 1, puzzleList, maxRow, maxCol);
		if (upLeft != null) CheckFlashFor(row - 1, col - 1, puzzleList, maxRow, maxCol);
		if (upRight != null) CheckFlashFor(row - 1, col + 1, puzzleList, maxRow, maxCol);
		if (downLeft != null) CheckFlashFor(row + 1, col - 1, puzzleList, maxRow, maxCol);
		if (downRight != null) CheckFlashFor(row + 1, col + 1, puzzleList, maxRow, maxCol);
	}
}

public static async Task<long> Part2()
{
	var AllFlashStep = default(int);
	var input = await GetPuzzleInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

	var puzzleList = inputList
						.Select(line => line.Select(c => int.Parse(c.ToString())).ToList())
						.ToList();

	var steps = 600;
	var maxRow = puzzleList.Count();
	var maxCol = puzzleList.First().Count();
	var flashCount = default(int);
	for (var step = 0; step < steps; step++)
	{
		for (var row = 0; row < maxRow; row++)
		{
			for (var col = 0; col < maxCol; col++)
			{
				CheckFlashFor(row, col, puzzleList, maxRow, maxCol);
			}
		}
		flashCount += puzzleList.Sum(l => l.Count(l => l > 9));//.Dump($"FlashCount step {step}");
		for (var row = 0; row < maxRow; row++)
		{
			for (var col = 0; col < maxCol; col++)
			{
				if (puzzleList[row][col] >= 10) puzzleList[row][col] = 0;
			}
		}
		if (puzzleList.All(l => l.All(l => l == 0)))
		{
			AllFlashStep = step + 1;
			break;
		}
	}
	return AllFlashStep;
}
