<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.FSharpExtensions</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 4;
static async Task Main()
{
	await Part1().Dump("Part1");
	await Part2().Dump("Part2");
}

public static async Task<int> Part1()
{
	//var input = await GetTestInputAsync(DayNumber);
	var input = await GetPuzzleInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
	//inputList.Dump();
	var calls = inputList.First().Split(',').Select(c => int.Parse(c));
	//calls.Dump();
	var boardLines = inputList.Skip(1).ToList();
	//load boards into dictionary
	var boards = new Dictionary<int, List<List<int>>>();
	var boardId = 0;
	//boardLines.Dump();
	for(var line = 0; line < boardLines.Count(); line += 5,boardId++)
	{
		var board = boardLines.Skip(line).Take(5);
		var horizontalList = board.Select(board => board
					.Split(' ', StringSplitOptions.RemoveEmptyEntries)
					.Select(s => int.Parse(s))
					.ToList());
		boards.Add(boardId, horizontalList.ToList());
		var verticalList = Enumerable.Range(0,5).Select(col => board.Select(board => board
					.Split(' ', StringSplitOptions.RemoveEmptyEntries)[col])
					.Select(s => int.Parse(s)).ToList())
					.ToList();
		boards[boardId].AddRange(verticalList);
	}
	//boards.Dump();
	foreach (var call in calls)
	{
		foreach (var board in boards.Values)
		{
			board.ForEach(e => e.RemoveAll(r => r == call));
			if (board.Any(b => b.Count == 0))
			{
				call.Dump("Bingo");
				//board.Dump();
				(board.SelectMany(b => b).Sum() / 2).Dump("Sum");
				return (board.SelectMany(b => b).Sum() / 2) * call;
			}
		}
	}
	return default;
	//boards.Dump();
}

public static async Task<int> Part2()
{
	//var input = await GetTestInputAsync(DayNumber);
	var input = await GetPuzzleInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
	//inputList.Dump();
	var calls = inputList.First().Split(',').Select(c => int.Parse(c));
	//calls.Dump();
	var boardLines = inputList.Skip(1).ToList();
	//load boards into dictionary
	var boards = new List<(int BoardId, bool won, List<List<int>>Board)>();
	var boardId = 0;
	for (var line = 0; line < boardLines.Count(); line += 5, boardId++)
	{
		var board = boardLines.Skip(line).Take(5);
		var horizontalList = board.Select(board => board
					.Split(' ', StringSplitOptions.RemoveEmptyEntries)
					.Select(s => int.Parse(s))
					.ToList())
					.ToList();
		boards.Add((boardId, false, horizontalList));
		var verticalList = Enumerable.Range(0, 5)
							.Select(col => board
										.Select(board => board.Split(' ', StringSplitOptions.RemoveEmptyEntries)[col])
					 					.Select(s => int.Parse(s)).ToList()).ToList();
		boards[boardId].Board.AddRange(verticalList);

	}
	int lastWinningBoard = default;
	int? lastWinningCall = null;
	foreach (var call in calls)
	{
		for(var i=0;i < boards.Count(); i++)
		{
			boards[i].Board.ForEach(e => e.RemoveAll(r => r == call));
			
			if (boards[i].Board.Any(b => b.Count == 0) && !boards[i].won)
			{
				lastWinningBoard = i;
				lastWinningCall = call;
				boards[i] = (boards[i].BoardId, true, boards[i].Board);
				continue;
			} 
		}
		if (boards.All(b => b.won))
		{
			break;
		}
	}
	if(lastWinningBoard == default) 
	{
		throw new InvalidOperationException("No winner found at all, something is not right");
	}
	var winningBoard = boards[lastWinningBoard].Board;
	return (winningBoard.SelectMany(b => b).Sum() / 2) * lastWinningCall.Value;

}

