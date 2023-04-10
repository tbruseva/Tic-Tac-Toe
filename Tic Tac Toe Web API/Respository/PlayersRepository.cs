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

        public async Task<PlayerDbModel> Create(Player player)
        {
            var playerDb = new PlayerDbModel() { Id = player.Id, Name = player.Name };
            var createdDbPlayer = await _dbContext.AddAsync(playerDb);
            await _dbContext.SaveChangesAsync();

            return createdDbPlayer.Entity;
        }
    }
}
