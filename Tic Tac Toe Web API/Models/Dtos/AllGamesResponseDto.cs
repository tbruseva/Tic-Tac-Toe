using System.Xml.Linq;
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

        public override bool Equals(object obj)
        {
            var item = obj as AllGamesResponseDto;

            if (item == null)
            {
                return false;
            }

            return this.Id == item.Id && this.Name == item.Name;
        }

        public override int GetHashCode()
        {
            int hash = Id.GetHashCode();
            hash = hash + (Name != null ? Name.GetHashCode() : 0);

            return hash;
        }
    }
}
