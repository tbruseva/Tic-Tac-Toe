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
        public Player SelectMark(int gameId, string username, string mark);
        public IGame MakeMove(int gameId, string username, int rowPosition, int colPosition);
        public IGame RestartGame(int gameId, string username);
       // public IGame StartGame(int id, Player player);
    }
}
