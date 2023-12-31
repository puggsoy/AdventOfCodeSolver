﻿using Advent_Of_Code_Solver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCodeSolver
{
	internal class Year2023 : IYear
	{
		public List<IYear.DayDefinition> DayDefinitions { get; private set; } = new List<IYear.DayDefinition>()
		{
			new IYear.DayDefinition(1, true, true),
			new IYear.DayDefinition(2, true, true),
			new IYear.DayDefinition(3, true, true),
			new IYear.DayDefinition(4, true, true),
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
				case 3:
					return SolveDay3(puzzle, input);
				case 4:
					return SolveDay4(puzzle, input);
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
			if (puzzle == 1)
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
			else if (puzzle == 2)
			{
				int powerSum = 0;

				foreach (string line in input)
				{
					Dictionary<string, int> minimums = new Dictionary<string, int>()
					{
						{ "red", 0 },
						{ "green", 0 },
						{ "blue", 0 }
					};

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

							if (amount > minimums[color])
							{
								minimums[color] = amount;
							}
						}
					}

					int power = minimums["red"] * minimums["green"] * minimums["blue"];

					powerSum += power;
				}

				return powerSum.ToString();
			}
			else
			{
				return null;
			}
		}

		private string SolveDay3(int puzzle, string[] input)
		{
			if (puzzle == 1)
			{
				int partSum = 0;

				int lineLength = input[0].Length;

				for (int i = 0; i < input.Length; i++)
				{
					int partNumEnd = 0;
					int partNumStart = IndexOfPredicate(input[i], partNumEnd, IsDigit);

					while (partNumStart >= 0)
					{
						partNumEnd = IndexOfPredicate(input[i], partNumStart,  c => !IsDigit(c));
						if (partNumEnd < 0)
							partNumEnd = input[i].Length;

						string partNumString = input[i].Substring(partNumStart, partNumEnd - partNumStart);
						int.TryParse(partNumString, out int partNum);

						bool valid = false;

						if ((partNumStart > 0 && IsSymbol(input[i][partNumStart - 1])) || (partNumEnd < input[i].Length - 1 && IsSymbol(input[i][partNumEnd])))
						{
							valid = true;
						}

						if (i > 0 && !valid)
						{
							for (int k = partNumStart - 1; k < partNumEnd + 1; k++)
							{
								if (k < 0 || k > input[i - 1].Length - 1)
									continue;

								if (IsSymbol(input[i - 1][k]))
								{
									valid = true;

									break;
								}
							}
						}

						if (i < input.Length - 1 && !valid)
						{
							for (int k = partNumStart - 1; k < partNumEnd + 1; k++)
							{
								if (k < 0 || k > input[i + 1].Length - 1)
									continue;

								if (IsSymbol(input[i + 1][k]))
								{
									valid = true;

									break;
								}
							}
						}

						if (valid)
						{
							partSum += partNum;
						}

						partNumStart = IndexOfPredicate(input[i], partNumEnd, IsDigit);
					}
				}

				return partSum.ToString();
			}
			else if (puzzle == 2)
			{
				int ratioSum = 0;
				// Key is star coordinates, value is part numbers
				Dictionary<(int, int), List<int>> gears = new Dictionary<(int, int), List<int>>();

				int lineLength = input[0].Length;

				for (int i = 0; i < input.Length; i++)
				{
					int partNumEnd = 0;
					int partNumStart = IndexOfPredicate(input[i], partNumEnd, IsDigit);

					while (partNumStart >= 0)
					{
						partNumEnd = IndexOfPredicate(input[i], partNumStart, c => !IsDigit(c));
						if (partNumEnd < 0)
							partNumEnd = input[i].Length;

						string partNumString = input[i].Substring(partNumStart, partNumEnd - partNumStart);
						int.TryParse(partNumString, out int partNum);

						bool valid = false;

						if (partNumStart > 0 && IsStar(input[i][partNumStart - 1]))
						{
							(int, int) key = (i, partNumStart - 1);

							if (!gears.ContainsKey((key)))
								gears[(key)] = new List<int>();

							gears[(key)].Add(partNum);
							valid = true;
						}
						else if (partNumEnd < input[i].Length - 1 && IsStar(input[i][partNumEnd]))
						{
							(int, int) key = (i, partNumEnd);

							if (!gears.ContainsKey((key)))
								gears[(key)] = new List<int>();

							gears[(key)].Add(partNum);
							valid = true;
						}

						if (i > 0 && !valid)
						{
							for (int k = partNumStart - 1; k < partNumEnd + 1; k++)
							{
								if (k < 0 || k > input[i - 1].Length - 1)
									continue;

								if (IsStar(input[i - 1][k]))
								{
									(int, int) key = (i - 1, k);

									if (!gears.ContainsKey((key)))
										gears[(key)] = new List<int>();

									gears[(key)].Add(partNum);
									valid = true;

									break;
								}
							}
						}

						if (i < input.Length - 1 && !valid)
						{
							for (int k = partNumStart - 1; k < partNumEnd + 1; k++)
							{
								if (k < 0 || k > input[i + 1].Length - 1)
									continue;

								if (IsStar(input[i + 1][k]))
								{
									(int, int) key = (i + 1, k);

									if (!gears.ContainsKey((key)))
										gears[(key)] = new List<int>();

									gears[(key)].Add(partNum);
									valid = true;

									break;
								}
							}
						}

						partNumStart = IndexOfPredicate(input[i], partNumEnd, IsDigit);
					}
				}

				foreach (List<int> partNums in gears.Values)
				{
					if (partNums.Count < 2)
						continue;

					int ratio = partNums[0] * partNums[1];
					ratioSum += ratio;
				}

				return ratioSum.ToString();
			}
			else
			{
				return null;
			}

			bool IsDigit(char c)
			{
				return int.TryParse(c.ToString(), out int _);
			}

			bool IsSymbol(char c)
			{
				return !IsDigit(c) && c != '.';
			}

			bool IsStar(char c)
			{
				return c == '*';
			}
		}

		private string SolveDay4(int puzzle, string[] input)
		{
			if (puzzle == 1)
			{
				int pointTotal = 0;

				for (int i = 0; i < input.Length; i++)
				{
					string numbers = input[i].Split(':')[1];

					string[] sets = numbers.Split("|");

					string[] winning = Regex.Split(sets[0].Trim(), @"\s+");
					string[] have = Regex.Split(sets[1].Trim(), @"\s+");

					int matchNum = 0;

					for (int k = 0; k < winning.Length; k++)
					{
						if (have.Contains(winning[k]))
							matchNum++;
					}

					if (matchNum > 0)
					{
						pointTotal += (int)Math.Pow(2, matchNum - 1);
					}
				}

				return pointTotal.ToString();
			}
			else if (puzzle == 2)
			{
				int cardTotal = 0;

				List<int> copies = new List<int>();

				for (int i = 0; i < input.Length; i++)
				{
					int copyNum = 0;

					if (copies.Count > 0)
					{
						copyNum = copies[0];
						copies.RemoveAt(0);
					}

					string numbers = input[i].Split(':')[1];

					string[] sets = numbers.Split("|");

					string[] winning = Regex.Split(sets[0].Trim(), @"\s+");
					string[] have = Regex.Split(sets[1].Trim(), @"\s+");

					int copyIndex = 0;

					for (int k = 0; k < winning.Length; k++)
					{
						if (have.Contains(winning[k]))
						{
							if (copyIndex == copies.Count)
								copies.Add(copyNum + 1);
							else
								copies[copyIndex] += copyNum + 1;

							copyIndex++;
						}
					}

					cardTotal += copyNum + 1;
				}

				return cardTotal.ToString();
			}
			else
			{
				return null;
			}
		}

		private int IndexOfPredicate(string input, int startingIndex, Predicate<char> predicate)
		{
			for (int i = startingIndex; i < input.Length; i++)
			{
				if (predicate(input[i]))
					return i;
			}

			return -1;
		}
	}
}
