using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Enums;
using System.Numerics;

namespace Tic_Tac_Toe_Web_API
{
    public class GameManager : IGameManager
    {
        private List<IGame> _allGames = new List<IGame>();

        public GameManager()
        {

        }

        public List<IGame> GetAllGames()
        {
            return _allGames;
        }

        public IGame GetGameById(int id)
        {
            var game = _allGames.Where(g => g.Id == id).FirstOrDefault();
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

        //public void AddPlayer(IGame game, Player player)
        //{
        //    if (game.Players.Count < game.MaxPlayers)
        //    {
        //        game.Players.Add(player);
        //    }
        //    else
        //    {
        //        throw new Exception("Maximum number of players is reached! Please try again later!");
        //    }
        //}

        //public IGame EnterGame(IGame game, Player player)
        //{
        //    if (game.Players.Count < 1)
        //    {
        //        game.Players.Add(player);
        //        game.GameStatus = GameStatus.WaitingForOpponent;
        //        return game;
        //    }
        //    else
        //    {
        //        throw new Exception("Players should be less than 1 to enter the game!");
        //    }
        //}
        public IGame JoinGame(int gameId, Player player)
        {
            var game = GetGameById(gameId);
            game.JoinGame(player);

            return game;
        }

        public IGame SelectMark(int gameId, int playerId, string playerMark)
        {
            var game = GetGameById(gameId);

            Mark selectedMark;
            Enum.TryParse(playerMark, true, out selectedMark);

            (game as TicTacToeGame).SelectMark(playerId, selectedMark);

            return game;
        }

        public IGame RestartGame(int gameId, string username)
        {
            var game = GetGameById(gameId);
            foreach (var player in game.Players)
            {
                if (player.Name == username)
                {
                    (game as TicTacToeGame).RestartGame();
                }
                else
                {
                    throw new UnauthorizedAccessException("Only game players can restart the game!");
                }
            }
            return game;
        }
        public IGame MakeMove(int gameId, int playerId, int rowPosition, int colPosition)
        {
            var game = GetGameById(gameId);
            if (game.Name == "Tic-Tac-Toe")
            {
                //game = (game as TicTacToeGame);
                //if (game.MakeMove(player, rowPosition, colPosition)

                (game as TicTacToeGame).MakeMove(playerId, rowPosition, colPosition);
            }

            return game;
        }


    }
}
