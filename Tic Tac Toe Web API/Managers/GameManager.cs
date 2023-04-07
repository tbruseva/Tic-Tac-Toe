using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Enums;
using System.Numerics;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Mappers;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Respository;

namespace Tic_Tac_Toe_Web_API.Managers
{
    public class GameManager : IGameManager
    {
        private List<IGame> _allGames = new List<IGame>();
        private PlayersRepository _playersRepository;
        private ResultsRepository _resultsRepository;

        public GameManager(PlayersRepository playersRepository, ResultsRepository resultsRepository)
        {
            _playersRepository= playersRepository;
            _resultsRepository= resultsRepository;
        }

        public async Task<List<IGame>> GetAllGamesAsync()
        {
            var taskGames = Task.Run(() => _allGames.ToList());
            var games = await taskGames;
            return games;
        }

        public async Task<IGame> GetGameByIdAsync(int id)
        {
            var game = _allGames.Where(g => g.Id == id).FirstOrDefault();
            if (game == null)
            {
                throw new Exception("Game doesn't exist!");
            }

            return game;
        }

        public async Task<IGame> CreateGameAsync(string name)
        {
            IGame game = null;

            if (name == "Tic-Tac-Toe")
            {
                game = new TicTacToeGame();
            }
            else if (name == "Rota")
            {
                game = new RotaGame();
            }
            else
            {
                throw new Exception("Game isn't supported!");
            }

            _allGames.Add(game);

            return game;
        }

        public async Task<IGame> JoinGameAsync(int gameId, Player player)
        {
            var game = await GetGameAsync(gameId);
            await game.JoinGameAsync(player);

            return game;
        }

        public async Task<IGame> JoinGameAgainstComputerAsync(int gameId, Player player)
        {
            var game = await GetGameAsync(gameId);
            await game.JoinGameAgainstComputerAsync(player);

            return game;
        }

        public async Task<RotaGame> AddPawnAsync(int gameId, int playerId, int position)
        {
            var game = await GetGameAsync(gameId) as RotaGame;
            if (game == null)
            {
                throw new Exception("Game doesn't exist!");
            }

            if (game.Players.Any(p => p.Id == 0))
            {
                await game.AddPawnAgainstComputerAsync(playerId, position);

                return game;
            }
            await game.AddPawnAsync(playerId, position);

            return game;
        }

        public async Task<IGame> SelectMarkAsync(int gameId, int playerId, string playerMark)
        {
            var game = await GetGameAsync(gameId) as TicTacToe;
            if (game == null)
            {
                throw new InvalidDataException("Game with Id {gameId} doesn't exist!");
            }

            Mark selectedMark;
            Enum.TryParse(playerMark, true, out selectedMark);

            await game.SelectMarkAsync(playerId, selectedMark);

            return game;
        }

        public async Task<IGame> RestartGameAsync(int gameId, int playerId)
        {

            var game = await GetGameAsync(gameId) as TicTacToe;
            if (game == null)
            {
                throw new InvalidDataException("Game with Id {gameId} doesn't exist!");
            }

            if (!game.Players.Exists(p => p.Id == playerId))
            {
                throw new UnauthorizedAccessException("Only game players can restart the game!");
            }

            await game.RestartGameAsync();

            return game;
        }

        public async Task<RotaGame> RotaMakeMoveAsync(int gameId, int playerId, int oldPosition, int newPosition)
        {
            var game = await GetGameAsync(gameId) as RotaGame;
            if (game == null)
            {
                throw new InvalidDataException($"Game with Id {gameId} doesn't exist!");
            }
            if (game.Players.Any(p => p.Id == 0))
            {
                await game.MakeMoveAgainstComputerAsync(playerId, oldPosition, newPosition);

                return game;
            }
            await game.MakeMoveAsync(playerId, oldPosition, newPosition);

            return game;
        }

        public async Task<TicTacToeGame> TicTacToeMakeMoveAsync(int gameId, int playerId, int rowPosition, int colPosition)
        {
            var game = await GetGameAsync(gameId) as TicTacToeGame;
            if (game == null)
            {
                throw new InvalidDataException($"Game with Id {gameId} doesn't exist!");
            }
            if (game.Players.Any(p => p.Id == 0))
            {
                await game.MakeMoveAgainstComputerAsync(playerId, rowPosition, colPosition);

                return game;
            }
            await game.MakeMoveAsync(playerId, rowPosition, colPosition);

            return game;
        }

        public async Task<int> GetGameStateAsync(int gameId)
        {
            var game = await GetGameAsync(gameId) as TicTacToe;

            var state = game.GetState();

            return state;
        }


        #region Private methods
        private async Task<IGame> GetGameAsync(int id)
        {
            var game = _allGames.Where(g => g.Id == id).FirstOrDefault();
            if (game == null)
            {
                throw new Exception("Game doesn't exist!");
            }
            return game;
        }
        #endregion
    }
}
