using Tic_Tac_Toe_Web_API.Database_Models;
using Tic_Tac_Toe_Web_API.Models;

namespace Tic_Tac_Toe_Web_API.Respository.Interfaces
{
    public interface IPlayersRepository
    {
        public Task<PlayerDbModel> Get(int id);
        public Task<PlayerDbModel> Get(string username);
        public Task<PlayerDbModel> Create(PlayerDbModel player);
    }
}
