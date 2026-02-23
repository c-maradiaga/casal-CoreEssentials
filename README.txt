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
