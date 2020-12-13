<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static async Task Main()
{
	var input = await File.ReadAllTextAsync(@"C:\code\AdventOfCode\Day8\input.txt");
	List<string> asmList = input.Replace("\r", "").Split("\n").ToList();
	var asmRegex = new Regex(@"(^[^\s]+)\s([+-][\d]+)", RegexOptions.Compiled);
	var asmOps = asmList.SelectMany(l => asmRegex.Matches(l).ToDictionary(r => r.Groups[1].Value, r => int.Parse(r.Groups[2].Value))).ToList();
	var hasCrashed = false;
	int? corruptedPosition = null;
	for (int i = 0; i < asmOps.Count(); i++)
	{
		if (!runProgram(asmOps, i))
		{
			corruptedPosition = i;//.Dump("HERE!!!!!!!!!!!!!");
			break;
		}
	}
}

public static bool runProgram(List<KeyValuePair<string,int>> asmOps, int? lineToSwap = null)
{
	var accumulator = 0;
	var position = 0;
	var visitedPosition = new List<int>();
	var crashed = false;
	while (!crashed && position < asmOps.Count() - 1)
	{
		if (visitedPosition.Contains(position))
		{
			crashed = true;
			break;
		}
		visitedPosition.Add(position);
		var op = asmOps[position];
		if (lineToSwap == position)
		{
			switch (op.Key)
			{
				case "acc":
					accumulator += op.Value;
					break;
				case "jmp":
					break;
				case "nop":
					position += op.Value;
					break;
			}
			if (op.Key != "nop")
			{
				position++;
			}
		}
		else
		{
			switch (op.Key)
			{
				case "acc":
					accumulator += op.Value;
					break;
				case "jmp":
					position += op.Value;
					break;
				case "nop":
					break;
			}
			if (op.Key != "jmp")
			{
				position++;
			}
		}

	}
	if (!crashed)
	{
		accumulator.Dump("accumulator");
		position.Dump("Position");
	}
	return crashed;
}