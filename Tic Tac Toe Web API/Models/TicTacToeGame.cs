using Microsoft.Extensions.Diagnostics.HealthChecks;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Models
{
    public class TicTacToeGame : IGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameStatus GameStatus { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int MinPlayers { get; } = 2;
        public int MaxPlayers { get; } = 2;

        //private Mark[3,3] Grid { get; set; } = new[3,3](); 
        public Mark Mark { get; set; }

        public void JoinGame( Player player)
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


    }
}
