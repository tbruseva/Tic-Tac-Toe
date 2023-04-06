using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tic_Tac_Toe_Web_API.Database_Models
{
    public class PlayerDbModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Game { get; set; }
        public int Wins { get; set; }
    }
}
