﻿using Microsoft.AspNetCore.Components;

namespace Playground.Games.Sudoku
{
	public partial class Sudoku: ComponentBase
	{
		public int[][]? Board = null;

		public override Task SetParametersAsync(ParameterView parameters)
		{
			int[,] sampleSudoku = {
				{3, 0, 0, 6, 0, 1, 8, 4, 0},
				{0, 0, 0, 0, 0, 0, 0, 0, 3},
				{0, 0, 0, 0, 0, 3, 0, 7, 0},
				{6, 0, 0, 0, 0, 7, 0, 1, 0},
				{1, 0, 0, 0, 0, 0, 2, 0, 6},
				{0, 0, 8, 0, 0, 0, 0, 9, 0},
				{7, 0, 0, 0, 0, 9, 0, 3, 0},
				{0, 0, 9, 0, 0, 0, 6, 0, 5},
				{0, 0, 0, 0, 3, 0, 9, 0, 0}
			};

			Console.WriteLine("Setting Board");
			Board = new int[9][];
			for (int i = 0; i < 9; i++)
			{
				Board[i] = new int[9];
				for (int j = 0; j < 9; j++)
				{
					Board[i][j] = sampleSudoku[i, j];
				}
			}

			return base.SetParametersAsync(parameters);
		}

		public void Resolve()
		{
			Board = SudokuService.Run(Board!);
			StateHasChanged();
		}
	}
}
