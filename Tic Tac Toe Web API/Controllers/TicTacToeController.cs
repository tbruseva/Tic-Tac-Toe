using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models.Mappers;

namespace Tic_Tac_Toe_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicTacToeController : ControllerBase
    {
        private IGameManager _gameManager;
        private TicTacToeGameMapper _gameMapper;

        public TicTacToeController(IGameManager gameManager, TicTacToeGameMapper gameMapper)
        {
            _gameManager = gameManager;
            _gameMapper = gameMapper;
        }

        [Route("{gameId}")]
        [HttpGet]
        public IActionResult GetGameById([FromRoute] int gameId)
        {
            try
            {
                var game = _gameManager.GetGameById(gameId);
                var responseDto = _gameMapper.ConvertToResponseDto(game);

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
                var game = _gameManager.TicTacToeMakeMove(gameId, playerId, rowPosition, colPosition);
                var responseDto = _gameMapper.ConvertToResponseDto(game);

                return StatusCode(200, responseDto);
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
                var responseDto = _gameMapper.ConvertToResponseDto(game);

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
                var responseDto = _gameMapper.ConvertToResponseDto(game);
                return StatusCode(200, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
