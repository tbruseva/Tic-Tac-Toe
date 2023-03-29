using Tic_Tac_Toe_Web_API.Enums;

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
                || (Players.Any(p => p.Id == Player.Computer.Id && this.Grid.All(m => m == Mark.None)))))
            {
                this.PlayerXIndex = mark == Mark.X ? 0 : 1;
                this.PlayerOIndex = mark == Mark.X ? 1 : 0;
                GameState++;

                if (mark == Mark.O)
                {
                    await this.ComputerAddPawnAsync();
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
                (PlayerXPawns > 0 || PlayerOPawns > 0))
            {
                var mark = await GetMarkByPlayerAsync(player.Id);

                if (CheckAvailablePosition(mark, position))
                {
                    Grid[position] = mark;
                    DecresePaws(mark);
                }
                else
                {
                    throw new Exception("Cell is already marked or you can not add a pawn on that position! Please choose another cell!");
                }

                CurrentPlayerIndex = (CurrentPlayerIndex == 0) ? 1 : 0;
            }
            else
            {
                throw new Exception("It is not your turn to add a pawn or your pawns have finished!");
            }
        }

        public async Task AddPawnAgainstComputerAsync(int playerId, int position)
        {
            await AddPawnAsync(playerId, position);
            await ComputerAddPawnAsync();
        }

        public async Task ComputerAddPawnAsync()
        {
            var mark = await GetMarkByPlayerAsync(Player.Computer.Id);
            Random random = new Random();
            int position;
            do
            {
                position = random.Next(0, 9);
            }
            while (!CheckAvailablePosition(mark, position));

            await this.AddPawnAsync(Player.Computer.Id, position);
        }

        public override async Task MakeMoveAsync(int playerId, int oldPosition, int newPosition)
        {
            var player = this.Players.Where(p => p.Id == playerId).FirstOrDefault();
            if (player == null)
            {
                throw new UnauthorizedAccessException("Please join another game!");
            }

            if (PlayerXPawns > 0 || PlayerOPawns > 0)
            {
                throw new Exception("First add all pawns!");
            }

            var currentPlayerId = this.Players[CurrentPlayerIndex].Id;

            if (currentPlayerId == player.Id && GameStatus == GameStatus.Started)
            {
                var mark = await GetMarkByPlayerAsync(player.Id);

                if (Grid[newPosition] == Mark.None &&
                    (newPosition == GetOffsetPosition(oldPosition, - 1) || newPosition == GetOffsetPosition(oldPosition, 1) || oldPosition == 0 || newPosition == 0))
                {
                    Grid[newPosition] = mark;
                    Grid[oldPosition] = Mark.None;
                    GameState++;

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
                    if (player == Player.Computer)
                    {
                        await ComputerMakeMoveAsync();
                        return;
                    }
                    throw new Exception("Please choose another cell!");
                }

                CurrentPlayerIndex = (CurrentPlayerIndex == 0) ? 1 : 0;
            }
            else if (GameStatus == GameStatus.Finished)
            {
                throw new Exception("Game has finished!");
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
            var positions = await ComputerCalcPositions();
            await MakeMoveAsync(Player.Computer.Id, positions.oldPosition, positions.newPosition);
        }


        public override async Task RestartGameAsync()
        {
            GameStatus = GameStatus.Started;
            Grid = new Mark[9];
            WinCells.Clear();
            CurrentPlayerIndex = 0;
            PlayerXPawns = 3;
            PlayerOPawns = 3;
            GameState++;
        }

        #region Private methods

        private bool CheckAvailablePosition(Mark mark, int position)
        {
            return (Grid[position] == Mark.None && CheckForLineOfThree(position, mark) == false);
        }

        private bool CheckForLineOfThree(int position, Mark mark)
        {
            var leftLine   = Grid[GetOffsetPosition(position, -2)] == mark && Grid[GetOffsetPosition(position, -1)] == mark;
            var rightLine  = Grid[GetOffsetPosition(position,  2)] == mark && Grid[GetOffsetPosition(position,  1)] == mark;
            var middleLine = Grid[GetOffsetPosition(position, -1)] == mark && Grid[GetOffsetPosition(position,  1)] == mark;

            return leftLine || rightLine || middleLine;
        }

        private int GetOffsetPosition(int current, int offset)
        {
            if (current == 0)
            {
                return current;
            }

            var result = current + offset;
            if (result > 8)
            {
                return result - 8;
            }
            else if (result < 1)
            {
                return 8 - Math.Abs(result);
            }
            return result;
        }

        private async Task<(int oldPosition, int newPosition)> ComputerCalcPositions()
        {
            var mark = await this.GetMarkByPlayerAsync(Player.Computer.Id);

            var markedPositions = Grid
            .Select((mark, index) => new { mark, index })
            .Where(m => m.mark == mark)
            .Select(m => m.index)
            .ToList();

            Random random = new Random();
            var positionInMarkedPositions = random.Next(0, markedPositions.Count);
            int oldPosition = markedPositions[positionInMarkedPositions];
            var possibleNewPositions = new List<int>();
            do
            {
                if (oldPosition == 0)
                {
                    for (int i = 1; i < Grid.Length; i++)
                    {
                        if (Grid[i] == Mark.None)
                        {
                            possibleNewPositions.Add(i);
                        }
                    }
                }
                else if (oldPosition == 1 && (Grid[0] == Mark.None || Grid[2] == Mark.None && Grid[8] == Mark.None))
                {
                    possibleNewPositions.Add(0);
                    possibleNewPositions.Add(2);
                    possibleNewPositions.Add(8);
                }
                else if (oldPosition == 8 && (Grid[0] == Mark.None || Grid[1] == Mark.None || Grid[7] == Mark.None))
                {
                    possibleNewPositions.Add(0);
                    possibleNewPositions.Add(1);
                    possibleNewPositions.Add(7);
                }
                else if (Grid[oldPosition - 1] == Mark.None || Grid[oldPosition + 1] == Mark.None || Grid[0] == Mark.None)
                {
                    possibleNewPositions.Add(0);
                    possibleNewPositions.Add(oldPosition - 1);
                    possibleNewPositions.Add(oldPosition + 1);
                }
            }
            while (possibleNewPositions.Count == 0);
            {
                positionInMarkedPositions = random.Next(0, markedPositions.Count);
                oldPosition = markedPositions[positionInMarkedPositions];
            }

            var positionInpossibleNewPositions = random.Next(0, possibleNewPositions.Count);
            int newPosition = possibleNewPositions[positionInpossibleNewPositions];

            while (Grid[newPosition] != Mark.None)
            {
                positionInpossibleNewPositions = random.Next(0, possibleNewPositions.Count);
                newPosition = possibleNewPositions[positionInpossibleNewPositions];
            }

            return (oldPosition, newPosition);
        }

        private void DecresePaws(Mark mark)
        {
            if (mark == Mark.X)
            {
                PlayerXPawns--;
                GameState++;
            }
            else
            {
                PlayerOPawns--;
                GameState++;
            }
        }


        #endregion
    }
}
