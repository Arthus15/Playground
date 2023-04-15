namespace Playground.Games.Sudoku.Service;

public class SudokuCell
{
	public int X { get; set; }
	public int Y { get; set; }
	public Tuple<int, int> Quadrant { get; set; }
	private List<int> _usedValues { get; set; } = new();

	private static readonly int[] _posibleValues = Enumerable.Range(1, 9).ToArray();
	private static readonly Tuple<int, int>[] _quadrants =
	{
		new(2, 2), new(2, 5), new(2, 8), new(5, 2), new(5, 5), new(5, 8), new(8, 2), new(8, 5), new(8, 8)
	};

	public SudokuCell(int i, int j, int[][] board)
	{
		X = i;
		Y = j;
		Quadrant = GetQuadrant();
	}

	public bool TrySetNextValue(int[][] board)
	{
		var next = CalculateNext(board);
		if (next is not 0)
		{
			_usedValues.Add(next);
			board[X][Y] = next;

			return true;
		}

		return false;
	}

	public int CalculateNext(int[][] board)
	{
		var numbersInUseList = new List<int>();

		for (int qi = ((Math.Max(Quadrant.Item1 - 2, 0))); qi <= Quadrant.Item1; qi++)
		{
			for (int qj = ((Math.Max(Quadrant.Item2 - 2, 0))); qj <= Quadrant.Item2; qj++)
			{
				if (board[qi][qj] != 0)
				{
					numbersInUseList.Add(board[qi][qj]);
				}
			}
		}

		for (int it = 0; it < 9; it++)
		{
			if (board[it][Y] != 0)
			{
				numbersInUseList.Add(board[it][Y]);
			}

			if (board[X][it] != 0)
			{
				numbersInUseList.Add(board[X][it]);
			}
		}

		return _posibleValues.FirstOrDefault(x => !numbersInUseList.Contains(x) && !_usedValues.Contains(x));
	}

	public void Clean(int[][] board)
	{
		board[X][Y] = 0;
	}

	public void ResetUsedValues()
	{
		_usedValues = new List<int>();
	}

	private Tuple<int, int> GetQuadrant()
	{
		return _quadrants.First(x => ValueIsInsideQuadrant(X, x.Item1) && ValueIsInsideQuadrant(Y, x.Item2));

		bool ValueIsInsideQuadrant(int value, int quadrantValue)
		{
			return value <= quadrantValue && quadrantValue - value <= 2;
		}
	}
}