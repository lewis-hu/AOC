<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../shared.linq"
static async Task Main()
{
	const int DayNumber = 9;
	
	const int preambleSize = 25;
	var input = await GetPuzzleInputAsync(DayNumber);

	//const int preambleSize = 5;
	//var input = await GetTestInputAsync(DayNumber);
	
	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	var inputListLong = inputList.Select(l => long.Parse(l)).ToList();

	var sumToCheck = 0L;
	for (int i = preambleSize; i < inputListLong.Count - preambleSize; i++)
	{
		//inputList[i].Dump("i");
		var preambleList = inputListLong.Skip(i - preambleSize).Take(preambleSize).ToList();
		var subSequence = GetSubSequence(preambleList);
		var result = PairExists(preambleList, inputListLong[i]);//.Dump(inputListLong[i].ToString());
		if (!result)
		{
			result.Dump(inputListLong[i].ToString());
			sumToCheck = inputListLong[i];
			break;
		}
		
	}
	for (int i = preambleSize; i < inputListLong.Count - preambleSize; i++)
	{
		//inputList[i].Dump("i");
		var preambleList = inputListLong.Skip(i - preambleSize).Take(preambleSize).ToList();
		var subSequence = GetSubSequence(preambleList);
		//subSequence.Select(s => s.Sum()).ToList().Dump();
		//subSequence.Dump();
		var exists = subSequence.Where(s => s.Sum() == sumToCheck).FirstOrDefault();
		//exists.Dump("exists");
		if(exists?.Count() > 0) 
		{
			var sum = exists.Min() + exists.Max();
			//preambleList.Dump();
			sum.Dump("Found");
		}
	}
}

//https://stackoverflow.com/a/25924449/1100988
public static bool PairExists(IEnumerable<long> arr, long sum)
{
	var set = new HashSet<long>();
	foreach (int elem in arr) set.Add(elem);
	foreach (int elem in set)
		if (set.Contains(sum - elem)) return true;
	return false;
}

//https://stackoverflow.com/a/56047248/1100988
public static IEnumerable<IEnumerable<T>> GetSubSequence<T>(List<T> list)
{
	return Enumerable.Range(0, list.Count).SelectMany(start => Enumerable.Range(1, list.Count-start).Select(count => list.GetRange(start, count)));
}

