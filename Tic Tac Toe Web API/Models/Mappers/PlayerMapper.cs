using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using Tic_Tac_Toe_Web_API.Database_Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;

namespace Tic_Tac_Toe_Web_API.Models.Mappers
{
    public class PlayerMapper
    {
        public PlayerResponseDto ConvertToResponseDto(Player player)
        {
            var responseDto = new PlayerResponseDto();
            responseDto.Id = player.Id;
            responseDto.Name = player.Name;

            return responseDto;
        }

        public PlayerDbModel ConvertToPlayerDbModel(Player player)
        {
            var playerDbModel = new PlayerDbModel();
            playerDbModel.Id = player.Id;
            playerDbModel.Name = player.Name;

            return playerDbModel;
        }

        public Player ConvertToPlayer(PlayerDbModel playerDbModel)
        {
            var player = new Player(playerDbModel.Id, playerDbModel.Name);

            return player;
        }

    }
}
