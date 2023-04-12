using Tic_Tac_Toe_Web_API.Database_Models;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Mappers;
using Tic_Tac_Toe_Web_API.Respository;
using Tic_Tac_Toe_Web_API.Respository.Interfaces;

namespace Tic_Tac_Toe_Web_API.Managers
{
    public class PlayerManager : IPlayerManager
    {
        private List<Player> players = new List<Player>();
        private IPlayersRepository _playersRepository;
        private PlayerMapper _mapper;

        public PlayerManager(IPlayersRepository playersRepository, PlayerMapper mapper)
        {
            _playersRepository = playersRepository;
            _mapper = mapper;
            players.Add(Player.Computer);
        }

        public async Task<Player> GetPlayerAsync(int playerId)
        {
            var player = await _playersRepository.Get(playerId);
            if (player == null)
            {
                throw new Exception("Player doesn't exist!");
            }

            var playerModel = _mapper.ConvertToPlayer(player);

            return playerModel;
        }

        public async Task<Player> GetPlayerAsync(string username)
        {
            var playerDbModel = await _playersRepository.Get(username);
            if (playerDbModel == null)
            {
                throw new Exception("Player doesn't exist!");
            }

            var player = _mapper.ConvertToPlayer(playerDbModel);

            return player;
        }

        public async Task<Player> CreatePlayerAsync(string? username)
        {
            username = string.IsNullOrEmpty(username) == false ? username : await GenerateUniqueUsernameAsync("Guest");

            if (await CheckPlayerExistAsync(username))
            {
                throw new InvalidOperationException("User with this username already exist! Please choose another name!");
            }

            var playerDbModel = new PlayerDbModel() { Name = username };

            var createdPlayer = await _playersRepository.Create(playerDbModel);
            if (createdPlayer == null)
            {
                throw new Exception("Something went wrong when saving player in the database!");
            }

            var player = _mapper.ConvertToPlayer(createdPlayer);

            return player;
        }

        #region Private methods
        private async Task<bool> CheckPlayerExistAsync(string username)
        {
            var player = await _playersRepository.Get(username);

            return player == null ? false : true;
        }
        private async Task<string> GenerateUniqueUsernameAsync(string baseUsername)
        {
            int playerIndex = 1;
            string userName;

            do
            {
                userName = baseUsername + (playerIndex++);
            }
            while (await CheckPlayerExistAsync(userName));

            return userName;
        }
        #endregion

    }
}
