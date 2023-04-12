using Tic_Tac_Toe_Web_API.Enums;

namespace Tic_Tac_Toe_Web_API.Models
{
    public class Player
    {
        private static Player computer = new Player(0, "Computer");

        public int Id { get; set; } 
        public string Name { get; set; }
        public static Player Computer { get; } = computer;

        public Player()
        {

        }
        public Player(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
