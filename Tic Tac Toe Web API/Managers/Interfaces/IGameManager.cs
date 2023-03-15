using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using System.Numerics;

namespace Tic_Tac_Toe_Web_API.Managers.Interfaces
{
    public interface IGameManager
    {
        public Task<IGame> CreateGameAsync();
        public Task<List<IGame>> GetAllGamesAsync();
        public Task<TicTacToeGame> GetGameByIdAsync(int id);
        public Task<IGame> JoinGameAsync(int id, Player player);
        public Task<IGame> JoinGameAgainstComputerAsync(int gameId, Player player);
        public Task<TicTacToeGame> TicTacToeSelectMarkAsync(int gameId, int playerId, string mark);
        public Task<TicTacToeGame> TicTacToeMakeMoveAsync(int gameId, int playerId, int rowPosition, int colPosition);
        public Task<TicTacToeGame> TicTacToeRestartGameAsync(int gameId, int playerId);
        public Task<int> GetGameStateAsync(int gameId);

    }
}
