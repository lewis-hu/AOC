<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 2;
static async Task Main()
{
	await Part1().Dump("Part1");
	await Part2().Dump("Part2");
}

public static async Task<int> Part1()
{
	var input = await GetPuzzleInputAsync(DayNumber);
	var h = 0;
	var d = 0;
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
	for (int i = 0; i < inputList.Count(); i++)
	{
		//if (string.IsNullOrEmpty(inputList[i])) continue;
		var line = inputList[i];
		//line.Dump();
		var command = Regex.Match(line, @"\w+").Value;
		var unit = int.Parse(Regex.Match(line, @"\d+").Value);

		switch (command)
		{
			case "forward":
				h += unit;
				break;
			case "back":
				h -= unit;
				break;
			case "up":
				d -= unit;
				break;
			case "down":
				d += unit;
				break;
		}
	}
	//h.Dump();
	//d.Dump();
	
	return h*d;
}

public static async Task<int> Part2()
{
	var input = await GetPuzzleInputAsync(DayNumber);
	var h = 0;
	var d = 0;
	var a = 0;
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
	for (int i = 0; i < inputList.Count(); i++)
	{
		var line = inputList[i];
		//line.Dump();
		var command = Regex.Match(line, @"\w+").Value;
		var unit = int.Parse(Regex.Match(line, @"\d+").Value);

		switch (command)
		{
			case "forward":
				h += unit;
				d += a * unit;
				break;
			case "back":
				h -= unit;
				break;
			case "up":
				a -= unit;
				break;
			case "down":
				a += unit;
				break;
		}
	}
	//h.Dump();
	//d.Dump();
	//a.Dump();
	return h * d;
}