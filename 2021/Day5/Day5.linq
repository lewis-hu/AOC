<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.FSharpExtensions</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 5;
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
	List<(int FromX, int FromY, int ToX, int ToY)> Coords = new List<(int, int, int, int)>();
	for(var i=0;i < inputList.Count();i++) 
	{
		var fromToCoord = inputList[i].Split(" -> ");
		var from = fromToCoord[0].Split(",").Select(l => int.Parse(l)).ToArray();
		var to = fromToCoord[1].Split(",").Select(l => int.Parse(l)).ToArray();
		var tuple = (from[0], from[1], to[0], to[1]);
		Coords.Add(tuple);
	}
	var validCoords = Coords;
	var gridWidth = Math.Max(validCoords.Max(c => c.ToX),validCoords.Max(c => c.FromX));
	var gridHeight = Math.Max(validCoords.Max(c => c.ToY),validCoords.Max(c => c.FromY));
	var grid = new int[gridHeight + 1, gridWidth + 1];
	//grid.Dump();
		foreach (var coord in validCoords)
		{
		if (coord.FromY == coord.ToY)
		{
			for (var x = coord.FromX; x <= coord.ToX; x++)
			{
				grid[coord.FromY, x] += 1;
			}
			for (var x = coord.ToX; x <= coord.FromX; x++)
			{
				try
				{
					grid[coord.FromY, x] += 1;
				}
				catch (IndexOutOfRangeException){
					coord.FromY.Dump("FromY");
					x.Dump("x");
				}
			}
		}
			if (coord.FromX == coord.ToX)
			{
				for (var y = coord.FromY; y <= coord.ToY; y++)
				{
					try
				{

					grid[y, coord.ToX] += 1;
				}
				catch (IndexOutOfRangeException)
				{
					coord.ToX.Dump("coord.ToX");
					y.Dump("y");
				}
			}
			for (var y = coord.ToY; y <= coord.FromY; y++)
				{
					grid[y, coord.ToX] += 1;
				}
			}
		}
	var overlapCount = default(int);
	for(var x = 0; x<grid.GetLength(1);x++) {
		for(var y = 0;y<grid.GetLength(0);y++) {
			if(grid[y,x]>1) overlapCount++;
		}
	}
	return overlapCount;
}

public static async Task<int> Part2()
{
	var input = await GetTestInputAsync(DayNumber);
	//var input = await GetPuzzleInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
	List<(int FromX, int FromY, int ToX, int ToY)> Coords = new List<(int, int, int, int)>();
	for (var i = 0; i < inputList.Count(); i++)
	{
		var fromToCoord = inputList[i].Split(" -> ");
		var from = fromToCoord[0].Split(",").Select(l => int.Parse(l)).ToArray();
		var to = fromToCoord[1].Split(",").Select(l => int.Parse(l)).ToArray();
		var tuple = (from[0], from[1], to[0], to[1]);
		Coords.Add(tuple);
	}
	var validCoords = Coords;
	var gridWidth = Math.Max(validCoords.Max(c => c.ToX), validCoords.Max(c => c.FromX));
	var gridHeight = Math.Max(validCoords.Max(c => c.ToY), validCoords.Max(c => c.FromY));
	var grid = new int[gridHeight + 1, gridWidth + 1];
	//grid.Dump();
	foreach (var c in validCoords)
	{
		//		if (coord.FromY == coord.ToY)
		//		{
		//			for (var x = coord.FromX; x <= coord.ToX; x++)
		//			{
		//				grid[coord.FromY, x] += 1;
		//			}
		//			for (var x = coord.ToX; x <= coord.FromX; x++)
		//			{
		//				grid[coord.FromY, x] += 1;
		//			}
		//			continue;
		//		}
		//		if (coord.FromX == coord.ToX)
		//		{
		//			for (var y = coord.FromY; y <= coord.ToY; y++)
		//			{
		//				grid[y, coord.ToX] += 1;
		//
		//			}
		//			for (var y = coord.ToY; y <= coord.FromY; y++)
		//			{
		//				grid[y, coord.ToX] += 1;
		//			}
		//			continue;
		//		}

		var direction = ((c.FromX < c.ToX) == (c.FromY < c.ToY))?  1 :-1;

		if(c.FromX != c.ToX && c.FromY != c.ToY) {
			if(c.FromX > c.ToX)
			{
				
			}
		}
		if(c.FromY < c.ToY)
			for (var y = c.FromY; y <= c.ToY; y++)
			{
				grid[y, c.ToX+y] += 1;

			}
			
		if(c.FromY > c.ToY)
			for (var y = c.ToY; y <= c.FromY; y++)
			{
				grid[y, c.ToX-y] += 1;
			}

	}
	grid.Dump();
	var overlapCount = default(int);
	for (var x = 0; x < grid.GetLength(1); x++)
	{
		for (var y = 0; y < grid.GetLength(0); y++)
		{
			if (grid[y, x] > 1) overlapCount++;
		}
	}
	return overlapCount;

}

