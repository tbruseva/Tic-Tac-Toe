using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Models.Mappers
{
    public class AllGamesMapper
    {
        public AllGamesResponseDto ConvertToResponseDto(IGame game)
        {
            var response = new AllGamesResponseDto();
            response.Id = game.Id;
            response.Name = game.Name;
            response.Status = game.GameStatus;
            response.MinPlayers = game.MinPlayers;
            response.MaxPlayers = game.MaxPlayers;
            response.Players = game.Players;

            return response;
        }
    }
}
