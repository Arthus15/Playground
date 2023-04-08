

namespace Playground.Games.GameOfLife.Service
{
    public class GameOfLifeService : IGameOfLifeService
    {
	    private int[,]? _matrix;

	    public IGameOfLifeService Setup(int[] grid)
		{
		    _matrix = ParseViewGridToMatrix(grid);

		    return this;
		}

	    public int[] Run()
	    {
		    if (_matrix is null)
		    {
			    throw new InvalidOperationException("You need to Setup the game before run it");
		    }

		    var grid = new int[_matrix!.Length];

		    for (int i = 0; i <= _matrix.GetUpperBound(1); i++)
		    {
			    for (int j = 0; j <= _matrix.GetUpperBound(1); j++)
			    {
				    grid[(i * (_matrix.GetUpperBound(1)+1)) + j] = CalculateCellStatus(i,j);
			    }
		    }

		    _matrix = ParseViewGridToMatrix(grid);

		    return grid;

		    int CalculateCellStatus(int i, int j)
		    {
			    int count = 0;
			    foreach (CellNeighbors neighbor in Enum.GetValues(typeof(CellNeighbors)))
			    {
				    var indexes = GetIndex(neighbor, i, j);

				    if (!IsInsideMatrix(indexes))
				    {
					    continue;
				    }

				    count += _matrix![indexes.i, indexes.j];
			    }

				Console.WriteLine($"Index [{i},{j}] living neighbors is {count}");

			    return IsAlive(count, _matrix[i,j]);

			    int IsAlive(int totalNeighborsAlive, int currentStatus)
			    {
				    if (IsDead(currentStatus))
				    {
					    return totalNeighborsAlive == 3 ? 1 : 0;
				    }

					return totalNeighborsAlive is 2 or 3 ? 1 : 0;
			    }
		    }
	    }

	    private int[,] ParseViewGridToMatrix(int[] grid)
		{
			var matrix = new int[20, 20];
			int i = 0;
			int j = 0;

			for (int p = 0; p < grid.Length; p++)
			{
				matrix[i, j] = grid[p];
				j = (j + 1) % 20;

				if (j == 0)
				{
					i += 1;
				}
			}

			return matrix;
		}

	    private (int i, int j) GetIndex(CellNeighbors neighbor, int i, int j) => neighbor switch
	    {
		    CellNeighbors.First => (i.Sub(), j.Sub()), CellNeighbors.Second => (i.Sub(), j),
		    CellNeighbors.Third => (i.Sub(), j.Sum()), CellNeighbors.Fourth => (i, j.Sub()),
		    CellNeighbors.Fifth => (i, j.Sum()), CellNeighbors.Sixth => (i.Sum(), j.Sub()),
		    CellNeighbors.Seventh => (i.Sum(), j), CellNeighbors.Eighth => (i.Sum(), j.Sum()),
		    _ => throw new ArgumentOutOfRangeException(nameof(neighbor), neighbor, null)
	    };

		//TODO: Setup matrix limit dynamically
	    private bool IsInsideMatrix((int i, int j) indexes) => indexes.i is >= 0 and < 20 && indexes.j is >= 0 and < 20;
	    private bool IsDead(int currentStatus) => currentStatus is 0;
	    private bool IsAlive(int currentStatus) => currentStatus is 1;
    }

    enum CellNeighbors
	{
		First
		,Second
		,Third
		,Fourth
		,Fifth
		,Sixth
		,Seventh
		,Eighth
    }

    public static class GameOfLifeServiceExtensions
    {
	    public static int Sum(this int i) => i + 1;
	    public static int Sub(this int i) => i - 1;
    }
}
