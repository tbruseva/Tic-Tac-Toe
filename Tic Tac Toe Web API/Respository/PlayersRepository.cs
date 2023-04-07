using Tic_Tac_Toe_Web_API.Database_Models;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Respository
{
    public class PlayersRepository
    {
        private AppDbContext _dbContext;

        public PlayersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task Create(Player player, IGame game)
        ////{
        ////    if (game.Name == "Tic-Tac-Toe")
        ////    {
        ////       game = game as TicTacToeGame;
        ////    }
        ////    else if (game.Name == "Rota")
        ////    {
        ////        game = game as RotaGame;
        ////    }
        ////    var playerDb = new PlayerDbModel() { Id = player.Id,Name = player.Name, Game = game.Name, Wins = (game.CounterWins.Where};
            
        //}
    }
}
