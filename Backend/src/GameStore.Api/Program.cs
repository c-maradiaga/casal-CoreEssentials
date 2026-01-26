using System.Collections.Concurrent;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

ConcurrentBag<Game> games = new()
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

const string GetGameEnpointName = "GetName";


app.MapGet("/games", () => games);


app.MapGet("/games/{id}", (Guid id) =>
{
    var game = games.FirstOrDefault(g => g.Id == id);
    return game is not null ? Results.Ok(game) : Results.NotFound();
}).WithName(GetGameEnpointName) ;

// POST 
app.MapPost("/games", (Game newGame) =>
{
    // if(string.IsNullOrWhiteSpace(newGame.Name))
    //     return Results.BadRequest("El nombre del Juego no puede estar vacio.");
   
    newGame.Id = Guid.NewGuid();
    games.Add(newGame);
    //return Results.Created($"/games/{newGame.Id}", newGame);

    return Results.CreatedAtRoute(GetGameEnpointName, new { id = newGame.Id }, newGame); 
}).WithParameterValidation();


// PUT /games/21565-1516
app.MapPut("/games/{id}", (Guid id, Game updatedGame) =>
{
    var existingGame = games.FirstOrDefault(g => g.Id == id);

    if (existingGame is null) 
        return Results.NotFound();

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
}).WithParameterValidation();



app.Run();
