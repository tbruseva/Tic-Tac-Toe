using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models.Mappers;

namespace Tic_Tac_Toe_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RotaController : Controller
    {
        private IGameManager _gameManager;
        private RomanTicTacToeGameMapper _mapper;

        public RotaController(IGameManager gameManager, RomanTicTacToeGameMapper mapper)
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
                var game = await _gameManager.GetGameByIdAsync(gameId) as RotaGame;
                if (game == null)
                {
                    throw new Exception("Game is not from type Rota");
                }

                var responseDto = _mapper.ConvertToResponseDto(game);

                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("AddPawn")]
        [HttpPost]
        public async Task<IActionResult> AddPawn([FromHeader] int gameId,[FromHeader] int playerId, [FromHeader] int position)
        {
            try
            {
                var game = await _gameManager.AddPawnAsync(gameId, playerId, position);
                var responseDto = _mapper.ConvertToResponseDto(game);

                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("MakeMove")]
        [HttpPost]
        public async Task<IActionResult> MakeMove([FromHeader] int playerId, [FromHeader] int gameId, [FromHeader] int oldPosition, int newPosition)
        {
            try
            {
                var game = await _gameManager.RotaMakeMoveAsync(gameId, playerId, oldPosition, newPosition);
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
                var game = await _gameManager.SelectMarkAsync(gameId, playerId, mark) as RotaGame;
                if (game == null)
                {
                    throw new Exception("Game doesn't exist!");
                }
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
                var game = await _gameManager.RestartGameAsync(gameId, playerId) as RotaGame;
                if (game == null)
                {
                    throw new Exception("Game doesn't exist!");
                }
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
                int state = await _gameManager.GetGameStateAsync(gameId);

                return StatusCode(200, state);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
