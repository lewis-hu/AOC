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
	var asmOps = asmList.SelectMany(l => asmRegex.Matches(l).ToDictionary(r => r.Groups[1].Value, r=> int.Parse(r.Groups[2].Value))).ToList();
	//asmOps.Dump();
	var accumulator = 0;
	var position = 0;
	var visitedPosition = new List<int>();
	var done = false;
	while(!done && position < asmOps.Count() - 1)
	{
		if(visitedPosition.Contains(position)) {
			done = true;
			break;
		}
		visitedPosition.Add(position);
		var op = asmOps[position];
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
		if(op.Key != "jmp")
		{
			position++;
		}
	}
	accumulator.Dump();
	position.Dump();
	
}
// You can define other methods, fields, classes and namespaces here
//public static void run_program(int counter, List<KeyValuePair<string, int>>pcode)
//{
//	const int max_no_instructions = 100
//	counter = 0;
//	for (int i = 0; i < max_no_instructions; i++)
//	{
//		eval(pcode)
//        counter += 1
//        if (counter > pcode.Count())
//		{
//			"Out of bounds".Dump();
//			break;
//		}
//	}
//end
//}
