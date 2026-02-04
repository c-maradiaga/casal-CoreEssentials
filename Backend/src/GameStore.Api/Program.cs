using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Data;
using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

const string GetGameEnpointName = "GetName";

GameStoreData data = new GameStoreData();

app.MapGetGames(data);


app.MapGet("/games/{id}", (Guid id) =>
{
    Game? game = data.GetGame(id);

    return game is null ? Results.NotFound() : Results.Ok(new GameDetailsDto(game.Id,
                                                                             game.Name,
                                                                             game.Genre.Id,
                                                                             game.Price,
                                                                             game.ReleaseDate,
                                                                             game.Description));
}
).WithName(GetGameEnpointName);

// POST 
app.MapPost("/games", (CreateGameDto gameDto) =>
{
    var genre = data.GetGenre(gameDto.GenreId);
    if (genre is null)
    {
        return Results.BadRequest("Invalid Genre id");
    }

    var game = new Game
    {
        Id = Guid.NewGuid(),
        Name = gameDto.Name,
        Genre = genre,
        Price = gameDto.Price,
        ReleaseDate = gameDto.ReleaseDate,
        Description = gameDto.Description
    };

    data.AddGame(game);

    return Results.CreatedAtRoute(
        GetGameEndpointName,
        new { id = game.Id },
        new GameDetailsDto(
            game.Id,
            game.Name,
            game.Genre.Id,
            game.Price,
            game.ReleaseDate,
            game.Description
        ));
})
.WithParameterValidation();

app.MapDelete("/games/{id}", (Guid id) =>
{
    data.RemoveGame(id);

    return Results.NoContent();
});

// PUT /games/122233-434d-43434....
app.MapPut("/games/{id}", (Guid id, UpdateGameDto gameDto) =>
{
    var existingGame = data.GetGame(id);

    if (existingGame is null)
        return Results.NotFound();

    var genre = data.GetGenre(gameDto.GenreId);

    if (genre is null)
        return Results.BadRequest("Invalid Genre id");

    existingGame.Name = gameDto.Name;
    existingGame.Genre = genre;
    existingGame.Price = gameDto.Price;
    existingGame.ReleaseDate = gameDto.ReleaseDate;
    existingGame.Description = gameDto.Description;

    return Results.NoContent();
})
.WithParameterValidation();

// Get/genres
app.MapGet("/genres", () => data.GetGenres().Select(genre => new GenreDto(genre.Id, genre.Name)));

app.Run();

public record GameDetailsDto(
    Guid Id,
    string Name,
    Guid GenreId,
    decimal Price,
    DateOnly ReleaseDate,
    string Description);



public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description
);

public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description
);

public record GenreDto(Guid Id, string Name);