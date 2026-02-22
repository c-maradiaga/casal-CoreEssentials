namespace GameStore.Api.Data
{
    public class GameDataLogger(GameStoreData data, ILogger<GameDataLogger> logger)
    {
        public void PrintGames()
        {
            foreach(var game in data.GetGames())
            {
                logger.LogInformation($"Game id:[{game.Id}] **  Game Name: [{game.Name}]");
            }
        }
    }
}
