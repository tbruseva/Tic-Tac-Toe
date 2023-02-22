using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models;

namespace Tic_Tac_Toe_Web_API.Dtos
{
    public class TicTacToeResponseDto
    {
        public int GameId { get; set; }
        public Mark[,] Grid { get; set; } = new Mark[3, 3];
        public Player? PlayerX { get; set; }
        public Player? PlayerO { get; set; }
        public Mark CurrentMark { get; set; }

    }
}
