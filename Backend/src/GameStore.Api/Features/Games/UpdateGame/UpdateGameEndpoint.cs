using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapUpdateGame(this IEndpointRouteBuilder app)
    {
        // PUT /game/1356-16
        app.MapPut("/{id}", (Guid id, UpdateGameDto updateGameDto, GameStoreData data) =>
        {
            var existingGame = data.GetGame(id);
            if (existingGame is null)
                return Results.NotFound();

            var genre = data.GetGenre(updateGameDto.GenreId);
            if (genre is null)
                return Results.BadRequest("Invalid Genre id");

            existingGame.Name = updateGameDto.Name;
            existingGame.Genre = genre;
            existingGame.Price = updateGameDto.Price;
            existingGame.ReleaseDate = updateGameDto.ReleaseDate;
            existingGame.Description = updateGameDto.Description;

            return Results.NoContent();

        }).WithParameterValidation();
    }
}
