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

VERTICAL ARCHITECTURE:
