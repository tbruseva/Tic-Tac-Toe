using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;
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
        public async Task<IActionResult> AllGames()
        {
            try
            {
                var allGames = await _gameManager.GetAllGamesAsync();
                var responseDto = allGames.Select(g => _allGamesMapper.ConvertToResponseDto(g));
                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CreateGame")]
        [HttpPost]
        public async Task<IActionResult> CreateGame()
        {
            try
            {
                var game = await _gameManager.CreateGameAsync();
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
        public async Task<IActionResult> CreatePlayer([FromHeader] string? username)
        {
            try
            {
                var player = await _playerManager.CreatePlayerAsync(username);
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
        public async Task <IActionResult> JoinGame([FromRoute] int gameId, [FromHeader] int playerId)
        {
            try
            {
                var player = await _playerManager.GetPlayerAsync(playerId);
                var game = await _gameManager.JoinGameAsync(gameId, player);
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