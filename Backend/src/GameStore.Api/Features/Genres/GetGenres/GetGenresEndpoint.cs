using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
    public static void MapGetGenres(this IEndpointRouteBuilder app)
    {
        // GET /genrres
        app.MapGet("/", (GameStoreContext dbContext) =>
                 dbContext.Genres
                          .Select(genre => new GenreDto(genre.Id, genre.Name))
                          .AsNoTracking());
    }
}
