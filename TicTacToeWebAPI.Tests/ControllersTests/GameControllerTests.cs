using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tic_Tac_Toe_Web_API;
using Tic_Tac_Toe_Web_API.Controllers;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace TicTacToeWebAPI.Tests.ControllersTests
{
    [TestFixture]
    public class GameControllerTests
    {
        private GameController _controller;
        private Mock<IGameManager> _gameManager;
        private Mock<IPlayerManager> _playerManager;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _gameManager= new Mock<IGameManager>();
            _playerManager= new Mock<IPlayerManager>();
            _controller = new GameController(_gameManager.Object, _playerManager.Object);
        }

        [Test]
        public void AllGames_Should_Return_List_Of_All_Games_If_Any_Are_Created()
        {
            //Arrange
            var responseDto = new List<AllGamesResponseDto> { new AllGamesResponseDto { GameId = 1, GameName =
                "Tic-Tac-Toe", GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Players = new List<Player>(), MaxPlayers = 2, MinPlayers = 2}};

            _gameManager.Setup(g => g.GetAllGames()).Returns(responseDto);

            //Act
            var result = _controller.AllGames();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.AreEqual(responseDto, okResult.Value);
        }

        [Test]
        public void AllGames_Should_Return_Empty_List_If_No_Games_Are_Created()
        {
            //Arrange
            var responseDto = new List<AllGamesResponseDto>();
            _gameManager.Setup(g => g.GetAllGames()).Returns(responseDto);

            //Act
            var result = _controller.AllGames();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public void CreateGame_Should_Return_Created_Game()
        {
            //Arrange
            var responseDto = new AllGamesResponseDto { GameId = 1, GameName =
                "Tic-Tac-Toe", GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Players = new List<Player>(), MaxPlayers = 2, MinPlayers = 2};
            _gameManager.Setup(g => g.CreateGame()).Returns(responseDto);

            //Act
            var result = _controller.CreateGame();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

            [Test]
        public void CreatePlayer_Should_Return_Created_Player_Details() 
        {
            //Arrange
            string username = "to";
            var player = new Player { Name= username };
            var responseDto = new PlayerResponseDto { Id = player.Id, Name = player.Name };
            _playerManager.Setup(g => g.CreatePlayer(username)).Returns(responseDto);

            // Act
            var result = _controller.CreatePlayer(username);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public void CreatePlayer_Should_Catch_Exception_If_Player_Exist()
        {
            //Arrange
            string username = "to";
            _playerManager.Setup(g => g.CreatePlayer(username)).Throws<InvalidOperationException>();

            //Act
            var result = _controller.CreatePlayer(username);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void JoinGame_Should_Updated_Game_Details()
        {
            //Arrange
            int gameId = 1;
            int playerId = 1;
            Player player = new Player { Name = "to" };
            var responseDto = new AllGamesResponseDto
            {
                GameId = 1,
                GameName =
                "Tic-Tac-Toe",
                GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted,
                Players = new List<Player>(),
                MaxPlayers = 2,
                MinPlayers = 2
            };
            _playerManager.Setup(g => g.GetPlayer(playerId)).Returns(player);
            _gameManager.Setup(g => g.JoinGame(gameId, player)).Returns(responseDto);

            //Act
            var result = _controller.JoinGame(gameId, playerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }
    }
}
