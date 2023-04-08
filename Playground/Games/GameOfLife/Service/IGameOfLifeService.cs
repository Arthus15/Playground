namespace Playground.Games.GameOfLife.Service
{
	public interface IGameOfLifeService
	{
		IGameOfLifeService Setup(int[] grid);
		int[] Run();
	}
}
