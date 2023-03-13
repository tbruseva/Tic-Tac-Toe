using Tic_Tac_Toe_Web_API.Enums;

namespace Tic_Tac_Toe_Web_API.Models.Dtos
{
    public class AllGamesResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int MaxPlayers { get; set; }
        public int MinPlayers { get; set; }
        public GameStatus Status { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
    }
}
