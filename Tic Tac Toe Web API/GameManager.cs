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
            _allGames.Add(new TicTacToeGame() { Id = 1, Name = "Tic-Tac-Toe" });
        }

        public List<IGame> GetAllGames()
        {
            return _allGames;
        }

        public IGame GetGame(int id)
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
            return game;
        }

        public void AddPlayer(IGame game, Player player)
        {
            if (game.Players.Count < game.MaxPlayers)
            {
                game.Players.Add(player);
            }
            else
            {
                throw new Exception("Maximum number of players is reached! Please try again later!");
            }
        }

        public IGame EnterGame(IGame game, Player player)
        {
            if (game.Players.Count < 1)
            {
                game.Players.Add(player);
                game.GameStatus = GameStatus.WaitingForOpponent;
                return game;
            }
            else
            {
                throw new Exception("Players should be less than 1 to enter the game!");
            }
        }
        public IGame JoinGame(int gameId, Player player)
        {
            var game = GetGame(gameId);

            if (game.Name == "Tic-Tac-Toe")
            {
                (game as TicTacToeGame).JoinGame(player);
            }

            return game;
        }

        public Player SelectFirstOrSecondPlayer(int gameId, string username, string playerMark)
        {
            var game = GetGame(gameId);
            var player = game.Players.Where(p => p.Name == username).FirstOrDefault();

            if (player == null)
            {
                throw new UnauthorizedAccessException("Please enter the game first!");
            }

            if (game.Name == "Tic-Tac-Toe")
            {
                if (game.Players[0].Name == username)
                {
                    Mark selectedMark;
                    if (Enum.TryParse(playerMark, true, out selectedMark))
                    {
                        player.Mark = selectedMark;
                        if (selectedMark == Mark.O)
                        {
                            game.Players[1].Mark = Mark.X;
                        }
                    }
                    else
                    {
                        throw new InvalidDataException("Entered symbol is not valid! Please select X or O !");
                    }
                }
                else if (game.Players[1].Name == username)
                {
                    throw new UnauthorizedAccessException("Only first player entered the game can select a mark!");
                }
            }

            return player;
        }

        public IGame MakeMove(int gameId, string username, int rowPosition, int colPosition)
        {
            var game = GetGame(gameId);
            if (game.Name == "Tic-Tac-Toe")
            {
                //game = (game as TicTacToeGame);
                //if (game.MakeMove(player, rowPosition, colPosition)

               (game as TicTacToeGame).MakeMove(username, rowPosition, colPosition);
            }

            return game;
        }


        //public IGame MakeMove(int id)
        //{
        //    var game = GetGame(id);
        //    if (game.Name == "TicTacToe")
        //    {
        //        TicTacToeGame.MakeMove(game);
        //    }
        //    else if (game.Name == Chess)
        //    {

        //    }
        //    return game;
        //}
    }
}
