using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tic_Tac_Toe_Web_API.Database_Models
{
    public class PlayerDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ResultDbModel Result { get; set; } = null!;
    }
}
