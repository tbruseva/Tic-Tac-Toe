using System.Reflection.Metadata.Ecma335;
using Tic_Tac_Toe_Web_API.Models.Dtos;

namespace Tic_Tac_Toe_Web_API.Models.Mappers
{
    public class PlayerMapper
    {
        public PlayerResponseDto ConvertToResponseDto(Player player) 
        {
            var responseDto = new PlayerResponseDto();
            responseDto.Id= player.Id;
            responseDto.Name= player.Name;

            return responseDto;
        }
    }
}
