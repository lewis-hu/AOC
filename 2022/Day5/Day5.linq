<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../../shared.linq"
const bool IS_TESTING = false;

static async Task Main()
{
	int dayNumber = GetDayNumberUsingFileName();
	await Part1(dayNumber).Dump("Part1");
	await Part2(dayNumber).Dump("Part2");
}
public static (Dictionary<int, Stack<string>>, int) InitialiseStacks(List<string> inputList)
{
	var stackLabelLine = inputList
						.Select((input, row) => new { input, row })
						.Where(i => i.input.Except(" ")
						.All(c => int.TryParse(c.ToString(), out int num)))
						.First();
	var stackLabelPos = stackLabelLine.input
						.Select((c, col) => c != ' '
											? new { label = int.Parse(c.ToString()), colPos = col }
											: new { label = 0, colPos = 0 })
						.Where(i => i.colPos != 0).ToList();
	var stackDictionary = new Dictionary<int, Stack<string>>();
	stackLabelPos.ForEach(i =>
	{
		stackDictionary.Add(i.label, new Stack<string>());
	}
	);

	for (var stackItemRow = stackLabelLine.row - 1; stackItemRow >= 0; stackItemRow--)
	{
		foreach (var i in stackLabelPos)
		{
			var item = inputList[stackItemRow][i.colPos].ToString();
			if (!string.IsNullOrWhiteSpace(item))
			{
				stackDictionary[i.label].Push(item);
			}
		}
	}
	return (stackDictionary, stackLabelLine.row);
}
public static async Task<string> Part1(int dayNumber)
{
	var input = await GetPuzzleInputAsync(dayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
	//inputList.Dump();
	//get stack label line
	(Dictionary<int, Stack<string>> stacks,int labelRow) = InitialiseStacks(inputList);
	for(var i = labelRow + 1; i<inputList.Count(); i++)
	{
		var move = inputList[i];
		MatchCollection matches = Regex.Matches(move, @"\d+");
		var numToMove = int.Parse(matches[0].Value);
		var fromStack = int.Parse(matches[1].Value);
		var toStack = int.Parse(matches[2].Value);
		for(var j=0;j<numToMove;j++)
		{
			stacks[toStack].Push(stacks[fromStack].Pop());
		}
	}
	return string.Join("",stacks.Values.Select(s=> s.Peek()));
}

public static async Task<string> Part2(int dayNumber)
{
	var input = await GetPuzzleInputAsync(dayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
	//inputList.Dump();
	//get stack label line
	(Dictionary<int, Stack<string>> stacks, int labelRow) = InitialiseStacks(inputList);
	for (var i = labelRow + 1; i < inputList.Count(); i++)
	{
		var move = inputList[i];
		MatchCollection matches = Regex.Matches(move, @"\d+");
		var numToMove = int.Parse(matches[0].Value);
		var fromStack = int.Parse(matches[1].Value);
		var toStack = int.Parse(matches[2].Value);
		var tempStack = new Stack<string>();
		for (var j = 0; j < numToMove; j++)
		{
			tempStack.Push(stacks[fromStack].Pop());
		}
		for (var j = 0; j < numToMove; j++)
		{
			stacks[toStack].Push(tempStack.Pop());
		}
	}
	return string.Join("", stacks.Values.Select(s => s.Peek()));
}

