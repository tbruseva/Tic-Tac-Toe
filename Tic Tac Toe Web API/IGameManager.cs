using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Enums;
using System.Numerics;

namespace Tic_Tac_Toe_Web_API
{
    public interface IGameManager
    {
        public void AddPlayer(IGame game, Player player);
        public IGame CreateGame();
        public List<IGame> GetAllGames();
        public IGame GetGameById(int id);
        public IGame JoinGame(int id, Player player, string mark);
        public Player SelectFirstOrSecondPlayer(int gameId, string username, string mark);
        public IGame MakeMove(int gameId, string username, int rowPosition, int colPosition);
       // public IGame StartGame(int id, Player player);
    }
}
