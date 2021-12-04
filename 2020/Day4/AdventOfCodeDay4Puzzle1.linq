<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static async Task Main()
{
	var validFields = "byr,iyr,eyr,hgt,hcl,ecl,pid,cid".Split(",").ToList();
	var input = await File.ReadAllTextAsync(@"C:\code\AdventOfCode\Day4\input.txt");
	List<string> inputList = input.Replace("\r", "").Split("\n\n").ToList();
	//inputList.Dump();
	var rows = inputList.Count();
	var validPassportCount = 0;
	foreach (var passport in inputList)
	{
		var isValid = true;
		var passportFields = passport.Replace("\n"," ").Split(" ").ToList();
		foreach (var field in validFields)
		{
			if(field == "cid")
			{
				continue;
			}
			if (!passport.Contains(field))
			{
				isValid = false;
				break;
			}
		}
		if (!isValid)
		{
			//passport.Dump();
			continue;
		}
		foreach (var passportField in passportFields)
		{
			var validate = passportField.Split(":");
			var field = validate[0];
			var value = validate[1];
			switch (field)
			{
				case "byr":
					if (int.Parse(value) < 1920 || int.Parse(value) > 2002) isValid = false;
					//isValid.Dump("byr");
					break;
				case "iyr":
					if (int.Parse(value) < 2010 || int.Parse(value) > 2020) isValid = false;
					//isValid.Dump("iyr");
					break;
				case "eyr":
					if (int.Parse(value) < 2020 || int.Parse(value) > 2030) isValid = false;
					//isValid.Dump("eyr");
					break;
				case "hgt":
					string numericHeight = new String(value.Where(Char.IsDigit).ToArray());
					if (value.Substring(value.Length - 2, 2) != "cm" && value.Substring(value.Length - 2, 2) != "in")
					{
						isValid = false;
						break;
					}
					if (value.Substring(value.Length - 2, 2) == "cm")
					{
						//int.Parse(numericHeight).Dump("int.Parse(numericHeight)");
						if (int.Parse(numericHeight) < 150 || int.Parse(numericHeight) > 193) isValid = false;
					}
					if (value.Substring(value.Length - 2, 2) == "in")
					{
						if (int.Parse(numericHeight) < 59 || int.Parse(numericHeight) > 76) isValid = false;
					}
					//isValid.Dump("hgt");
					break;
				case "hcl":
					if (!Regex.Match(value, @"^#(?:[0-9a-fA-F]{3}){1,2}$").Success) isValid = false;
					//isValid.Dump("hcl");
					break;
				case "ecl":
					var validEyeColours = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
					if (!validEyeColours.Contains(value)) isValid = false;
					//isValid.Dump("ecl");
					break;
				case "pid":
					if (!Regex.Match(value, @"^[0-9]{9}$").Success) isValid = false;
					//isValid.Dump("pid");
					break;
				case "cid":
					break;
			}
			if(!isValid) break;
		}
		if(isValid) validPassportCount++;
	}
	validPassportCount.Dump();
}
