<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static async Task Main()
{
	var input = await File.ReadAllTextAsync(@"C:\code\AdventOfCode\Day6\test.txt");
	List<string> groupList = input.Replace("\r", "").Split("\n\n").ToList();
	int uniqueCount = 0;
	foreach(var group in groupList)
	{
		var hashset = new HashSet<char>();
		var groupCharArray = group.Replace("\n","").ToCharArray();
		foreach(var g in groupCharArray)
		{
			hashset.Add(g);
		}
		uniqueCount += hashset.Count();
	}
	uniqueCount.Dump();
}
