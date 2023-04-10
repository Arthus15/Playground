using Microsoft.AspNetCore.Components;

namespace Playground.Games.GameOfLife
{
	public partial class GameOfLife : ComponentBase
	{
		public int CellCount = 400;
		private int[]? _cellStatus;
		private bool _gameRunning;
		public override Task SetParametersAsync(ParameterView parameters)
		{
			_cellStatus = Enumerable.Repeat(0, CellCount).ToArray();

			return base.SetParametersAsync(parameters);
		}

		void UpdateCell(int index)
		{
			if(_gameRunning)
				return;

			_cellStatus![index] = _cellStatus[index] is 0 ? 1 : 0;
			Console.WriteLine($"Cell[{index}] = {_cellStatus[index]}");
			StateHasChanged();
		}

		string SetStyle(int index)
		{
			if (_cellStatus![index] is 0)
			{
				return "background: #ffffff";
			}

			return "background: #1CFF8A";
		}

		async Task StartGameAsync()
		{
			if (_gameRunning)
			{
				Console.Error.WriteLine("Game is already started");
				return;
			}

			GameOfLifeService.Setup(_cellStatus!);
			_gameRunning = true;

			while (_gameRunning)
			{
				_cellStatus = GameOfLifeService.Run();
				StateHasChanged();
				await Task.Delay(TimeSpan.FromMilliseconds(50));

				if (!_cellStatus.Any(x => x is 1))
				{
					_gameRunning = false;
				}
			}
		}

		void Stop()
		{
			_gameRunning = false;
		}
	}
}
