using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Numerics;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models.Interfaces;
namespace Tic_Tac_Toe_Web_API.Models
{
    public class TicTacToeGame : IGame
    {
        private static int uniqueId;
        public int Id { get; set; }
        public string Name { get; set; }
        public GameStatus GameStatus { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int MinPlayers { get; } = 2;
        public int MaxPlayers { get; } = 2;
        public Mark[] Grid { get; set; } = new Mark[9];
        public int CounterWinX = 0;
        public int CounterWinO = 0;
        public int CounterDraw = 0;
        public int CurrentPlayerIndex = 0;
        public int PlayerXIndex = 0;
        public int PlayerOIndex = 1;



        public TicTacToeGame()
        {
            Id = ++uniqueId;
            Name = "Tic-Tac-Toe";
        }

        public void JoinGame(Player player)
        {
            if (this.GameStatus == GameStatus.NotStarted && this.Players.Count == 0)
            {
                this.GameStatus = GameStatus.WaitingForOpponent;
                this.Players.Add(player);
            }
            else if (this.GameStatus == GameStatus.WaitingForOpponent && this.Players.Count == 1)
            {
                this.GameStatus = GameStatus.Started;
                this.Players.Add(player);
            }
            else
            {
                throw new Exception("Game is already started! You can not join this game!");
            }
        }

        public void MakeMove(int playerId, int rowPosition, int colPosition)
        {
            var player = this.Players.Where(p => p.Id == playerId).FirstOrDefault();
            if (player == null)
            {
                throw new UnauthorizedAccessException("Please join another game!");
            }
            var currentPlayerId = this.Players[CurrentPlayerIndex].Id;

            var position = this.CalculatePosition(rowPosition, colPosition);

            if (currentPlayerId == player.Id && GameStatus == GameStatus.Started)
            {
                var mark = GetMarkByPlayer(player.Id);

                if (Grid[position] == Mark.None)
                {
                    Grid[position] = mark;

                    if (this.CheckIfWin(mark))
                    {
                        GameStatus = GameStatus.Finished;
                        if (mark == Mark.X)
                        {
                            CounterWinX++;
                        }
                        else if (mark == Mark.O)
                        {
                            CounterWinO++;
                        }
                    }
                    else if (!Grid.Contains(Mark.None))
                    {
                        CounterDraw++;
                    }
                }
                else
                {
                    throw new Exception("Cell is already marked, please choose another cell!");
                }

                if (CurrentPlayerIndex == 0)
                {
                    CurrentPlayerIndex = 1;
                }
                else if (CurrentPlayerIndex == 1)
                {
                    CurrentPlayerIndex = 0;
                }
            }
            else
            {
                throw new Exception("It is not your turn to make move or still waiting for opponent!");
            }
        }

        private Mark GetMarkByPlayer(int playerId)
        {
            if (Players[PlayerXIndex].Id == playerId)
            {
                return Mark.X;
            }

            return Mark.O;
        }

        public void RestartGame()
        {
            GameStatus = GameStatus.Started;
            Grid = new Mark[9];
            CurrentPlayerIndex = 0;
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

        public void SelectMark(int playerId, Mark mark)
        {
            if (mark != Mark.X && mark != Mark.O)
            {
                throw new InvalidDataException("Entered symbol is not valid! Please select X or O !");
            }
            if (!Players.Exists(p => p.Id == playerId))
            {
                throw new UnauthorizedAccessException("Please enter the game first!");
            }

            if (Players[0].Id == playerId)
            {
                this.PlayerXIndex = mark == Mark.X ? 0 : 1;
                this.PlayerOIndex = mark == Mark.X ? 1 : 0;
            }
            else if (Players[1].Id == playerId)
            {
                throw new UnauthorizedAccessException("Only first player entered the game can select a mark!");
            }
        }



    }


}

