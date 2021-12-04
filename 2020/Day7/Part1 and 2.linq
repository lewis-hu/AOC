<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static async Task Main()
{
	var input = await File.ReadAllTextAsync(@"C:\code\AdventOfCode\Day7\test.txt");
	List<string> bagRuleList = input.Replace("\r", "").Split("\n").ToList();
	var bags = bagRuleList.Select(l => new Bag(l)).ToList();

	//Bag.GetBag("shiny gold", bags).Dump();
	//Bag.GetBag("shiny gold", bags).Content.Dump();
	bags.Count(b => b.ContainsBag("shiny gold", bags)).Dump();
	
	Bag.GetBag("shiny gold", bags).CalculateBags(bags).Dump();
	
	
	//bags.Dump();
}
// You can define other methods, fields, classes and namespaces here
public class Bag
{
	private static Regex bagColour = new Regex(@"^[^\s]+\s+[^\s]+", RegexOptions.Compiled);
	private static Regex bagContent = new Regex(@"(\d+) ([^\s]+\s+[^\s]+)", RegexOptions.Compiled);
	public string Name;
	public IDictionary<string, int> Content;

	public Bag(string line)
	{
		//bagColour.Match(line).Dump();
		Name = bagColour.Match(line).Value;
		//bagContent.Matches(line).Dump();
		Content = bagContent.Matches(line)
			.ToDictionary(
				m => m.Groups[2].Value,
				m => int.Parse(m.Groups[1].Value)
			);
		//Content.Dump();
	}

	public static Bag GetBag(string key, List<Bag> allBags)
	{
		return allBags.First(b => b.Name.Equals(key));
	}

	public bool ContainsBag(string name, List<Bag> allBags)
	{
		//Content.Keys.Dump();
		return Content.Keys.Any(key => key.Equals(name) || GetBag(key, allBags).ContainsBag(name, allBags));
	}

	public long CalculateBags(List<Bag> allBags)
	{
		Content.Dump(Name);
		return Content.Sum(kv =>
					kv.Value * 
					(1 + GetBag(kv.Key, allBags).CalculateBags(allBags)
				)
			);
	}
}