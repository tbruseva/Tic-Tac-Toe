using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Models
{
    public class RomanTicTacToeGame : IGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int MinPlayers { get; set; } = 2;
        public int MaxPlayers { get; set; } = 2;

        public int PlayerXIndex { get; set; }
        public int PlayerOIndex { get; set; }
        public int CurrentPlayerIndex { get;set; }
        public GameStatus GameStatus { get; set; }
        public Mark[] Grid { get; set; } = new Mark[9];
        public List<int> WinCells { get; set; } = new List<int>();
        public Dictionary<int, int> CounterWins { get; set; }
        public int CounterTotalGames { get; set; }
        public int GameState { get; set; } = 0;
        public Task JoinGameAgainstComputerAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public Task JoinGameAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public Task MakeMoveAsync(int playerId, int position)
        {
            throw new NotImplementedException();
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
    }
}
