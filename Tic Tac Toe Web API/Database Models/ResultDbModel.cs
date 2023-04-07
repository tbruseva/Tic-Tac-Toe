using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Tic_Tac_Toe_Web_API.Database_Models
{
    public class ResultDbModel
    {
        [Key]
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public PlayerDbModel Player { get; set; } = null!;
        public string GameName { get; set; } = null!;  
        public int Wins { get; set; }
    }
}
