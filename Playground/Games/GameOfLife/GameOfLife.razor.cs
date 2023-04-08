using Microsoft.AspNetCore.Components;

namespace Playground.Games.GameOfLife
{
	public partial class GameOfLife : ComponentBase
	{
		public int CellCount = 400;
		private int[] _cellStatus;
		private bool _gameStarted = false;
		public override Task SetParametersAsync(ParameterView parameters)
		{
			_cellStatus = Enumerable.Repeat(0, CellCount).ToArray();

			return base.SetParametersAsync(parameters);
		}

		void UpdateCell(int index)
		{
			_cellStatus[index] = _cellStatus[index] is 0 ? 1 : 0;
			Console.WriteLine($"Cell[{index}] = {_cellStatus[index]}");
			StateHasChanged();
		}

		string SetStyle(int index)
		{
			if (_cellStatus[index] is 0)
			{
				return "background: #ffffff";
			}

			return "background: #1CFF8A";
		}

		void StartGame()
		{
			//if (_gameStarted)
			//{
			//	Console.Error.WriteLine("Game is already started");
			//	return;
			//}

			GameOfLifeService.Setup(_cellStatus);
			_gameStarted = true;

			//TODO: Add stopper
			_cellStatus = GameOfLifeService.Run();
			StateHasChanged();
		}
	}
}
