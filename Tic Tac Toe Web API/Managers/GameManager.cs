using Tic_Tac_Toe_Web_API.Models.Interfaces;
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

        public List<IGame> GetAllGames()
        {
            return _allGames.ToList();
        }

        public TicTacToeGame GetGameById(int id)
        {
            var game = _allGames.Where(g => g.Id == id).Select(g => g as TicTacToeGame).FirstOrDefault();
            if (game == null)
            {
                throw new Exception("Game doesn't exist!");
            }

            return game;
        }


        public IGame CreateGame()
        {
            var game = new TicTacToeGame();
            _allGames.Add(game);

            return game;
        }

        public IGame JoinGame(int gameId, Player player)
        {
            var game = GetGame(gameId);
            game.JoinGame(player);

            return game;
        }


        public TicTacToeGame TicTacToeSelectMark(int gameId, int playerId, string playerMark)
        {
            var game = GetGame(gameId) as TicTacToeGame;

            Mark selectedMark;
            Enum.TryParse(playerMark, true, out selectedMark);

            game.SelectMark(playerId, selectedMark);

            return game;
        }

        public TicTacToeGame TicTacToeRestartGame(int gameId, int playerId)
        {

            var game = GetGame(gameId) as TicTacToeGame;

            if (playerId != game.Players[0].Id && playerId != game.Players[1].Id)
            {
                throw new UnauthorizedAccessException("Only game players can restart the game!");
            }

            game.RestartGame();

            return game;
        }

        public TicTacToeGame TicTacToeMakeMove(int gameId, int playerId, int rowPosition, int colPosition)
        {
            var game = GetGame(gameId) as TicTacToeGame;
            game.MakeMove(playerId, rowPosition, colPosition);

            return game;
        }


        #region Private methods
        private IGame GetGame(int id)
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
