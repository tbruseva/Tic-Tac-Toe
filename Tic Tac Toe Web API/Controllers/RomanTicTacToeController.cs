using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Mappers;

namespace Tic_Tac_Toe_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RomanTicTacToeController : Controller
    {
        private IGameManager _gameManager;
        private RomanTicTacToeGameMapper _mapper;

        public RomanTicTacToeController(IGameManager gameManager, RomanTicTacToeGameMapper mapper)
        {
            _gameManager = gameManager;
            _mapper = mapper;
        }

        [Route("{gameId}")]
        [HttpGet]
        public async Task<IActionResult> GetGameById([FromRoute] int gameId)
        {
            try
            {
                var game = await _gameManager.GetGameByIdAsync(gameId);
                var responseDto = _mapper.ConvertToResponseDto((RomanTicTacToeGame)game);

                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("MakeMove")]
        [HttpPost]
        public async Task<IActionResult> MakeMove([FromHeader] int playerId, [FromHeader] int gameId, [FromHeader] int position)
        {
            try
            {
                var game = await _gameManager.RomanTicTacToeMakeMoveAsync(gameId, playerId, position);
                var responseDto = _mapper.ConvertToResponseDto(game);

                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Route("SelectMark/{gameId}")]
        [HttpPost]
        public async Task<IActionResult> SelectMark([FromRoute] int gameId, [FromHeader] int playerId, [FromBody] string mark)
        {
            try
            {
                var game = await _gameManager.RomanTicTacToeSelectMarkAsync(gameId, playerId, mark);
                var responseDto = _mapper.ConvertToResponseDto(game);

                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("RestartGame/{gameId}")]
        [HttpPost]
        public async Task<IActionResult> RestartGame([FromRoute] int gameId, [FromHeader] int playerId)
        {
            try
            {
                var game = await _gameManager.RomanTicTacToeRestartGameAsync(gameId, playerId);
                var responseDto = _mapper.ConvertToResponseDto(game);
                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GameState/{gameId}")]
        [HttpGet]
        public async Task<IActionResult> GetStateLatestVersion(int gameId)
        {
            try
            {
                int state = await _gameManager.RomanTicTacToeGetGameStateAsync(gameId);

                return StatusCode(200, state);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
