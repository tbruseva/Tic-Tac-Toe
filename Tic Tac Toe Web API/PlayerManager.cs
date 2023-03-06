using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Mappers;

namespace Tic_Tac_Toe_Web_API
{
    public class PlayerManager : IPlayerManager
    {
        private List<Player> players = new List<Player>();

        private PlayerMapper _playerMapper;

        public PlayerManager(PlayerMapper playerMapper)
        {
            _playerMapper = playerMapper;
        }

        public Player GetPlayer(string username)
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

        public Player GetPlayer(int playerId)
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

        public bool CheckPlayerExist(string username)
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

        public PlayerResponseDto CreatePlayer(string username)
        {
            if (CheckPlayerExist(username))
            {
                throw new InvalidOperationException("User with this username already exist! Please choose another name!");
            }

            var player = new Player();
            player.Name = username; 
            players.Add(player);
            var responseDto = _playerMapper.ConvertToResponseDto(player);

            return responseDto;
        }

    }
}
