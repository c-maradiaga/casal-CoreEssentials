namespace GameStore.Api.Features.Games.Constants
{
    public static class EndpointNames
    {
        //public const string GetGame="GetGame";
        // como la constante tiene el mismo nombre que el valor que se le
        // asigna, entonce se utiliza el nameof, y de esta forma, no queda
        // el valor asignado en duro.
        public const string GetGame = nameof(GetGame);

    }
}