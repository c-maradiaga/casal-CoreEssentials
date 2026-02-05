using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres.GetGenres;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


GameStoreData data = new GameStoreData();

// Estos Endpoints se han movido a sus respectivas clases

app.MapGames(data);
app.MapGenres(data);


app.Run();


