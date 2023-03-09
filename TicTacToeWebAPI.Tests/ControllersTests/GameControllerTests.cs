using AutoFixture;
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

        private static readonly Fixture _fixture = new Fixture ();

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
            var responseDto = _fixture.CreateMany<AllGamesResponseDto>(3).ToList();

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
        public void AllGames_Should_Returns_Response_Dto()
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
            var responseDto = _fixture.Create<AllGamesResponseDto>();
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
        public void CreatePlayer_Should_Catch_Exception_If_PlayerManager_Throws_Exception()
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
        public void JoinGame_Should_Return_ResponseDto()
        {
            //Arrange
            Player player = _fixture.Create<Player>();
            var responseDto = _fixture.Create<AllGamesResponseDto>();
            _playerManager.Setup(g => g.GetPlayer(player.Id)).Returns(player);
            _gameManager.Setup(g => g.JoinGame(responseDto.GameId, player)).Returns(responseDto);

            //Act
            var result = _controller.JoinGame(responseDto.GameId, player.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public void JoinGame_Should_Catch_Exception_When_PlayerManager_Throws_Exception()
        {
            //Arrange
            int gameId = 1;
            int playerId = 3;
            
            _playerManager.Setup(g => g.GetPlayer(playerId)).Throws<Exception>();

            //Act
            var result = _controller.JoinGame(gameId, playerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);
            
            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void JoinGame_Should_Catch_Exception_When_GameManager_Throws_Exception()
        {
            //Arrange
            int gameId = 1;
            Player player = _fixture.Create<Player>();

            _playerManager.Setup(g => g.GetPlayer(player.Id)).Returns(player);
            _gameManager.Setup(g=>g.JoinGame(gameId, player)).Throws<Exception>();

            //Act
            var result = _controller.JoinGame(gameId, player.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
