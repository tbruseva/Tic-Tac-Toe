using System.Numerics;
using Tic_Tac_Toe_Web_API.Enums;

namespace Tic_Tac_Toe_Web_API.Models.Interfaces
{
    public interface IGame
    {
        public GameStatus GameStatus { get; set; }
        public int Id { get; }
        public int MaxPlayers { get; }
        public int MinPlayers { get; }
        public string Name { get; set;}
        public int GameState {  get; set; }
        public List<Player> Players { get; set; }
        public Task JoinGameAsync(Player player);
        public Task JoinGameAgainstComputerAsync(Player player);
    }
}
