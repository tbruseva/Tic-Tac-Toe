using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace Tic_Tac_Toe_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicTacToeController : ControllerBase
    {
        private IGameManager _gameManager;
        //private IPlayerManager _playerManager;

        public TicTacToeController(IGameManager gameManager, IPlayerManager playerManager)
        {
            _gameManager = gameManager;
            //_playerManager = playerManager;
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

        [Route("MakeMove")]
        [HttpPost]
        public IActionResult MakeMove([FromHeader] int playerId, int gameId, int rowPosition, int colPosition)
        {
            try
            {
                var gameResponseDto = _gameManager.TicTacToeMakeMove(gameId, playerId, rowPosition, colPosition);
                return StatusCode(200, gameResponseDto);
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
                var game = _gameManager.TicTacToeSelectMark(gameId, playerId, mark);
                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("RestartGame/{gameId}")]
        [HttpPost]
        public IActionResult RestartGame([FromRoute] int gameId, [FromHeader] int playerId)
        {
            try
            {
                var game = _gameManager.TicTacToeRestartGame(gameId, playerId);
                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
