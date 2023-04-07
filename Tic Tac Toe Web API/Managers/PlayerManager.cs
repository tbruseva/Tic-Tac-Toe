using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Mappers;
using Tic_Tac_Toe_Web_API.Respository;

namespace Tic_Tac_Toe_Web_API.Managers
{
    public class PlayerManager : IPlayerManager
    {
        private List<Player> players = new List<Player>();
        private PlayersRepository _playersRepository;

        public PlayerManager(PlayersRepository playersRepository)
        {
            _playersRepository = playersRepository;
            players.Add(Player.Computer);
        }

        public async Task<Player> GetPlayerAsync(string username)
        {
            foreach (var player in players)
            {
                if (player.Name == username)
                {
                    return player;
                }
            }

            throw new Exception("Player doesn't exist!");
        }

        public async Task<Player> GetPlayerAsync(int playerId)
        {
            foreach (var player in players)
            {
                if (player.Id == playerId)
                {
                    return player;
                }
            }

            throw new Exception("Player doesn't exist!");
        }

        public async Task<bool> CheckPlayerExistAsync(string username)
        {
            foreach (var player in players)
            {
                if (player.Name == username)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<Player> CreatePlayerAsync(string? username)
        {
            username = string.IsNullOrEmpty(username) == false ? username : await GenerateUniqueUsernameAsync("Guest");

            if (await CheckPlayerExistAsync(username))
            {
                throw new InvalidOperationException("User with this username already exist! Please choose another name!");
            }

            var player = new Player();
            player.Name = username;
            await _playersRepository.CreatePlearAsync(player);

            return player;
        }

        #region Private methods
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
