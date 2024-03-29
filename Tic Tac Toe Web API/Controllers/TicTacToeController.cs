﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
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
        public async Task<IActionResult> GetGameById([FromRoute] int gameId)
        {
            try
            {
                var game = await _gameManager.GetGameByIdAsync(gameId) as TicTacToeGame;

                if (game == null)
                {
                    throw new Exception("Game is not from type TicTacToe");
                }
                var responseDto = _gameMapper.ConvertToResponseDto(game);

                return StatusCode(200, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("MakeMove")]
        [HttpPost]
        public async Task<IActionResult> MakeMove([FromHeader] int playerId, [FromHeader] int gameId, [FromHeader] int rowPosition, [FromHeader] int colPosition)
        {
            try
            {
                var game = await _gameManager.TicTacToeMakeMoveAsync(gameId, playerId, rowPosition, colPosition);
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
        public async Task<IActionResult> SelectMark([FromRoute] int gameId, [FromHeader] int playerId, [FromBody] string mark)
        {
            try
            {
                var game = await _gameManager.SelectMarkAsync(gameId, playerId, mark) as TicTacToeGame;
                if (game == null)
                {
                    throw new Exception("Game doesn't exist!");
                }
                var responseDto = _gameMapper.ConvertToResponseDto(game);

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
                var game = await _gameManager.RestartGameAsync(gameId, playerId) as TicTacToeGame;
                if (game == null)
                {
                    throw new Exception("Game doesn't exist!");
                }
                var responseDto = _gameMapper.ConvertToResponseDto(game);
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
