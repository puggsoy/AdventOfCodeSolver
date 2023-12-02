using Advent_Of_Code_Solver;
using System;
using System.Collections.Generic;
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

		public string SolveDay1(int puzzle, string[] input)
		{
			char[] charArray = input[0].ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
	}
}
