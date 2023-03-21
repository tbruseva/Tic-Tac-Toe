namespace Tic_Tac_Toe_Web_API.Models
{
    public class TicTacToe
    {
        private static int uniqueId;
        public int Id { get; }
        public string Name { get; set; }

        public TicTacToe()
        {
            Id = Interlocked.Increment(ref uniqueId);
        }
    }
}
