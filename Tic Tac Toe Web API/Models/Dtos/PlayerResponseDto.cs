using System.Xml.Linq;

namespace Tic_Tac_Toe_Web_API.Models.Dtos
{
    public class PlayerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            var item = obj as PlayerResponseDto;

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
