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
        private readonly GameMapper _gameMapper;

        public GameManager()
        {
            _gameMapper= new GameMapper();
        }

        public List<GameResponseDto> GetAllGames()
        {
            return _allGames.Select(g => _gameMapper.ConvertToResponseDto(g)).ToList();
        }

        public GameResponseDto GetGameById(int id)
        {
            var game = _allGames.Where(g => g.Id == id).Select(g=> _gameMapper.ConvertToResponseDto(g)).FirstOrDefault();
            if (game == null)
            {
                throw new Exception("Game doesn't exist!");
            }
            return game;
        }


        public GameResponseDto CreateGame()
        {
            var game = new TicTacToeGame();
            _allGames.Add(game);
            var gameResponseDto = _gameMapper.ConvertToResponseDto(game);
            return gameResponseDto;
        }

        public GameResponseDto JoinGame(int gameId, Player player)
        {
            var game = GetGame(gameId);
            game.JoinGame(player);
            var gameResponseDto = _gameMapper.ConvertToResponseDto(game);

            return gameResponseDto;
        }


        public GameResponseDto SelectMark(int gameId, int playerId, string playerMark)
        {
            var game = GetGame(gameId);

            Mark selectedMark;
            Enum.TryParse(playerMark, true, out selectedMark);

            (game as TicTacToeGame).SelectMark(playerId, selectedMark);

            var gameResponseDto = _gameMapper.ConvertToResponseDto(game);

            return gameResponseDto;
        }

        public GameResponseDto RestartGame(int gameId, string username)
        {
            var game = GetGame(gameId);
            foreach (var player in game.Players)
            {
                if (player.Name == username)
                {
                    (game as TicTacToeGame).RestartGame();
                }
                else
                {
                    throw new UnauthorizedAccessException("Only game players can restart the game!");
                }
            }

            var gameResponseDto = _gameMapper.ConvertToResponseDto(game);

            return gameResponseDto;
        }

        public GameResponseDto MakeMove(int gameId, int playerId, int rowPosition, int colPosition)
        {
            var game = GetGame(gameId);
            if (game.Name == "Tic-Tac-Toe")
            {
                //game = (game as TicTacToeGame);
                //if (game.MakeMove(player, rowPosition, colPosition)

                (game as TicTacToeGame).MakeMove(playerId, rowPosition, colPosition);
            }

            var gameResponseDto = _gameMapper.ConvertToResponseDto(game);

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
