using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (GameStoreContext dbContext) =>
          dbContext.Games
             .Include(game => game.Genre)
             .Select(game => new GameSummaryDto(
                    game.Id,
                    game.Name,
                    game.Genre!.Name,
                    game.Price,
                    game.ReleaseDate
                ))
            .AsNoTracking());
    }
}
