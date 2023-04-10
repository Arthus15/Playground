using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Playground;
using Playground.Games.GameOfLife.Service;
using Playground.Games.Sudoku.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IGameOfLifeService, GameOfLifeService>().AddScoped<ISudokuService, SudokuService>();
builder.Services.AddMudServices();
await builder.Build().RunAsync();