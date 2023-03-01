using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

    
        [Route("MakeMove")]
        [HttpPost]
        public IActionResult MakeMove([FromHeader] int playerId, int gameId, int rowPosition, int colPosition)
        {
            try
            {
                var game = _gameManager.MakeMove(gameId, playerId, rowPosition, colPosition).ToJson();
                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("SelectMark/{gameId}")]
        [HttpPost]
        public IActionResult SelectMark([FromRoute] int gameId, [FromHeader] int playerId, [FromBody] string mark)
        {
            try
            {
                var game = _gameManager.SelectMark(gameId, playerId, mark).ToJson();
                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("RestartGame/{gameId}")]
        [HttpPost]
        public IActionResult RestartGame([FromRoute] int gameId, [FromHeader] string username)
        {
            try
            {
                var game = _gameManager.RestartGame(gameId, username).ToJson();
                return StatusCode(200, game);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
