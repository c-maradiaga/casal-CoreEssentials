using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

//! Registrar los Servicios en el contenedor de dependencias, Antes del app = builder.Build();
//? Los servicios para el contenedor de dependencias se registran depues del var builder y antes
//? del var app, es decir, antes de builder.Build().
builder.Services.AddTransient<GameDataLogger>();
builder.Services.AddScoped<GameStoreData>();

var app = builder.Build();

app.MapGames();
app.MapGenres();

app.Run();




