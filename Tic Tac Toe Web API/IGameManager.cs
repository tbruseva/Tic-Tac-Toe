using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Enums;

namespace Tic_Tac_Toe_Web_API
{
    public interface IGameManager
    {
        public void AddPlayer(IGame game, Player player);
        public IGame CreateGame();
        public List<IGame> GetAllGames();
        public IGame JoinGame(int id, Player player);
        public Player SelectFirstOrSecondPlayer(int gameId, string username, string mark);
       // public IGame StartGame(int id, Player player);
    }
}
