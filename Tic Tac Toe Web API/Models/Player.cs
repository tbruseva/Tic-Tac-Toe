using Tic_Tac_Toe_Web_API.Enums;

namespace Tic_Tac_Toe_Web_API.Models
{
    public class Player
    {
        private static int UniqueId;

        public int Id { get;} 
        public string Name { get; set; }
        public Mark Mark { get; set; } = Mark.None;
        public int CounterWins { get; set; }

        public Player()
        {
            Id = ++UniqueId;
        }

    }
}
