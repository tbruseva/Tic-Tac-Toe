using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models.Mappers;

namespace Tic_Tac_Toe_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        private IGameManager _gameManager;
        private IPlayerManager _playerManager;
        private AllGamesMapper _allGamesMapper;
        private PlayerMapper _playerMapper;

        public GameController(IGameManager gameManager, IPlayerManager playerManager, AllGamesMapper gamesMapper, PlayerMapper playerMapper)
        {
            _gameManager = gameManager;
            _playerManager = playerManager;
            _allGamesMapper= gamesMapper;
            _playerMapper = playerMapper;
        }

        [Route("allGames")]
        [HttpGet]
        public IActionResult AllGames()
        {
            try
            {
                var responseDto = _gameManager.GetAllGames().Select(g => _allGamesMapper.ConvertToResponseDto(g));

                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CreateGame")]
        [HttpPost]
        public IActionResult CreateGame()
        {
            try
            {
                var game = _gameManager.CreateGame();
                var responseDto = _allGamesMapper.ConvertToResponseDto(game);

                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Player")]
        [HttpPost]
        public IActionResult CreatePlayer([FromHeader] string username)
        {
            try
            {
                var player = _playerManager.CreatePlayer(username);
                var responseDto = _playerMapper.ConvertToResponseDto(player);

                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("JoinGame/{gameId}")]
        [HttpPost]
        public IActionResult JoinGame([FromRoute] int gameId, [FromHeader] int playerId)
        {
            try
            {
                var player = _playerManager.GetPlayer(playerId);
                var game = _gameManager.JoinGame(gameId, player);
                var responseDto = _allGamesMapper.ConvertToResponseDto(game);

                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}