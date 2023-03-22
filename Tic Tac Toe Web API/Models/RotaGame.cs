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
            Name = "Roman-Tic-Tac-Toe";
        }

        public Task JoinGameAgainstComputerAsync(Player player)
        {
            throw new NotImplementedException();
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

        #region Private methods

        #endregion
    }
}
