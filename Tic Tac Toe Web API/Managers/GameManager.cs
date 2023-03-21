﻿using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Enums;
using System.Numerics;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Mappers;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;

namespace Tic_Tac_Toe_Web_API.Managers
{
    public class GameManager : IGameManager
    {
        private List<IGame> _allGames = new List<IGame>();

        public GameManager()
        {

        }

        public async Task<List<IGame>> GetAllGamesAsync()
        {
            return _allGames.ToList();
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
            IGame game = (name == "Tic-Tac-Toe") ? new TicTacToeGame() : new RomanTicTacToeGame();
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

        public async Task<RomanTicTacToeGame> AddPawnAsync(int gameId,int playerId, int position)
        {
            var game = await GetGameAsync(gameId) as RomanTicTacToeGame;
            await game.AddPawnAsync(playerId, position);

            return game;
        }

        public async Task<TicTacToeGame> TicTacToeSelectMarkAsync(int gameId, int playerId, string playerMark)
        {
            var game = await GetGameAsync(gameId) as TicTacToeGame;
            if (game == null)
            {
                throw new InvalidDataException("Game with Id {gameId} doesn't exist!");
            }

            Mark selectedMark;
            Enum.TryParse(playerMark, true, out selectedMark);

            await game.SelectMarkAsync(playerId, selectedMark);

            return game;
        }

        public async Task<RomanTicTacToeGame> RomanTicTacToeSelectMarkAsync(int gameId, int playerId, string playerMark)
        {
            var game = await GetGameAsync(gameId) as RomanTicTacToeGame;
            if (game == null)
            {
                throw new InvalidDataException("Game with Id {gameId} doesn't exist!");
            }

            Mark selectedMark;
            Enum.TryParse(playerMark, true, out selectedMark);

            await game.SelectMarkAsync(playerId, selectedMark);

            return game;
        }

        public async Task<TicTacToeGame> TicTacToeRestartGameAsync(int gameId, int playerId)
        {

            var game = await GetGameAsync(gameId) as TicTacToeGame;
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

        public async Task<RomanTicTacToeGame> RomanTicTacToeRestartGameAsync(int gameId, int playerId)
        {

            var game = await GetGameAsync(gameId) as RomanTicTacToeGame;
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

        public async Task<RomanTicTacToeGame> RomanTicTacToeMakeMoveAsync(int gameId, int playerId, int oldPosition, int newPosition)
        {
            var game = await GetGameAsync(gameId) as RomanTicTacToeGame;
            if (game == null)
            {
                throw new InvalidDataException($"Game with Id {gameId} doesn't exist!");
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

        public async Task<int> TicTacToeGetGameStateAsync(int gameId)
        {
            var game = await GetGameAsync(gameId) as TicTacToeGame;

            var state = game.GetState();

            return state;
        }
        public async Task<int> RomanTicTacToeGetGameStateAsync(int gameId)
        {
            var game = await GetGameAsync(gameId) as TicTacToeGame;

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
