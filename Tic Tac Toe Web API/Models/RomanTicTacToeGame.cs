using System.Xml;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Models
{
    public class RomanTicTacToeGame : TicTacToe, IGame
    {
        //public string Name { get; } = "Roman-Tic-Tac-Toe";
        public List<Player> Players { get; set; } = new List<Player>();
        public int MinPlayers { get; set; } = 2;
        public int MaxPlayers { get; set; } = 2;
        public int CurrentPlayerIndex { get; set; }
        public GameStatus GameStatus { get; set; }
        public Mark[] Grid { get; set; } = new Mark[9];
        public int PlayerXPawns { get; set; } = 3;
        public int PlayerOPawns { get; set; } = 3;
        public List<int> WinCells { get; set; } = new List<int>();
        public Dictionary<int, int> CounterWins { get; set; } = new Dictionary<int, int>();
        public int CounterTotalGames { get; set; }
        public int GameState { get; set; } = 0;
        public List<List<int>> WinningCombinations { get; set; } = new List<List<int>> {
            new List<int> { 1, 2, 3 },
            new List<int> { 2, 3, 4 },
            new List<int> { 3, 4, 5 },
            new List<int> { 4, 5, 6 },
            new List<int> { 6, 7, 8 },
            new List<int> { 1, 2, 8 }};

        public int PlayerXIndex = 0;
        public int PlayerOIndex = 1;


        public RomanTicTacToeGame()
        {
            Name = "Roman-Tic-Tac-Toe";
        }

        public Task JoinGameAgainstComputerAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public async Task JoinGameAsync(Player player)
        {
            if (this.GameStatus == GameStatus.NotStarted && this.Players.Count == 0)
            {
                this.GameStatus = GameStatus.WaitingForOpponent;
                this.Players.Add(player);
                CounterWins.Add(player.Id, 0);
                GameState++;
            }
            else if (this.GameStatus == GameStatus.WaitingForOpponent && this.Players.Count == 1)
            {
                this.GameStatus = GameStatus.Started;
                this.Players.Add(player);
                CounterWins.Add(player.Id, 0);

                GameState++;
            }
            else
            {
                throw new Exception("Game is already started! You can not join this game!");
            }
        }

        public async Task AddPawnAsync(int playerId, int position)
        {
            var player = this.Players.Where(p => p.Id == playerId).FirstOrDefault();
            if (player == null)
            {
                throw new UnauthorizedAccessException("Please join another game!");
            }
            var currentPlayerId = this.Players[CurrentPlayerIndex].Id;
            if (currentPlayerId == player.Id && GameStatus == GameStatus.Started &&
                PlayerXPawns > 0 && PlayerOPawns > 0)
            {
                var mark = await GetMarkByPlayerAsync(player.Id);
                if (Grid[position] == Mark.None)
                {
                    Grid[position] = mark;
                    PlayerXPawns = (mark == Mark.X) ? PlayerXPawns-- : PlayerOPawns--;
                    GameState++;
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
                throw new Exception("It is not your turn to add a pawn or your pawns have finished!");
            }
        }

        public async Task MakeMoveAsync(int playerId, int oldPosition, int newPosition)
        {
            var player = this.Players.Where(p => p.Id == playerId).FirstOrDefault();
            if (player == null)
            {
                throw new UnauthorizedAccessException("Please join another game!");
            }
            var currentPlayerId = this.Players[CurrentPlayerIndex].Id;

            if (currentPlayerId == player.Id && GameStatus == GameStatus.Started)
            {
                var mark = await GetMarkByPlayerAsync(player.Id);

                if (Grid[newPosition] == Mark.None && (newPosition == oldPosition - 1 || newPosition == oldPosition + 1 || oldPosition == 0 || newPosition == 0))
                {
                    Grid[newPosition] = mark;
                    Grid[oldPosition] = Mark.None;
                    GameState++; ;

                    if (await this.CheckIfWinAsync(mark))
                    {
                        CounterWins[player.Id]++;
                        GameStatus = GameStatus.Finished;
                        CounterTotalGames++;
                        GameState = 0;
                    }
                }
                else
                {
                    throw new Exception("Please choose another cell!");
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

            if (Players[0].Id == playerId && (GameStatus == GameStatus.WaitingForOpponent || GameStatus == GameStatus.Finished))
            {
                this.PlayerXIndex = mark == Mark.X ? 0 : 1;
                this.PlayerOIndex = mark == Mark.X ? 1 : 0;
                GameState++;

                if (mark == Mark.O)
                {
                    GameStatus = GameStatus.Started;
                    //await this.ComputerMakeMoveAsync();
                    GameState++;
                }
            }
            else if (Players[1].Id == playerId)
            {
                throw new UnauthorizedAccessException("Only first player entered the game can select a mark!");
            }
        }

        public async Task RestartGameAsync()
        {
            GameStatus = GameStatus.Started;
            Grid = new Mark[9];
            WinCells.Clear();
            CurrentPlayerIndex = 0;
            GameState++;
        }

        public int GetState()
        {
            return GameState;
        }

        #region Private methods
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
        #endregion
    }
}
