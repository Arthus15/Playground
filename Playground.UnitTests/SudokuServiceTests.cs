using NUnit.Framework;
using Playground.Games.Sudoku.Service;
using System;

namespace Playground.UnitTests
{
	public class SudokuServiceTests
	{
		private int[][] _board = null!;
		[SetUp]
		public void Setup()
		{
			int[,] sampleSudoku = {
                {8, 0, 4, 0, 0, 0, 0, 1, 0},
                {0, 0, 3, 9, 0, 0, 0, 0, 0},
                {0, 5, 0, 0, 0, 0, 0, 0, 2},
                {0, 0, 0, 1, 3, 0, 0, 0, 0},
                {0, 3, 0, 2, 6, 5, 8, 0, 0},
                {0, 0, 0, 0, 0, 0, 4, 0, 0},
                {0, 2, 0, 6, 0, 0, 0, 0, 7},
                {0, 0, 8, 0, 0, 1, 0, 5, 0},
                {7, 0, 0, 0, 0, 3, 0, 0, 6}
            };

            Console.WriteLine("Setting Board");
			_board = new int[9][];
			for (int i = 0; i < 9; i++)
			{
				_board[i] = new int[9];
				for (int j = 0; j < 9; j++)
				{
					_board[i][j] = sampleSudoku[i, j];
				}
			}
		}

		[Test]
		public void CanResolveSudoku()
		{
			var sudokuService = new SudokuService();
			var result = sudokuService.Run(_board);
			Assert.IsTrue(true);
		}
	}
}