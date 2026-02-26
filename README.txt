## casal-CoreEssentials

Proyecto .NET tipo Web API con .NET 8 del curso de Julio Casal

### 7 Adding Serverside Validtaion

Nota: Asegurarse que REST Client este instalada, y habilitada en este Workspace
para que los endpoint del archivo gamestore.http se puedan ejecutar desde ahi, es decir que
aparezca encima de cada endpoint la opcion "Send"

* Las MinimalAPIS no aplican diretamente todo lo que es DataAnnotation. Para forzar las validaciones de DataAnnotation
  en MinimalAPI se una un Endpoint Filter, que se usa para ejecutar algo comportamiento ya sea antes o despues
  del request. Para esto se instalo la librearia "MinimalApis.Extensions - helpers for working with ASP.NET Core Minimal APIs."
  en la ruta: \casal-CoreEssentials\Backend\src\GameStore.Api> (se debe instarlar en el directorio d onde esta el archvio .csproj)
  2. En el endpoint que se debe validar, es decir donde se desea aplicar el filtro, se debe agregar el siguiente codigo:
    "}).WithParameterValidation();"

Para la lista originall Lis<Game> se cambio a ConcurrentBag<Game>  para evitar conflictos de cocurrencia cuando la aplicacióon
se ejecuta varias veces en un mismo momento.

USING DATATRANFERS OBJECTS:
* In a REST API, what is the primary purpose of using a Data Transfer Object (DTO)?
A Data Transfer Object (DTO) serves as a contract in a REST API by defining the expected structure and content of data exchanged 
between the client and the server. 
This contract ensures consistency and compatibility, even if the underlying database models change. By using DTOs, the API can adapt to 
changes in database structure without breaking the client, as only the DTO structure (the "contract") remains consistent in client-server communication. 
This approach helps maintain a stable interface for clients, enhancing the API’s reliability and flexibility.

* Why are record types preferred for defining DTOs in .NET applications?
Record types in .NET are preferred for defining DTOs because they are immutable by default. Once their properties are set,
usually at the time of creation, they cannot be changed. 
This immutability is perfect for DTOs as they are meant to carry data from one point to another without the need 
for modification, ensuring data consistency and integrity as it moves across different layers of an application or between applications.

** VERTICAL ARCHITECTURE: **
Al definir public IEnumerable<Game> GetGames() => games;  como IEnumerable en lugar de List es porque IEnumerable
unicamente permite iterar sobre la colección, mientras que con List se permite agregar o eliminar elementos, lo cual
no se desea cuando se retorna la data al cliente.

IEnumerable<Game> GetGames() => games  es equivalent a tener:
public IEnumerable<Game> GetGames()
{
    return games;
}

### Introducction to Vertical Architecture:
Prestation Layer, Business Layer, Data Access Layer, Database Layer.

Slice: Codebase is deivden int oindependent feature (slices)
Each slice conains evertything neeeded for a specific feature.

Vertical Architecture: 
    - Each slice is a feature.
    - Each slice is independent of the others.

Structuring a slice:
INPUT LAYER: (CreateGameDto) ----> Handler (CreateGameEndpoint) ---> OUTPUT LAYER (CreateGameResultDto)

Using Route Groups:

**** What is one of the main advantages of using Vertical Slice Architecture over n-tier architecture?
Vertical Slice Architecture organizes code by features rather than technical concerns. This setup allows all parts 
of a feature—presentation, business logic, and data access—to exist within a single unit, making it easier to 
maintain and understand.
By avoiding layer-based separation, it reduces unnecessary abstractions that can slow down productivity and 
complicate code navigation, especially for new team members. 

 **** What is a primary benefit of using extension methods in C#?   
 Extension methods provide a way to "extend" a class by adding new methods without altering the class's 
 original code or structure. This is especially 
 useful for built-in or third-party classes that can't be changed directly.

**** What is one of the main benefits of using route groups in ASP.NET Core Minimal APIs?
Using route groups in ASP.NET Core Minimal APIs helps reduce redundancy by allowing a common route 
prefix and settings to be applied across multiple endpoints. 
This makes the code more maintainable and organized, as developers don’t need to repeat the route prefix in each endpoint.

