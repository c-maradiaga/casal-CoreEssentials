using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");

/* What service lifetime to use for dbContext ?
- DbContext is designed to be used as a single Unit of Work
- DbContext created --> Entity changes traked --> save changes --> dispose
- DB connection are expensive
- DbContext is no thread safe
- Increase memory usage due to change tracking

USE: Scope servive lifetime for DbContext
- Aligining the context lifetime to the lifetime of the request
- There is only ne thread executing each client reque at a given time
- Ensure each request gets a separate DbContext instance.
*/

// Otra forma seria:
builder.Services.AddDbContext<GameStoreContext>(
    options => options.UseSqlite(connString)

);

// Formamas simple, sin necesidad de instalar el paquete Microsoft.EntityFrameworkCore.Sqlite, ya que el paquete Microsoft.EntityFrameworkCore incluye un proveedor de SQLite integrado. 
//builder.Services.AddSqlite<GameStoreContext>(connString);

//! Registrar los Servicios en el contenedor de dependencias, Antes del app = builder.Build();
//? Los servicios para el contenedor de dependencias se registran depues del var builder y antes
//? del var app, es decir, antes de builder.Build().
builder.Services.AddTransient<GameDataLogger>();
builder.Services.AddScoped<GameStoreData>();

var app = builder.Build();

app.MapGames();
app.MapGenres();

//? Cuando la aplicacion se inicia, se ejecuta el método MigrateDb para aplicar las migraciones pendientes a la base de datos. 
//? Esto asegura que la base de datos esté actualizada con el esquema definido en el código antes de que la aplicación comience a manejar solicitudes.    
app.MigrateDb();

app.Run();

