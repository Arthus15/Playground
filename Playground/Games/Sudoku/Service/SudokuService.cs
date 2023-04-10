namespace Playground.Games.Sudoku.Service;

public class SudokuService : ISudokuService
{
	private int[][] _board = null!;
	private List<SudokuCell> _cells = new ();


	public int[][] Run(int[][] board)
	{
		_board = board;

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				if (_board[i][j] != 0)
				{
					continue;
				}

				_cells.Add(new SudokuCell(i, j, board));
			}
		}

		int it = 0;
		while (it < _cells.Count)
		{
			var cell = _cells[it];

			if (cell.TrySetNextValue(_board))
			{
				it++;
			}
			else
			{
				cell.Clean(_board);
				it--;
			}

			if (it == -1)
			{
				break;
			}
		}

		return _board;
	}
}