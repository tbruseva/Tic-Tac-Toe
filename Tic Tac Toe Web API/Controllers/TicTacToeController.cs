using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicTacToeController : ControllerBase
    {
        private IGameManager _gameManager;
        private IPlayerManager _playerManager;

        public TicTacToeController(IGameManager gameManager, IPlayerManager playerManager)
        {
            _gameManager = gameManager;
            _playerManager = playerManager;
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

        [Route("JoinGame/{gameId}")]
        [HttpPost]
        public IActionResult JoinGame([FromRoute] int gameId, [FromHeader] string username, [FromBody] string? mark)
        {
            try
            {
                var player = _playerManager.CreatePlayer(username);
                var game = _gameManager.JoinGame(gameId, player, mark);

                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("MakeMove")]
        [HttpPost]
        public IActionResult MakeMove([FromHeader] string username, int gameId, int rowPosition, int colPosition)
        {
            try
            {
                var game = _gameManager.MakeMove(gameId, username, rowPosition, colPosition);
                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("SelectFirstOrSecondPlayer/{gameId}")]
        [HttpPost]
        public IActionResult SelectFirstOrSecondPlayer([FromRoute] int gameId, [FromHeader] string username, [FromBody] string mark)
        {
            try
            {
                var player = _gameManager.SelectFirstOrSecondPlayer(gameId, username, mark);
                return StatusCode(200, player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
