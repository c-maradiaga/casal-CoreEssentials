using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Genre> genres =
[
    new Genre { Id = new Guid("4e179397-c3f1-45ec-a271-c26f07ff64f3"), Name = "Fighting"},
    new Genre { Id = new Guid("b2c3d4e5-f678-90a1-b2c3-d4e5f67890a1"), Name = "Kids and Family" },
    new Genre { Id = new Guid("c3d4e5f6-7890-a1b2-c3d4-e5f67890a1b2"), Name = "Racing" },
    new Genre { Id = new Guid("d4e5f678-90a1-b2c3-d4e5-f67890a1b2c3"), Name = "Roleplaying" },
    new Genre { Id = new Guid("e5f67890-a1b2-c3d4-e5f6-7890a1b2c3d4"), Name = "Sports" }
];

List<Game> games =
[
    new Game {
        Id = Guid.NewGuid(),
        Name = "Street Fighter II",
        Genre = genres[0],
        Price = 19.99m,
        ReleaseDate = new DateOnly(1992, 7, 15),
        Description = "Street Fighter 2, the most iconic fighting game of all time, is back on the Nintendo Switch! The newest iteration of SFII in nearly 10 years, Ultra Street Fighter 2 features all of the classic characters, a host of new single player and multiplayer features, as well as two new fighters: Evil Ryu and Violent Ken!"
    },
    new Game {
        Id = Guid.NewGuid(),
        Name = "Final Fantasy XIV",
        Genre = genres[3],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2010, 9, 30),
        Description = "Join over 27 million adventurers worldwide and take part in an epic and ever-changing FINAL FANTASY. Experience an unforgettable story, exhilarating battles, and a myriad of captivating environments to explore." },
    new Game {
        Id = Guid.NewGuid(),
        Name = "FIFA 23",
        Genre = genres[4],
        Price = 69.99m,
        ReleaseDate = new DateOnly(2022, 9, 27),
        Description = "FIFA 23 brings The World's Game to the pitch, with HyperMotion2 Technology, men's and women's FIFA World Cup™, women's club teams, cross-play features, and more." }
];

const string GetGameEnpointName = "GetName";


app.MapGet("/games", () => games);


app.MapGet("/games/{id}", (Guid id) =>
{
    var game = games.FirstOrDefault(g => g.Id == id);
    return game is not null ? Results.Ok(game) : Results.NotFound();
}).WithName(GetGameEnpointName);

// POST 
app.MapPost("/games", (CreateGameDto gameDto) =>
{
    var genre = genres.Find(genre => genre.Id == gameDto.GenreId);

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

    games.Add(game);

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
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

// PUT /games/122233-434d-43434....
app.MapPut("/games/{id}", (Guid id, UpdateGameDto gameDto) =>
{
    var existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
        return Results.NotFound();


    var genre = genres.Find(genre => genre.Id == gameDto.GenreId);

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



app.Run();

public record GameDetailsDto(
    Guid Id,
    string Name,
    Guid GenreId,
    decimal Price,
    DateOnly ReleaseDate,
    string Description);

public record GameSummaryDto(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);

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