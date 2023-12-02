using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_Solver
{
	internal interface IYear
	{
		public struct DayDefinition
		{
			public int Number;
			public bool Puzzle1;
			public bool Puzzle2;

			public DayDefinition(int num, bool puz1,  bool puz2)
			{
				Number = num;
				Puzzle1 = puz1;
				Puzzle2 = puz2;
			}
		}

		public List<DayDefinition> DayDefinitions { get; }

		public string Solve(int day, int puzzle, string[] input);
	}
}
