using Advent_Of_Code_Solver;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Diagnostics;

namespace AdventOfCodeSolver
{
	public partial class MainWindow : Window
	{
		private IYear m_year = null;

		public MainWindow()
		{
			InitializeComponent();

			yearComboBox.Items.Add(2023);
			yearComboBox.SelectedIndex = 0;
		}

		public void OnYearChanged(object sender, SelectionChangedEventArgs e)
		{
			switch (e.AddedItems[0])
			{
				case 2023:
					m_year = new Year2023();
					break;
				default:
					throw new NotImplementedException();
			}

			dayComboBox.Items.Clear();

			for (int i = 0; i < m_year.DayDefinitions.Count; i++)
			{
				dayComboBox.Items.Add(m_year.DayDefinitions[i].Number);
			}

			dayComboBox.SelectedIndex = 0;
		}

		public void OnDayChanged(object sender, SelectionChangedEventArgs e)
		{
			int day = (int)e.AddedItems[0];

			puzzleComboBox.Items.Clear();

			for (int i = 0; i < m_year.DayDefinitions.Count; i++)
			{
				if (m_year.DayDefinitions[i].Number == day)
				{
					if (m_year.DayDefinitions[i].Puzzle1)
					{
						puzzleComboBox.Items.Add(1);
					}

					if (m_year.DayDefinitions[i].Puzzle2)
					{
						puzzleComboBox.Items.Add(2);
					}
				}
			}

			puzzleComboBox.SelectedIndex = 0;
		}
	}
}

