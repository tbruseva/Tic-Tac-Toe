using Tic_Tac_Toe_Web_API.Models.Dtos;

namespace Tic_Tac_Toe_Web_API.Models.Mappers
{
    public class RotaGameMapper
    {
        public RotaResponseDto ConvertToResponseDto(RotaGame game)
        {
            Player? playerX = game.Players.ElementAtOrDefault(game.PlayerXIndex) != null ? game.Players[game.PlayerXIndex] : null;
            Player? playerO = game.Players.ElementAtOrDefault(game.PlayerOIndex) != null ? game.Players[game.PlayerOIndex] : null;
            int? currentPlayerId = game.Players.ElementAtOrDefault(game.CurrentPlayerIndex) != null ? game.Players[game.CurrentPlayerIndex].Id : null;

            var responseDto = new RotaResponseDto();
            responseDto.Id = game.Id;
            responseDto.CurrentPlayerId = currentPlayerId;
            responseDto.PlayerX = playerX;
            responseDto.PlayerO = playerO;
            responseDto.Grid = game.Grid;
            responseDto.WinCells = game.WinCells;
            responseDto.Status = game.GameStatus;
            responseDto.GameState = game.GameState;
            responseDto.CounterWins = game.CounterWins;
            responseDto.CounterTotalGamesPlayed = game.CounterTotalGames;

            return responseDto;
        }
    }
}
