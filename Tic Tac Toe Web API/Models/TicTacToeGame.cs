using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Numerics;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Models
{
    public class TicTacToeGame : IGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameStatus GameStatus { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int MinPlayers { get; } = 2;
        public int MaxPlayers { get; } = 2;
        public Mark[,] Grid { get; set; } = new Mark[3, 3];
        public Mark CurrentMark { get; set; } = Mark.X;

        public void JoinGame(Player player)
        {
            if (this.GameStatus == GameStatus.NotStarted && this.Players.Count == 0)
            {
                this.GameStatus = GameStatus.WaitingForOpponent;
                player.Mark = Mark.X;
                this.Players.Add(player);
            }
            else if (this.GameStatus == GameStatus.WaitingForOpponent && this.Players.Count == 1)
            {
                this.GameStatus = GameStatus.Started;
                player.Mark = Mark.O;
                this.Players.Add(player);
            }
            else
            {
                throw new Exception("Game is already started! You can not join this game!");
            }
        }

        public void MakeMove(string username, int rowPosition, int colPosition)
        {
            var mark = this.Players.Where(p => p.Name == username).FirstOrDefault().Mark;

            if (CurrentMark == mark)
            {
                if (Grid[rowPosition, colPosition] == Mark.None)
                {
                    Grid[rowPosition, colPosition] = mark;
                    if (this.CheckIfWin(mark))
                    {
                        GameStatus = GameStatus.Finished;
                    }
                    this.ChangePlayer();
                }
                else
                {
                    throw new Exception("Cell is already marked, please choose another cell!");
                }
            }
            else
            {
            throw new Exception("It is not your turn to make move!");
            }
        }

        public string ToJson()
        {
            var result = new
            {
                gameId = this.Id,
                grid = this.Grid,
                player1 = this.Players[0],
                player2 = this.Players[1],
                currentMark = this.CurrentMark,
            };
            
            return JsonConvert.SerializeObject(result);
        }

        private bool CheckIfWin(Mark mark)
        {
            if (CheckRows(mark))
            {
                return true;
            }
            else if(CheckCols(mark)) 
            {
                return true;
            }
            else if(CheckDiagonals(mark)) 
            {
                return true;
            }

            return false;
        }

        private bool CheckRows(Mark mark)
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                if (Grid[i, 0] == mark && Grid[i, 0] == Grid[i, 1] && Grid[i, 1] == Grid[i, 2])
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckCols(Mark mark)
        {
            for (int i = 0; i < Grid.GetLength(1); i++)
            {
                if (Grid[0, i] == mark && Grid[0, i] == Grid[1, i] && Grid[1, i] == Grid[2, i])
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckDiagonals(Mark mark)
        {
            if (Grid[1, 1] == mark)
            {
                if (Grid[0, 0] == Grid[1, 1] && Grid[1, 1] == Grid[2, 2])
                {
                    return true;
                }
                else if (Grid[0, 2] == Grid[1, 1] && Grid[1, 1] == Grid[2, 0])
                {
                    return true;
                }
            }
            return false;
        }

        private void ChangePlayer()
        {
            if (CurrentMark == Mark.X)
            {
                CurrentMark = Mark.O;
            }
            else if (CurrentMark == Mark.O)
            {
                CurrentMark = Mark.X;
            }
            else
            {
                throw new Exception("Something went wrong!");
            }

        }


    }


}

