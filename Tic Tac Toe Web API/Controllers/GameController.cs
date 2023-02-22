using Microsoft.AspNetCore.Mvc;
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
                var listAllGames = _gameManager.GetAllGames();//.Select(g=>g.ToJson());

                return StatusCode(200, listAllGames);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{gameId}")]
        [HttpGet]
        public IActionResult GetGameById([FromRoute] int gameId, [FromHeader]string username)
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

        [Route("JoinGame")]
        [HttpPost]
        public IActionResult JoinGame([FromBody] int gameId, [FromHeader] string username)
        {
            try
            {
                var player = _playerManager.CreatePlayer(username);
                var game = _gameManager.JoinGame(gameId, player).ToJson();
                
                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("SelectFirstOrSecondPlayer")]
        [HttpPost]
        public IActionResult SelectFirstOrSecondPlayer([FromHeader] string username, [FromBody] int gameId, string mark)
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


        [Route("TicTacToe/MakeMove")]
        [HttpPost]
        public IActionResult MakeMove([FromHeader] string username, int gameId, int rowPosition, int colPosition)
        {
            try
            {
                //var player = _playerManager.GetPlayer(username);
                var move = _gameManager.MakeMove(gameId, username, rowPosition, colPosition);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}