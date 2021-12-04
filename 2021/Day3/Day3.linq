<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../../shared.linq"

public const int DayNumber = 3;
static async Task Main()
{
	await Part1().Dump("Part1");
	await Part2().Dump("Part2");
}

public static async Task<int> Part1()
{
	var input = await GetPuzzleInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
	//inputList.Dump();
	
	var rows = inputList.Count();
	var cols = inputList.FirstOrDefault().Length;
	var isZeroForColumn = new List<bool>();
	var gamma = new Dictionary<int, int>();
	var epsilon = new Dictionary<int, int>();
	for (int col = 0; col < cols; col++)
	{
		var colSum = inputList.Sum(l => int.Parse(l[col].ToString()));
		gamma[col] = colSum > rows / 2.0 ? 1 : 0;
		epsilon[col] = colSum > rows / 2.0 ? 0 : 1;
	}
	var gammaBinary = string.Join("", gamma.Values);
	var epsilonBinary = string.Join("", epsilon.Values);
	var g = Convert.ToInt32(gammaBinary, 2);
	var e  = Convert.ToInt32(epsilonBinary, 2);
	return g * e;
}

public static async Task<int> Part2()
{
	var input = await GetPuzzleInputAsync(DayNumber);
	List<string> inputList = input
							.Replace("\r", "").Split("\n")
							.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
	
	var rows = inputList.Count();
	var cols = inputList.FirstOrDefault().Length;
	var isZeroForColumn = new List<bool>();
	var oxygen = new Dictionary<int, int>();
	var co2 = new Dictionary<int, int>();
	var oxygenString = GetOxygenRating(inputList);
	var oxygenBinary = string.Join("", oxygenString);
	var oxygenRating = Convert.ToInt32(oxygenBinary, 2);
	var CO2String = GetCO2Rating(inputList);
	var CO2Binary = string.Join("", CO2String);
	var CO2Rating = Convert.ToInt32(CO2Binary, 2);
	return oxygenRating * CO2Rating;
}

public static string GetOxygenRating(IList<string> inputList, int colPosition = 0)
{
	var rows = inputList.Count();
	var cols = inputList.FirstOrDefault().Length;
	if(rows <= 1 || colPosition > cols - 1) return inputList.First();
	var result = new List<string>();

	var colSum = inputList.Sum(l => int.Parse(l[colPosition].ToString()));
	if (colSum >= rows / 2.0 || rows <= 2)
	{
		result.AddRange(inputList.Where(l => int.Parse(l[colPosition].ToString()) == 1));
	}
	else
	{
		result.AddRange(inputList.Where(l => int.Parse(l[colPosition].ToString()) == 0));
	}
	return GetOxygenRating(result, ++colPosition);
}
public static string GetCO2Rating(IList<string> inputList, int colPosition = 0)
{
	//inputList.Count().Dump("InputList Count");
	//colPosition.Dump("ColPosition");
	var rows = inputList.Count();
	var cols = inputList.FirstOrDefault().Length;
	if (rows <= 1 || colPosition > cols - 1) return inputList.First();
	var result = new List<string>();

	var colSum = inputList.Sum(l => int.Parse(l[colPosition].ToString()));
	//colSum.Dump("Number of 1s");
	if (colSum >= rows / 2.0 || rows <= 2)
	{
		//inputList.Where(l => int.Parse(l[colPosition].ToString()) == 0).Dump($"Picked zero at position {colPosition}");
		result.AddRange(inputList.Where(l => int.Parse(l[colPosition].ToString()) == 0));
	}
	else
	{
		//inputList.Where(l => int.Parse(l[colPosition].ToString()) == 1).Dump($"Picked one at position {colPosition}");
		result.AddRange(inputList.Where(l => int.Parse(l[colPosition].ToString()) == 1));
	}
	return GetCO2Rating(result, ++colPosition);
}