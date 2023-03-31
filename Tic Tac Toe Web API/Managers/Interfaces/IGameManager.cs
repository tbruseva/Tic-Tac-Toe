using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using System.Numerics;

namespace Tic_Tac_Toe_Web_API.Managers.Interfaces
{
    public interface IGameManager
    {
        public Task<IGame> CreateGameAsync(string name);
        public Task<List<IGame>> GetAllGamesAsync();
        public Task<IGame> GetGameByIdAsync(int id);
        public Task<IGame> JoinGameAsync(int id, Player player);
        public Task<IGame> JoinGameAgainstComputerAsync(int gameId, Player player);
        public Task<IGame> SelectMarkAsync(int gameId, int playerId, string playerMark);
        public Task<RotaGame> AddPawnAsync(int gameId, int playerId, int position);
        public Task<RotaGame>  RotaMakeMoveAsync(int gameId, int playerId, int oldPosition, int newPosition);
        public Task<TicTacToeGame> TicTacToeMakeMoveAsync(int gameId, int playerId, int rowPosition, int colPosition);
        public Task<IGame> RestartGameAsync(int gameId, int playerId);
        public Task<int> GetGameStateAsync(int gameId);

    }
}
