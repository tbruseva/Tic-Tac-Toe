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
                var game = _gameManager.CreateGame().ToJson();

                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
