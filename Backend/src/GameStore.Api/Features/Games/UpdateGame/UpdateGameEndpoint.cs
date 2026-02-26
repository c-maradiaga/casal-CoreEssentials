using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapUpdateGame(this IEndpointRouteBuilder app)
    {
        // PUT /game/1356-16
        app.MapPut("/{id}", (Guid id, UpdateGameDto updateGameDto, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);
            if (existingGame is null)
                return Results.NotFound();

            existingGame.Name = updateGameDto.Name;
            existingGame.GenreId = updateGameDto.GenreId;
            existingGame.Price = updateGameDto.Price;
            existingGame.ReleaseDate = updateGameDto.ReleaseDate;
            existingGame.Description = updateGameDto.Description;

            dbContext.SaveChanges();

            return Results.NoContent();

        }).WithParameterValidation();
    }
}
