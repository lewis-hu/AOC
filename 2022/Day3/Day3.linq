<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "../../shared.linq"
const bool IS_TESTING = true;
const int DayNumber = 2;

static async Task Main()
{
	await Part1().Dump("Part1");
	await Part2().Dump("Part2");
}

public static async Task<long> Part1()
{
	var score = 0;
	var input = await GetPuzzleInputAsync(DayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	var combinationScore =
		new Dictionary<(char Opponent, char Me), int>
	{
			{('A','X'), 3},
			{('A','Y'), 6},
			{('A','Z'), 0},
			{('B','X'), 0},
			{('B','Y'), 3},
			{('B','Z'), 6},
			{('C','X'), 6},
			{('C','Y'), 0},
			{('C','Z'), 3}
	};
	var shapeScore = new Dictionary<char, int> {
		{'X', 1},
		{'Y', 2},
		{'Z', 3}
	};
	foreach (var row in inputList)
	{
		if (string.IsNullOrEmpty(row))
		{
			continue;
		}
		(char Opponent, char Me) round = (row[0], row[2]);
		if (combinationScore.ContainsKey(round))
		{
			score += combinationScore[round] + shapeScore[round.Me];
		}
	}
	return score;
}
public static async Task<long> Part2()
{
	var score = 0;
	var input = await GetPuzzleInputAsync(DayNumber, IS_TESTING);
	List<string> inputList = input.Replace("\r", "").Split("\n").ToList();
	var combinationScore =
		new Dictionary<(char Opponent, char Me), int>
	{
			{('A','X'), 3},
			{('A','Y'), 1},
			{('A','Z'), 2},
			{('B','X'), 1},
			{('B','Y'), 2},
			{('B','Z'), 3},
			{('C','X'), 2},
			{('C','Y'), 3},
			{('C','Z'), 1}
	};
	var shapeScore = new Dictionary<char, int> {
		{'X', 0},
		{'Y', 3},
		{'Z', 6}
	};
	foreach (var row in inputList)
	{
		if (string.IsNullOrEmpty(row))
		{
			continue;
		}
		(char Opponent, char Me) round = (row[0], row[2]);
		if (combinationScore.ContainsKey(round))
		{
			score += combinationScore[round] + shapeScore[round.Me];
		}
	}
	return score;
}

