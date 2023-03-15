using Tic_Tac_Toe_Web_API.Enums;

namespace Tic_Tac_Toe_Web_API.Models
{
    public class Player
    {
        private static int UniqueId;
        private static Player computer = new Player(0, "Computer");

        public int Id { get; } 
        public string Name { get; set; }
        public static Player Computer { get; } = computer;


        public Player()
        {
            Id = Interlocked.Increment(ref UniqueId);
        }

        private Player(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
