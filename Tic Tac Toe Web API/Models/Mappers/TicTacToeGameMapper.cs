using Tic_Tac_Toe_Web_API.Models.Dtos;

namespace Tic_Tac_Toe_Web_API.Models.Mappers
{
    public class TicTacToeGameMapper
    {

    public TicTacToeGameResponseDto ConvertToResponseDto(TicTacToeGame game) 
        {
            Player? playerX = game.Players.ElementAtOrDefault(game.PlayerXIndex) != null ? game.Players[game.PlayerXIndex] : null;
            Player? playerO = game.Players.ElementAtOrDefault(game.PlayerOIndex) != null ? game.Players[game.PlayerOIndex] : null;
            int? currentPlayerId = game.Players.ElementAtOrDefault(game.CurrentPlayerIndex) != null ? game.Players[game.CurrentPlayerIndex].Id : null;

            var responseDto = new TicTacToeGameResponseDto();
            responseDto.Id = game.Id;
            responseDto.CurrentPlayerId = currentPlayerId;
            responseDto.PlayerX = playerX;
            responseDto.PlayerO = playerO;
            responseDto.Grid = game.Grid;
            responseDto.WinCells = game.WinCells;
            responseDto.GameState = game.GameState;

            return responseDto;
        }
    }
}
