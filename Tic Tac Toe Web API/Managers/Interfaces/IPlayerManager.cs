using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;

namespace Tic_Tac_Toe_Web_API.Managers.Interfaces
{
    public interface IPlayerManager
    {
        public Task<Player> GetPlayerAsync(string username);
        public Task<Player> GetPlayerAsync(int username);
        public Task<bool> CheckPlayerExistAsync(string username);
        public Task<Player> CreatePlayerAsync(string username);
    }
}