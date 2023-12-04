using Advent_Of_Code_Solver;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

		public async void OnBrowseClick(object sender, RoutedEventArgs e)
		{
			FilePickerOpenOptions options = new FilePickerOpenOptions()
			{
				Title = "Select input file",
				AllowMultiple = false,
				FileTypeFilter = new FilePickerFileType[] { FilePickerFileTypes.TextPlain }
			};
			Task<IReadOnlyList<IStorageFile>> fileTask = StorageProvider.OpenFilePickerAsync(options);
			await fileTask;

			if (fileTask.Result.Count == 0)
			{
				return;
			}

			IStorageFile file = fileTask.Result[0];

			string[] allLines = File.ReadAllLines(file.Path.AbsolutePath);

			browseTextBox.Text = file.Path.AbsolutePath;

			int day = (int)dayComboBox.SelectedItem;
			int puzzle = (int)puzzleComboBox.SelectedItem;

			string output = m_year.Solve(day, puzzle, allLines);

			if (output == null)
			{
				outputTextBox.Text = "INVALID INPUT OR PUZZLE!";
			}
			else
			{
				outputTextBox.Text = output;
			}
		}
	}
}
