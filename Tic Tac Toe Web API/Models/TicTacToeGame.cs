using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Numerics;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models.Interfaces;
namespace Tic_Tac_Toe_Web_API.Models
{
    public class TicTacToeGame : TicTacToe
    {
        public override List<List<int>> WinningCombinations { get; set; } = new List<List<int>> {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 5 },
            new List<int> { 6, 7, 8 },
            new List<int> { 0, 3, 6 },
            new List<int> { 1, 4, 7 },
            new List<int> { 2, 5, 8 },
            new List<int> { 0, 4, 8 },
            new List<int> { 2, 4, 6 } 
        };

        public TicTacToeGame()
        {
            Name = "Tic-Tac-Toe";
        }

        public async Task JoinGameAgainstComputerAsync(Player player)
        {
            if (this.GameStatus == GameStatus.NotStarted && this.Players.Count == 0)
            {
                this.GameStatus = GameStatus.Started;
                this.Players.Add(player);
                this.Players.Add(Player.Computer);
                CounterWins.Add(player.Id, 0);
                CounterWins.Add(Player.Computer.Id, 0);

                GameState++;
            }
            else
            {
                throw new Exception("Game is already started! You can not join this game!");
            }
        }

        public override async Task MakeMoveAsync(int playerId, int rowPosition, int colPosition)
        {
            var player = this.Players.Where(p => p.Id == playerId).FirstOrDefault();
            if (player == null)
            {
                throw new UnauthorizedAccessException("Please join another game!");
            }
            var currentPlayerId = this.Players[CurrentPlayerIndex].Id;

            var position = await this.CalculatePositionAsync(rowPosition, colPosition);

            if (currentPlayerId == player.Id && GameStatus == GameStatus.Started)
            {
                var mark = await GetMarkByPlayerAsync(player.Id);

                if (Grid[position] == Mark.None)
                {
                    Grid[position] = mark;
                    GameState++; ;

                    if (await this.CheckIfWinAsync(mark))
                    {
                        CounterWins[player.Id]++;
                        GameStatus = GameStatus.Finished;
                        CounterTotalGames++;
                        GameState = 0;
                    }
                    else if (!Grid.Contains(Mark.None))
                    {
                        GameStatus = GameStatus.Finished;
                        CounterTotalGames++;
                        GameState = 0;
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

        public async Task MakeMoveAgainstComputerAsync(int playerId, int rowPosition, int colPosition)
        {
            await MakeMoveAsync(playerId, rowPosition, colPosition);
            await ComputerMakeMoveAsync();
        }

        private async Task ComputerMakeMoveAsync()
        {
            var position = ComputerCalcPosition();
            var row = (int)(position / 3);
            var col = (int)(position % 3);
            await MakeMoveAsync(Player.Computer.Id, row, col);
        }

        private int ComputerCalcPosition()
        {
            Random random = new Random();
            var position = random.Next(0, 8);
            while (Grid[position] != Mark.None)
            {
                position = random.Next(0, 8);
            }
            return position;
        }
        

        private async Task<int> CalculatePositionAsync(int row, int col)
        {
            var position = (row * 3) + col;
            return position;
        }

    }
}

