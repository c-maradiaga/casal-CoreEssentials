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
