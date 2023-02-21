using System.Numerics;
using Tic_Tac_Toe_Web_API.Enums;

namespace Tic_Tac_Toe_Web_API.Models.Interfaces
{
    public interface IGame
    {
        GameStatus GameStatus { get; set; }
        int Id { get; set; }
        int MaxPlayers { get; }
        int MinPlayers { get; }
        string Name { get; set; }
        List<Player> Players { get; set; }
        string ToJson();
    }
}
