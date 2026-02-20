using GameStore.Api.Data;

namespace GameStore.Api.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
    public static void MapGenres(this IEndpointRouteBuilder app, GameStoreData data)
    {
        // GET /genrres
        app.MapGet("/", () => data.GetGenres().Select(genre => new GenreDto(genre.Id, genre.Name)));
    }
}
