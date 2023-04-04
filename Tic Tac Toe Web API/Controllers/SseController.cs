using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models.Mappers;

namespace Tic_Tac_Toe_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SseController : Controller
    {
        private IGameManager _gameManager;

        public SseController(IGameManager gameManager)
        {
            _gameManager = gameManager;

        }
        
        [Route("{gameId}")]
        [HttpGet]
        public async Task<IActionResult> GetEvents([FromRoute] int gameId)
        {
            var game = await _gameManager.GetGameByIdAsync(gameId);
            var gameState = 0;

            Response.Headers.Add("Content-Type", "text/event-stream");

            if (gameState != game.GameState)
            {
                gameState = game.GameState;
                
                var eventData = $"event: GameState\n" +
                                $"data: {gameState}\n"; ;
                var eventDataBytes = Encoding.UTF8.GetBytes(eventData);
                Response.Body.Write(eventDataBytes, 0, eventDataBytes.Length);
                Response.Body.Flush();
            }

            return new EmptyResult();
        }
    }
}
