using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;

namespace Tic_Tac_Toe_Web_API
{
    public interface IGameManager
    {
        public AllGamesResponseDto CreateGame();
        public List<AllGamesResponseDto> GetAllGames();
        public TicTacToeGameResponseDto GetGameById(int id);
        public AllGamesResponseDto JoinGame(int id, Player player);
        public TicTacToeGameResponseDto TicTacToeSelectMark(int gameId, int playerId, string mark);
        public TicTacToeGameResponseDto TicTacToeMakeMove(int gameId, int playerId, int rowPosition, int colPosition);
        public TicTacToeGameResponseDto TicTacToeRestartGame(int gameId, string username);

    }
}
