<Query Kind="Program">
  <NuGetReference>Microsoft.Extensions.Configuration.Json</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Microsoft.Extensions.Configuration</Namespace>
</Query>

public static int AOC_YEAR { get; set; }

private static string GetSessionCookie()
{
	var builder = new ConfigurationBuilder()
.AddJsonFile(Path.Join(Path.GetDirectoryName(Util.CurrentQueryPath), $@"../..","appsettings.json.local"));
	IConfiguration config = builder.Build();
	return config.GetSection("cookie").Value;
}

public static int GetDayNumberUsingFileName() 
{
	return int.Parse(Path.GetFileNameWithoutExtension(Util.CurrentQueryPath).Last().ToString());
}

public static async Task<string> GetPuzzleInputAsync(int dayNumber, bool isTesting = false)
{
	string sessionCookie = GetSessionCookie();
	if(isTesting)
	{
		return await GetTestInputAsync(dayNumber);
	}
	var folderPath = GetFolderPath(dayNumber);
	Directory.CreateDirectory(folderPath);
	var filePath = $@"{folderPath}\input.txt";
	if (File.Exists(filePath))
	{
		return await File.ReadAllTextAsync(filePath);
	}

	// Initialization.  
	List<string> puzzleInput = new List<string>();
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
		if(AOC_YEAR == default(int))
		{
			AOC_YEAR = DateTime.Now.Year;
		}

		// HTTP GET
		response = await client.GetAsync($"{AOC_YEAR}/day/{dayNumber}/input");

		// Verification  
		if (response.IsSuccessStatusCode)
		{
			// Reading Response.  
			result = await response.Content.ReadAsStringAsync();
			result.Replace("\r", "").Replace("\n\n","\n");
			System.IO.File.WriteAllText(filePath, result);
		}
		return result;
	}
}
private static string GetFolderPath(int dayNumber) 
{
	return Path.Join(Path.GetDirectoryName(Util.CurrentQueryPath));
}
public static async Task<string> GetTestInputAsync(int dayNumber)
{
	var folderPath = GetFolderPath(dayNumber);
	Directory.CreateDirectory(folderPath);
	var filePath = $@"{folderPath}\test.txt";
	if (File.Exists(filePath))
	{
		return await File.ReadAllTextAsync(filePath);
	}
	return string.Empty;

}

