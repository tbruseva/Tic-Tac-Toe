using Tic_Tac_Toe_Web_API.Database_Models;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Respository.Interfaces;

namespace Tic_Tac_Toe_Web_API.Respository
{
    public class PlayersRepository : IPlayersRepository
    {
        private AppDbContext _dbContext;

        public PlayersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PlayerDbModel> Get(int id)
        {
            var player = _dbContext.Players.Where(p => p.Id == id).FirstOrDefault();

            return player;
        }
        public async Task<PlayerDbModel> Get(string username)
        {
            var player = _dbContext.Players.Where(p => p.Name == username).FirstOrDefault();

            return player;
        }
        public async Task<PlayerDbModel> Create(PlayerDbModel player)
        {
            var createdDbPlayer = await _dbContext.AddAsync(player);
            await _dbContext.SaveChangesAsync();

            return createdDbPlayer.Entity;
        }
    }
}
