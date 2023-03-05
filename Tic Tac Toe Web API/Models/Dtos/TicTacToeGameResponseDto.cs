using Tic_Tac_Toe_Web_API.Enums;

namespace Tic_Tac_Toe_Web_API.Models.Dtos
{
    public class TicTacToeGameResponseDto
    {
        public int GameId { get; set; }
        public Mark Grid { get; set; }
        public Player? PlayerX { get; set; }
        public Player? PlayerO { get; set; }
        public int? CurrentPlayerId { get; set; }
    }
}
