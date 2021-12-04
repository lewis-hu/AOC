<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static async Task Main()
{
	var input = await File.ReadAllTextAsync(@"C:\code\AdventOfCode\Day6\input.txt");
	List<string> groupList = input.Replace("\r", "").Split("\n\n").ToList();
	int uniqueCount = 0;
	foreach(var group in groupList)
	{
		var subGroup =group.Split("\n");
		var commons = GetCommonItems(subGroup);
		uniqueCount += commons.Count();
	}
	uniqueCount.Dump();
}
static IEnumerable<T> GetCommonItems<T>(IEnumerable<T>[] lists)
{
	HashSet<T> hs = new HashSet<T>(lists.First());
	for (int i = 1; i < lists.Length; i++)
		hs.IntersectWith(lists[i]);
	return hs;
}