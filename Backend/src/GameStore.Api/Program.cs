using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Game> games = new()
{
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "The Witcher 3: Wild Hunt",
        Genre = "RPG",
        Price = 39.99M,
        ReleaseDate = new DateOnly(2015, 5, 19)
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "Cyberpunk 2077",
        Genre = "Action RPG",
        Price = 59.99M,
        ReleaseDate = new DateOnly(2020, 12, 10)
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "Minecraft",
        Genre = "Sandbox",
        Price = 26.95M,
        ReleaseDate = new DateOnly(2011, 11, 18)
    }
};

app.MapGet("/", () => "Hello World!");

app.Run();
