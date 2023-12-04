using Advent_Of_Code_Solver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeSolver
{
	internal class Year2023 : IYear
	{
		public List<IYear.DayDefinition> DayDefinitions { get; private set; } = new List<IYear.DayDefinition>()
		{
			new IYear.DayDefinition(1, true, true),
			new IYear.DayDefinition(2, true, false)
		};

		public string Solve(int day, int puzzle, string[] input)
		{
			if (puzzle < 1 || puzzle > 2)
				return null;

			switch (day)
			{
				case 1:
					return SolveDay1(puzzle, input);
				case 2:
					return SolveDay2(puzzle, input);
				default:
					return null;
			}
		}

		private string SolveDay1(int puzzle, string[] input)
		{
			if (puzzle == 1)
			{
				int total = 0;

				foreach (string line in input)
				{
					int.TryParse(line.First(IsDigit).ToString(), out int firstDigit);
					int.TryParse(line.Last(IsDigit).ToString(), out int lastDigit);
					int joined = (firstDigit * 10) + lastDigit;

					Trace.WriteLine(joined);

					total += joined;
				}

				return total.ToString();

				bool IsDigit(char c)
				{
					return int.TryParse(c.ToString(), out int _);
				}
			}
			else if (puzzle == 2)
			{
				string[] digits = new string[]
				{
					"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
				};

				int total = 0;

				foreach (string line in input)
				{
					int firstDigit = FirstDigit(line);
					int lastDigit = LastDigit(line);
					int joined = (firstDigit * 10) + lastDigit;

					total += joined;
				}

				return total.ToString();

				int FirstDigit(string line)
				{
					int lowestIndex = line.Length - 1;
					int firstDigit = 0;

					for (int i = 0; i < 10; i++)
					{
						int intIndex = line.IndexOf(i.ToString());

						if (intIndex >= 0 && intIndex <= lowestIndex)
						{
							lowestIndex = intIndex;
							firstDigit = i;
						}

						if (i >= digits.Length)
							continue;

						int stringindex = line.IndexOf(digits[i]);

						if (stringindex >= 0 && stringindex <= lowestIndex)
						{
							lowestIndex = stringindex;
							firstDigit = i + 1;
						}
					}

					return firstDigit;
				}

				int LastDigit(string line)
				{
					int highestIndex = 0;
					int lastDigit = 0;

					for (int i = 0; i < 10; i++)
					{
						int intIndex = line.LastIndexOf(i.ToString());

						if (intIndex >= 0 && intIndex >= highestIndex)
						{
							highestIndex = intIndex;
							lastDigit = i;
						}

						if (i >= digits.Length)
							continue;

						int stringindex = line.LastIndexOf(digits[i]);

						if (stringindex >= 0 && stringindex >= highestIndex)
						{
							highestIndex = stringindex;
							lastDigit = i + 1;
						}
					}

					return lastDigit;
				}
			}
			else
			{
				return null;
			}
		}

		private string SolveDay2(int puzzle, string[] input)
		{
			Dictionary<string, int> bagDefinition = new Dictionary<string, int>()
			{
				{ "red", 12 },
				{ "green", 13 },
				{ "blue", 14 }
			};

			int idSum = 0;

			foreach (string line in input)
			{
				bool possible = true;

				int colonIndex = line.IndexOf(':');
				string id = line.Substring(0, colonIndex);
				string list = line.Substring(colonIndex + 2);
				string[] sets = list.Split("; ");

				foreach (string set in sets)
				{
					string[] draws = set.Split(", ");
					
					foreach (string draw in draws)
					{
						int spaceIndex = draw.IndexOf(" ");
						string amountString = draw.Substring(0, spaceIndex);
						int.TryParse(amountString, out int amount);
						string color = draw.Substring(spaceIndex + 1);

						if (amount > bagDefinition[color])
						{
							possible = false;
							break;
						}
					}

					if (!possible)
						break;
				}

				if (!possible)
					continue;

				id = id.Substring(5);
				int.TryParse(id, out int idNum);

				idSum += idNum;
			}

			return idSum.ToString();
		}
	}
}
