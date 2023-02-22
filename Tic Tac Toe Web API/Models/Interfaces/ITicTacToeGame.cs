using Tic_Tac_Toe_Web_API.Enums;

namespace Tic_Tac_Toe_Web_API.Models.Interfaces
{
    public interface ITicTacToeGame
    {
        Mark CurrentMark { get; set; }
        GameStatus GameStatus { get; set; }
        Mark[] Grid { get; set; } 
        int Id { get; set; }
        int MaxPlayers { get; }
        int MinPlayers { get; }
        string Name { get; set; }
        List<Player> Players { get; set; }

        void JoinGame(Player player, string mark);
        void MakeMove(string username, int rowPosition, int colPosition);
        string ToJson();
    }
}