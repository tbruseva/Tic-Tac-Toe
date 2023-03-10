using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;

namespace Tic_Tac_Toe_Web_API.Managers.Interfaces
{
    public interface IGameManager
    {
        public IGame CreateGame();
        public List<IGame> GetAllGames();
        public TicTacToeGame GetGameById(int id);
        public IGame JoinGame(int id, Player player);
        public TicTacToeGame TicTacToeSelectMark(int gameId, int playerId, string mark);
        public TicTacToeGame TicTacToeMakeMove(int gameId, int playerId, int rowPosition, int colPosition);
        public TicTacToeGame TicTacToeRestartGame(int gameId, int playerId);

    }
}
