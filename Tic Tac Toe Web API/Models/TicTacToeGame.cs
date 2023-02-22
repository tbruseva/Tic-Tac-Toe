using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Numerics;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Models
{
    public class TicTacToeGame : IGame, ITicTacToeGame
    {
        private static int uniqueId;
        public int Id { get; set; }
        public string Name { get; set; }
        public GameStatus GameStatus { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int MinPlayers { get; } = 2;
        public int MaxPlayers { get; } = 2;
        public Mark[] Grid { get; set; } = new Mark[9];
        public Mark CurrentMark { get; set; } = Mark.X;

        public TicTacToeGame()
        {
            Id = ++uniqueId;
            Name = "Tic-Tac-Toe";
        }
        public void JoinGame(Player player, string mark)
        {
            if (this.GameStatus == GameStatus.NotStarted && this.Players.Count == 0)
            {
                this.GameStatus = GameStatus.WaitingForOpponent;
                if (string.IsNullOrWhiteSpace(mark))
                {
                player.Mark = Mark.X;
                }
                else
                {
                    Mark selectedMark;
                    Enum.TryParse(mark, true, out selectedMark);
                    player.Mark = selectedMark;
                }

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
            var position = this.CalculatePosition(rowPosition, colPosition);

            if (CurrentMark == mark)
            {
                if (Grid[position] == Mark.None)
                {
                    Grid[position] = mark;
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
            if ((Grid[0] == mark && Grid[1] == mark && Grid[2] == mark) ||
        (Grid[3] == mark && Grid[4] == mark && Grid[5] == mark) ||
        (Grid[6] == mark && Grid[7] == mark && Grid[8] == mark) ||
        (Grid[0] == mark && Grid[3] == mark && Grid[6] == mark) ||
        (Grid[1] == mark && Grid[4] == mark && Grid[7] == mark) ||
        (Grid[2] == mark && Grid[5] == mark && Grid[8] == mark) ||
        (Grid[0] == mark && Grid[4] == mark && Grid[8] == mark) ||
        (Grid[2] == mark && Grid[4] == mark && Grid[6] == mark))
            {
                return true;
            }

            return false;
        }

        private int CalculatePosition(int row, int col)
        {
            var position = (row * 3) + col;
            return position;
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

