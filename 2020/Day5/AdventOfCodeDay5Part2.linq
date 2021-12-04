<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static async Task Main()
{
	var input = await File.ReadAllTextAsync(@"C:\code\AdventOfCode\Day5\input.txt");
	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	//inputList.Dump();
	List<int> seatIdList = new List<int>();
	foreach(var boardingPass in inputList)
	{
		var rowRange = Enumerable.Range(0, 128);
		//rowRange.Dump();
		for (int i = 0; i < 7; i++)
		{
			if (rowRange.Count() <= 1) break;
			rowRange = boardingPass[i] == 'F' ? rowRange.Take((rowRange.Count()) / 2) : rowRange.Skip((rowRange.Count()) / 2);
			//rowRange.Dump(rowRange.Count().ToString());
		}
		var colRange = Enumerable.Range(0, 8);
		for (int j = 7; j < 10; j++)
		{
			if (colRange.Count() <= 1) break;
			colRange = boardingPass[j] == 'L' ? colRange.Take((colRange.Count()) / 2) : colRange.Skip((colRange.Count()) / 2);
		}
		seatIdList.Add(rowRange.First() * 8 + colRange.First());
	}
	seatIdList.Sort();
	for (int i = 1; i < seatIdList.Count() - 1; i++)
	{
		if(seatIdList[i] - seatIdList[i-1] > 1) {
			seatIdList[i].Dump();
			seatIdList[i-1].Dump();
		}
	}
}
