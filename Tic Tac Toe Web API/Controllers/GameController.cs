using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        private IGameManager _gameManager;
        private IPlayerManager _playerManager;

        public GameController(IGameManager gameManager, IPlayerManager playerManager)
        {
            _gameManager = gameManager;
            _playerManager = playerManager;
        }

        [Route("allGames")]
        [HttpGet]
        public IActionResult AllGames()
        {
            try
            {
                var listAllGames = _gameManager.GetAllGames();

                return StatusCode(200, listAllGames);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{gameId}")]
        [HttpGet]
        public IActionResult GetGameById([FromRoute] int gameId)
        {
            try
            {
                var game = _gameManager.GetGameById(gameId);

                return StatusCode(200, game);
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
                return StatusCode(200, player);
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

                return StatusCode(200, game);
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

                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}