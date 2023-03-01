using Tic_Tac_Toe_Web_API.Models;

namespace Tic_Tac_Toe_Web_API
{
    public interface IPlayerManager
    {
        public Player GetPlayer(string username);
        public Player GetPlayer(int username);
        public bool CheckPlayerExist(string username);
        public Player CreatePlayer(string username);
    }
}