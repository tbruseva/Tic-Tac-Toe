using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Enums;
using System.Numerics;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Mappers;

namespace Tic_Tac_Toe_Web_API
{
    public class GameManager : IGameManager
    {
        private List<IGame> _allGames = new List<IGame>();
        private readonly AllGamesMapper _gameMapper;
        private readonly TicTacToeGameMapper _TicTacToeMapper;

        public GameManager()
        {
            _gameMapper = new AllGamesMapper();
            _TicTacToeMapper = new TicTacToeGameMapper();
        }

        public List<AllGamesResponseDto> GetAllGames()
        {
            return _allGames.Select(g => _gameMapper.ConvertToResponseDto(g)).ToList();
        }

        public TicTacToeGameResponseDto GetGameById(int id)
        {
            var game = _allGames.Where(g => g.Id == id).Select(g => g as TicTacToeGame).FirstOrDefault();
            if (game == null)
            {
                throw new Exception("Game doesn't exist!");
            }
            var responseDto = _TicTacToeMapper.ConvertToResponseDto(game);

            return responseDto;
        }


        public AllGamesResponseDto CreateGame()
        {
            var game = new TicTacToeGame();
            _allGames.Add(game);
            var gameResponseDto = _gameMapper.ConvertToResponseDto(game);
            return gameResponseDto;
        }

        public AllGamesResponseDto JoinGame(int gameId, Player player)
        {
            var game = GetGame(gameId);
            game.JoinGame(player);
            var gameResponseDto = _gameMapper.ConvertToResponseDto(game);

            return gameResponseDto;
        }


        public TicTacToeGameResponseDto TicTacToeSelectMark(int gameId, int playerId, string playerMark)
        {
            var game = GetGame(gameId) as TicTacToeGame;

            Mark selectedMark;
            Enum.TryParse(playerMark, true, out selectedMark);

            game.SelectMark(playerId, selectedMark);

            var gameResponseDto = _TicTacToeMapper.ConvertToResponseDto(game);

            return gameResponseDto;
        }

        public TicTacToeGameResponseDto TicTacToeRestartGame(int gameId, int playerId)
        {
            var game = GetGame(gameId) as TicTacToeGame;
            foreach (var player in game.Players)
            {
                if (player.Id == playerId)
                {
                    game.RestartGame();
                    break;
                }
                else
                {
                    throw new UnauthorizedAccessException("Only game players can restart the game!");
                }
            }

            var gameResponseDto = _TicTacToeMapper.ConvertToResponseDto(game);

            return gameResponseDto;
        }

        public TicTacToeGameResponseDto TicTacToeMakeMove(int gameId, int playerId, int rowPosition, int colPosition)
        {
            var game = GetGame(gameId) as TicTacToeGame;
            game.MakeMove(playerId, rowPosition, colPosition);
        
        var gameResponseDto = _TicTacToeMapper.ConvertToResponseDto(game);

            return gameResponseDto;
        }


    #region Private methods
    private IGame GetGame(int id)
    {
        var game = _allGames.Where(g => g.Id == id).FirstOrDefault();
        if (game == null)
        {
            throw new Exception("Game doesn't exist!");
        }
        return game;
    }
    #endregion
}
}
