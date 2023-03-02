using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;

namespace Tic_Tac_Toe_Web_API
{
    public interface IGameManager
    {
        public GameResponseDto CreateGame();
        public List<GameResponseDto> GetAllGames();
        public GameResponseDto GetGameById(int id);
        public GameResponseDto JoinGame(int id, Player player);
        public GameResponseDto SelectMark(int gameId, int playerId, string mark);
        public GameResponseDto MakeMove(int gameId, int playerId, int rowPosition, int colPosition);
        public GameResponseDto RestartGame(int gameId, string username);

    }
}
