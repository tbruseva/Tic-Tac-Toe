using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;

namespace Tic_Tac_Toe_Web_API.Managers.Interfaces
{
    public interface IPlayerManager
    {
        public Player GetPlayer(string username);
        public Player GetPlayer(int username);
        public bool CheckPlayerExist(string username);
        public Player CreatePlayer(string username);
    }
}