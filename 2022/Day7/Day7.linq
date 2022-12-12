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
record ElfItem(string Name);
record ElfFolder(string Name, ElfFolder Parent, Dictionary<string, ElfItem>Items) : ElfItem(Name);
record ElfFile(string Name, long Size) : ElfItem(Name);

static bool ParseTerminalOutput(ElfFolder rootFolder,List<string> termOutput) 
{
	var currentDirectory = rootFolder;
	foreach (var line in termOutput)
	{
		switch(line)
		{
			case "$ cd /":
				currentDirectory = rootFolder;
				break;
			case "$ cd ..":
				currentDirectory = currentDirectory.Parent;
				break;
			case string l when l.StartsWith("$ cd "):
				var folderName = l.Split(" ").Last();
				currentDirectory = (ElfFolder)currentDirectory.Items[folderName];
				break;
			case string l when l.StartsWith("dir "):
				var subFolderName = l.Split(" ").Last();
				currentDirectory.Items[subFolderName] = new ElfFolder(subFolderName,currentDirectory,new Dictionary<string, ElfItem>());
				break;
			case string l when char.IsNumber(l[0]):
				var match = Regex.Match(l, @"(\d+) (\w+.?\w*)");
				if(match.Success) 
				{
					var size = long.Parse(match.Groups[1].Value);
					var name = match.Groups[2].Value;
					currentDirectory.Items[name]=new ElfFile(name,size);
				}
				break;
			default:
				break;
		}
	}
	return true;
}

static long GetFolderSizes(ElfFolder dir, Dictionary<ElfFolder, long> sizes)
{
	long size = 0;
	foreach (var sub in dir.Items)
	{
		if (sub.Value is ElfFile f)
		{
			size += f.Size;
		}
		else if (sub.Value is ElfFolder d)
		{
			GetFolderSizes(d, sizes);
			size += sizes[d];
		}
	}
	sizes[dir] = size;
	return size;
}

public static async Task<long> Part1(int dayNumber)
{
	var input = await GetPuzzleInputAsync(dayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();

	var driveContent = new ElfFolder("/", null, new Dictionary<string,ElfItem>());
	ParseTerminalOutput(driveContent, inputList);
	var folderSizes = new Dictionary<ElfFolder, long>();
	GetFolderSizes(driveContent, folderSizes);
	return folderSizes.Values.Where(s=>s <= 100_000).Sum();
}

public static async Task<long> Part2(int dayNumber)
{
	var input = await GetPuzzleInputAsync(dayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();

	var driveContent = new ElfFolder("/", null, new Dictionary<string, ElfItem>());
	ParseTerminalOutput(driveContent, inputList);
	var folderSizes = new Dictionary<ElfFolder, long>();
	GetFolderSizes(driveContent, folderSizes);
	return folderSizes.Values.Where(s => s >= folderSizes[driveContent] - 40_000_000).Min();
}