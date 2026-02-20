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

#### RESUMEN:
An API is a set of definitions and protocols for building and integratin application software. It defines methos and data formats
for communicating with the service from an application.

REST (Representational State Transfer), on the other hand, is an architectural style that defines a set of constraints for creating
Web services. RESTful services enable interacting parties to communicate over the Web using the standart HTTP protocol.

The principles of REST guide the design of the architecture for APIs, focusing on stateless communication, resource-base URLs,
and the use of HTTP methods to perfom operations.
This relationship alowa APIS to be desgined in a way that is efficient, scalable, and easy to use.

Unlike POST, wich is used to create new resources, PUT is idempotent, meaning that multiple identical request should have the same
effects as a single request.

### Using Data Transfer Objects
Para generar un GUID desde la terminal: [guid]::NewGuid()  luego ENTER