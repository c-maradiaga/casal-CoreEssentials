namespace GameStore.Api.Features.Games.GetGame.Constants
{
    public static class EndpointNames
    {
      /// El nombre del endpoint es importante para generar correctamente los enlaces a este 
      /// endpoint desde otros lugares de la aplicación, como por ejemplo desde el endpoint que
      ///  devuelve la lista de juegos. 
      /// Esta constante se utiliza en el método WithName() al definir el endpoint, y luego se puede usar 
      /// ese nombre para generar enlaces a este endpoint de manera segura, sin tener que preocuparse 
      /// por errores tipográficos o cambios en la ruta del endpoint.
      const string GetGame = nameof(GetGame); 
    }
}