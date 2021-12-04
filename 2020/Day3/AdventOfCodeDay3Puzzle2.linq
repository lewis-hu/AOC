<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static async Task Main()
{
	var input = File.ReadAllText(@"C:\code\AdventOfCode\Day3\input.txt");
	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	var treeCount = 0;

	treeCount = GetTreeCount(inputList, 1, 1);
	treeCount *= GetTreeCount(inputList, 3, 1);
	treeCount *= GetTreeCount(inputList, 5, 1);
	treeCount *= GetTreeCount(inputList, 7, 1);
	treeCount *= GetTreeCount(inputList, 1, 2);

	treeCount.Dump();

}

public static int GetTreeCount(List<string> inputList, int right, int down) 
{
	var col = 0;
	var treeCount = 0;
	for (int row = down; row < inputList.Count; row += down)
	{
		col += right;
		if (inputList[row][col % 31] == '#')
		{
			treeCount++;
		}
	}
	return treeCount;
}

// You can define other methods, fields, classes and namespaces here
public static async Task<string> GetPuzzleInputAsync()
{
	// Initialization.  
	List<string> puzzleInput = new List<string>();
	const string sessionCookie = "_ga=GA1.2.918020773.1606803859; _gid=GA1.2.1131951502.1606803859; ru=53616c7465645f5f40ba35026abee564eec17100e741144c4fd294b2d652f987e0e66a05f3d536ffbb893d36ebc954d3; session=53616c7465645f5f30362387c56a05404f21b59f522335f1fbf01d028a480a5a68be90f0f00b4cd064e548429d869b7b; _gat=1";
	// HTTP GET.  
	string result = string.Empty;
	using (var client = new HttpClient())
	{
		// Setting Base address.  
		client.BaseAddress = new Uri("https://adventofcode.com/");

		// Setting content type.  
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
		client.DefaultRequestHeaders.Add("Cookie", sessionCookie);

		// Initialization.  
		HttpResponseMessage response = new HttpResponseMessage();

		// HTTP GET  
		response = await client.GetAsync("2020/day/3/input");

		// Verification  
		if (response.IsSuccessStatusCode)
		{
			// Reading Response.  
			
			result = await response.Content.ReadAsStringAsync();
			
		}
	}
	return result;
}

public static async Task<string> GetPuzzleInputOffline()
{
	return await Task.FromResult(System.IO.File.ReadAllText(@"C:\code\AdventOfCode\Day3\input.txt"));
}