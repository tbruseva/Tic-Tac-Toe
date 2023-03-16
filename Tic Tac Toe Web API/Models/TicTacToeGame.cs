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
        public int GameState { get; set; } = 0;
        public List<List<int>> WinningCombinations { get; set; } = new List<List<int>> {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 5 },
            new List<int> { 6, 7, 8 },
            new List<int> { 0, 3, 6 },
            new List<int> { 1, 4, 7 },
            new List<int> { 2, 5, 8 },
            new List<int> { 0, 4, 8 },
            new List<int> { 2, 4, 6 } };
        public List<int> WinCells { get; set; } = new List<int>();

        public Dictionary<int, int> counterWins = new Dictionary<int, int>();
        //public int CounterWinX = 0;
        //public int CounterWinO = 0;
        public int CounterTotal = 0;
        public int CurrentPlayerIndex = 0;
        public int PlayerXIndex = 0;
        public int PlayerOIndex = 1;



        public TicTacToeGame()
        {
            Id = Interlocked.Increment(ref uniqueId);
            Name = "Tic-Tac-Toe";
        }

        public async Task JoinGameAsync(Player player)
        {
            if (this.GameStatus == GameStatus.NotStarted && this.Players.Count == 0)
            {
                this.GameStatus = GameStatus.WaitingForOpponent;
                this.Players.Add(player);
                counterWins.Add(player.Id, 0);
                GameState++;
            }
            else if (this.GameStatus == GameStatus.WaitingForOpponent && this.Players.Count == 1)
            {
                this.GameStatus = GameStatus.Started;
                this.Players.Add(player);
                counterWins.Add(player.Id, 0);

                GameState++;
            }
            else
            {
                throw new Exception("Game is already started! You can not join this game!");
            }
        }

        public async Task JoinGameAgainstComputerAsync(Player player)
        {
            if (this.GameStatus == GameStatus.NotStarted && this.Players.Count == 0)
            {
                this.GameStatus = GameStatus.LoadedAgainstComputer;
                this.Players.Add(player);
                this.Players.Add(Player.Computer);
                counterWins.Add(player.Id, 0);
                counterWins.Add(Player.Computer.Id, 0);

                GameState++;
            }
            else
            {
                throw new Exception("Game is already started! You can not join this game!");
            }
        }

        public async Task MakeMoveAsync(int playerId, int rowPosition, int colPosition)
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
                        GameStatus = GameStatus.Finished;
                        counterWins[player.Id]++;
                        CounterTotal++;
                        GameState++;
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
            var player = this.Players.Where(p => p.Id == playerId && p.Id != 0).FirstOrDefault();

            if (player == null)
            {
                throw new InvalidDataException("Please enter valid player Id!");
            }

            var position = await this.CalculatePositionAsync(rowPosition, colPosition);

            if (GameStatus == GameStatus.Started)
            {
                var mark = await GetMarkByPlayerAsync(player.Id);

                if (Grid[position] == Mark.None)
                {
                    Grid[position] = mark;
                    GameState++; ;

                    if (await this.CheckIfWinAsync(mark))
                    {
                        GameStatus = GameStatus.Finished;
                        counterWins[player.Id]++;
                        CounterTotal++;
                        GameState++;
                    }
                    else if (!Grid.Contains(Mark.None))
                    {
                        CounterTotal++;
                        GameState++;
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

                await ComputerMakeMoveAsync();
            }
            else
            {
                throw new Exception("It is not your turn to make move or still waiting for opponent!");
            }
        }

        public async Task ComputerMakeMoveAsync()
        {
            var mark = await GetMarkByPlayerAsync(Player.Computer.Id);
            Random random = new Random();
            var position = random.Next(0, 8);
            while (Grid[position] != Mark.None)
            {
                position = random.Next(0, 8);
            }
            Grid[position] = mark;
        }
        public async Task RestartGameAsync()
        {
            GameStatus = GameStatus.Started;
            Grid = new Mark[9];
            WinCells.Clear();
            CurrentPlayerIndex = 0;
            GameState++;
        }

        private async Task<Mark> GetMarkByPlayerAsync(int playerId)
        {
            if (Players[PlayerXIndex].Id == playerId)
            {
                return Mark.X;
            }

            return Mark.O;
        }
        private async Task<bool> CheckIfWinAsync(Mark mark)
        {
            foreach (var list in WinningCombinations)
            {
                if (Grid[list[0]] != Mark.None && (Grid[list[0]] == Grid[list[1]]) && (Grid[list[0]] == Grid[list[2]]))
                {
                    WinCells.AddRange(list);
                    GameState++;

                    return true;
                }

            }

            return false;
        }

        private async Task<int> CalculatePositionAsync(int row, int col)
        {
            var position = (row * 3) + col;
            return position;
        }

        public async Task SelectMarkAsync(int playerId, Mark mark)
        {
            if (mark != Mark.X && mark != Mark.O)
            {
                throw new InvalidDataException("Entered symbol is not valid! Please select X or O !");
            }
            if (!Players.Exists(p => p.Id == playerId))
            {
                throw new UnauthorizedAccessException("Please enter the game first!");
            }

            if ((Players[0].Id == playerId && GameStatus == GameStatus.WaitingForOpponent) ||
                 (Players.Exists(p => p.Id == 0) && GameStatus == GameStatus.LoadedAgainstComputer))
            {
                this.PlayerXIndex = mark == Mark.X ? 0 : 1;
                this.PlayerOIndex = mark == Mark.X ? 1 : 0;
                GameState++;

                if (mark == Mark.O)
                {
                    GameStatus = GameStatus.Started;
                    await this.ComputerMakeMoveAsync();
                    GameState++;
                }
            }
            else if (Players[1].Id == playerId)
            {
                throw new UnauthorizedAccessException("Only first player entered the game can select a mark!");
            }
        }

        public int GetState()
        {
            return GameState;
        }
    }
}

