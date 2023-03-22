using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Models
{
    public abstract class TicTacToe : IGame
    {
        private static int uniqueId;
        public int Id { get; }
        public string Name { get; set; }
        public Mark[] Grid { get; set; } = new Mark[9];
        public int MaxPlayers { get; } = 2;
        public int MinPlayers { get; } = 2;
        public List<Player> Players { get; set; } = new List<Player>();
        public GameStatus GameStatus { get; set; }
        public abstract List<List<int>> WinningCombinations { get; set; }
        public List<int> WinCells { get; set; } = new List<int>();
        public Dictionary<int, int> CounterWins { get; set; } = new Dictionary<int, int>();
        public int CounterTotalGames { get; set; }
        public int GameState { get; set; } = 0;

        public int PlayerXIndex = 0;
        public int PlayerOIndex = 1;
        public int CurrentPlayerIndex = 0;

        public TicTacToe()
        {
            Id = Interlocked.Increment(ref uniqueId);
        }

        public virtual async Task JoinGameAsync(Player player)
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

        public Task JoinGameAgainstComputerAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public abstract Task MakeMoveAsync(int playerId, int rowPosition, int colPosition);

        public virtual async Task RestartGameAsync()
        {
            GameStatus = GameStatus.Started;
            Grid = new Mark[9];
            WinCells.Clear();
            CurrentPlayerIndex = 0;
            GameState++;
        }


        public virtual async Task SelectMarkAsync(int playerId, Mark mark)
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
            else
            {
                throw new AccessViolationException("You cannot select a mark after the game has started!");
            }
        }

        public int GetState()
        {
            return GameState;
        }

        protected virtual async Task<bool> CheckIfWinAsync(Mark mark)
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

        protected virtual async Task<Mark> GetMarkByPlayerAsync(int playerId)
        {
            if (Players[PlayerXIndex].Id == playerId)
            {
                return Mark.X;
            }

            return Mark.O;
        }
    }
}
