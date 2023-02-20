using Microsoft.AspNetCore.Mvc;
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

        [Route("JoinGame")]
        [HttpPost]
        public IActionResult JoinGame([FromBody] int gameId, [FromHeader] string username)
        {
            try
            {
                var player = _playerManager.CreatePlayer(username);
                var game = _gameManager.JoinGame(gameId, player);
                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("SelectPlayer")]
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

        //[Route("JoinGame")]
        //[HttpPost]
        //public IActionResult StartGame([FromBody] int gameId, [FromHeader] string username)
        //{
        //    try
        //    {
        //        var player = _playerManager.CreatePlayer(username);
        //        var startedGame = _gameManager.StartGame(gameId, player);
        //        return StatusCode(200, startedGame);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[Route("TicTacToe/MakeMove")]
        //[HttpPost]
        //public IActionResult MakeMove([FromHeader] int userId, int gameId, [FromBody] int rowPosition, int colPosition, )
        //{
        //    try
        //    {
        //        var player = _playerManager.GetPlayer(username);
        //        //var move = _gameManager.MakeMove(id, player);
        //        return StatusCode(200);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

    }
}