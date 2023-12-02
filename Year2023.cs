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
			new IYear.DayDefinition(1, true, false)
		};

		public string Solve(int day, int puzzle, string[] input)
		{
			if (puzzle < 1 || puzzle > 2)
				return null;

			switch (day)
			{
				case 1:
					return SolveDay1(puzzle, input);
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
			else
			{
				return null;
			}
			
		}
	}
}