### Dependency Injection:

** Transient lifetime services are create each time they are requested from the service container
(IServiceProvider)

** Scoped lifetime services are create once por HTTP request and reused within that request.

** Singleton lifetime services are created the first time they are requested and reused across
  the application lifetime.


Dependency Injection allows a class to rely on dependencies without managing their creation or configuration. 
Instead, dependencies are provided to the class, which simplifies code by removing the need for direct instantiation.

This approach decouples the class from specific implementations, so if a dependency changes, there’s no need to modify the class itself. 
DI promotes flexibility, testability, and cleaner code structure.

Why can't a scoped service be injected into a singleton in ASP.NET Core?
Scoped services depend on the request lifecycle, which does not exist for singletons.

### INTRODUCTION TO ENTITY FRAMEWORK:
#### Creando las migraciones:

El Entity Framework se bajo desde www.nuget.org 
Se busco dotnet-ef y se paso a la pesta;a de Versions para bajar la ultima de la version 8, ya que el proyecto lo\
tengo en la version .NET 8

Paquetes y tools a instalar : Ambos dentro de la carpeta GameStore.Api
dotnet tool install --global dotnet-ef --version 8.0.24
dotnet-ef 
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.24

Creando las migraciones:
dotnet ef migrations add Inicial --output-dir Data\Migraciones

Migrating the database when the app starts:

# Genera un script SQL con todos los cambios desde el inicio  
  
  dotnet ef migrations script

# Genera un script solo desde una migración específica hasta otra
# Esto es lo más útil: genera SOLO los cambios pendientes

  dotnet ef migrations script MigracionAnterior MigracionNueva

# El flag --idempotent es muy importante: genera un script que verifica
# si cada migración ya fue aplicada antes de ejecutarla.
# Así puedes correrlo múltiples veces sin miedo.
  
    dotnet ef migrations script --idempotent --output migration.sql

#### In Entity Framework Core, what is the primary role of the DbContext?
A DbContext instance in Entity Framework Core represents a session with the database and is used primarily for querying and saving instances of your entities. 
This makes it a central class in the Entity Framework API, acting as a bridge between your application's domain or entity classes and the database. 
Its role encompasses the patterns of Unit Of Work and Repository, allowing for a simplified and abstracted approach to handling data access. 
While it does offer features that could indirectly support tasks like data validation and direct SQL execution, its primary purpose is to manage entity objects during runtime, which includes retrieving and saving data to the database.

#### What is the key benefit of the configuration system in ASP.NET Core?
The configuration system in ASP.NET Core is designed to be flexible and versatile, allowing for the use of multiple configuration 
sources like appsettings.json, command line arguments, environment variables, and more. 

A significant benefit of this system is that it abstracts the details of where configuration data comes from. 
This simplification is achieved through the IConfiguration interface, which presents a unified API for accessing configuration 
values regardless of their source. 

This means developers can write code that is agnostic of the configuration source, which makes the application 
more modular and adaptable to changes in its environment.

#### Creating new database records:

#### ** Questions:
- 1.When using Entity Framework Core, why is it necessary to call SaveChanges after modifying an entity?
Entity Framework Core uses change tracking to monitor modifications to entities in memory, but these tracked
changes are not applied to the database until SaveChanges is explicitly called. 
This allows developers to group multiple changes and save them in a single transaction.

 - 2. In Entity Framework Core, what is the purpose of the ExecuteDelete method?
To delete a set of records from the database directly without loading them into memory.
The ExecuteDelete method in EF Core allows for a bulk deletion of records directly at the database level without
loading each record into memory, which improves performance when deleting multiple records at once. 
This method is particularly efficient for scenarios where you want to delete a filtered set of data in one call.
Unlike other methods that require tracking entities, ExecuteDelete is designed to execute directly in the database,
bypassing EF Core’s change tracker, making it an ideal choice for bulk deletions.

- 3.
What is the primary purpose of the AsNoTracking() method in Entity Framework Core?
The AsNoTracking() method is used to retrieve entities from the database without adding them to the change tracker. 
This improves query performance because EF Core skips tracking information. 
This method is particularly useful in read-only scenarios where you don’t need to modify the data.
























