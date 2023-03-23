using System.Xml;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Models
{
    public class RotaGame : TicTacToe
    {
        public int PlayerXPawns { get; set; } = 3;
        public int PlayerOPawns { get; set; } = 3;

        public override List<List<int>> WinningCombinations { get; set; } = new List<List<int>> {
            new List<int> { 1, 2, 3 },
            new List<int> { 2, 3, 4 },
            new List<int> { 3, 4, 5 },
            new List<int> { 4, 5, 6 },
            new List<int> { 6, 7, 8 },
            new List<int> { 1, 2, 8 }
        };


        public RotaGame()
        {
            Name = "Rota";
        }

        public override async Task SelectMarkAsync(int playerId, Mark mark)
        {
            if (mark != Mark.X && mark != Mark.O)
            {
                throw new InvalidDataException("Entered symbol is not valid! Please select X or O !");
            }
            if (!Players.Exists(p => p.Id == playerId))
            {
                throw new UnauthorizedAccessException("Please enter the game first!");
            }

            if (Players.Any(p => p.Id == playerId) && (GameStatus == GameStatus.WaitingForOpponent
                || GameStatus == GameStatus.Finished
                || Players.Any(p => p.Id == Player.Computer.Id)))
            {
                this.PlayerXIndex = mark == Mark.X ? 0 : 1;
                this.PlayerOIndex = mark == Mark.X ? 1 : 0;
                GameState++;

                if (mark == Mark.O)
                {
                    Random random= new Random();
                    var position = random.Next(0, 9);
                    await this.AddPawnAsync(Player.Computer.Id, position);
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

        public override async Task MakeMoveAsync(int playerId, int oldPosition, int newPosition)
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

        public override async Task MakeMoveAgainstComputerAsync(int playerId, int oldPosition, int newPosition)
        {
            await MakeMoveAsync(playerId, oldPosition, newPosition);
            await ComputerMakeMoveAsync();
        }

        public override async Task ComputerMakeMoveAsync()
        {
            var positions = await ComputerCalcPosition();
            await MakeMoveAsync(Player.Computer.Id, positions.oldPosition, positions.newPosition);
        }

        #region Private methods
        private async Task<(int oldPosition, int newPosition)> ComputerCalcPosition()
        {
            var mark = await GetMarkByPlayerAsync(Player.Computer.Id);
            List<int> positionsComputerMarks = new List<int>();
            List<Mark> possibleNewPositions = new List<Mark>();
            for (int i = 0; i < Grid.Length; i++)
            {
                if (Grid[i] == mark)
                {
                    positionsComputerMarks.Add(i);
                }
            }
            Random random = new Random();
            var oldPosition = random.Next(0, positionsComputerMarks.Count);
            while (Grid[oldPosition - 1] != Mark.None || Grid[oldPosition + 1] != Mark.None || Grid[0] != Mark.None)
            {
                oldPosition = random.Next(0, positionsComputerMarks.Count);
            }
            possibleNewPositions.Add(Grid[oldPosition-1]);
            possibleNewPositions.Add(Grid[oldPosition+1]);
            possibleNewPositions.Add(Grid[0]);

            var newPosition = random.Next(0, possibleNewPositions.Count);
            while (Grid[newPosition] != Mark.None)
            {
                newPosition = random.Next(0, positionsComputerMarks.Count);
            }

            return (oldPosition, newPosition);
        }
        #endregion
    }
}
