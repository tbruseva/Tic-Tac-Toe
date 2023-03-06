using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Models.Mappers
{
    public class AllGamesMapper
    {
        public AllGamesResponseDto ConvertToResponseDto(IGame game)
        {
            var response = new AllGamesResponseDto();
            response.GameId = game.Id;
            response.GameName = game.Name;
            response.GameStatus = game.GameStatus;
            response.MinPlayers = game.MinPlayers;
            response.MaxPlayers = game.MaxPlayers;
            response.Players = game.Players;

            return response;
        }
    }
}
