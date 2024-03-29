﻿using Moq;
using Tic_Tac_Toe_Web_API.Controllers;
using Tic_Tac_Toe_Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Internal;
using AutoFixture;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Akka.Actor.Setup;
using Tic_Tac_Toe_Web_API.Models.Mappers;
using Tic_Tac_Toe_Web_API.Respository.Interfaces;
using Tic_Tac_Toe_Web_API.Respository;

namespace TicTacToeWebAPI.Tests.ControllersTests
{
    [TestFixture]
    public class TicTacToeGameControllerTests
    {
        private TicTacToeController _controller;
        private Mock<IGameManager> _gameManager;
        private Mock<IResultsRepository> _resultsRepo;
        private TicTacToeGameMapper _gameMapper;

        private static readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            // Arrange
            _gameManager = new Mock<IGameManager>();
            _gameMapper = new TicTacToeGameMapper();
            _resultsRepo = new Mock<IResultsRepository>();
            _controller = new TicTacToeController(_gameManager.Object, _gameMapper);
        }


        [Test]
        public async Task GetGameById_Should_Return_ResponseDto_With_Correct_Details()
        {
            //Arrange
            var game = _fixture.Create<TicTacToeGame>();
            _gameManager.Setup(g => g.GetGameByIdAsync(game.Id)).ReturnsAsync(game);
            var responseDto = _gameMapper.ConvertToResponseDto(game);
            //Act
            var result = await _controller.GetGameById(game.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value.Equals(responseDto));
        }

        [Test]
        public async Task GetGameById_Should_Return_StatusCode_200_If_Game_Exist()
        {
            //Arrange
            var game = _fixture.Create<TicTacToeGame>();
            _gameManager.Setup(g => g.GetGameByIdAsync(game.Id)).ReturnsAsync(game);
            var responseDto = _gameMapper.ConvertToResponseDto(game);
            //Act
            var result = await _controller.GetGameById(game.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetGameById_Should_Catch_Exception_If_GameManager_Throws_Exception()
        {
            //Arrange
            int gameId = 1;
            _gameManager.Setup(g => g.GetGameByIdAsync(gameId)).Throws<Exception>();

            //Act
            var result = await _controller.GetGameById(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        //[Test]
        //public async Task MakeMove_Should_Return_ResponseDto_With_Correct_Details()
        //{
        //    int playerId = 1;
        //    int gameId = 1;
        //    int rowPosition = 0;
        //    int colPosition = 0;
        //    var player1 = new Player { Name = "to" };
        //    var player2 = new Player { Name = "no" };
        //    var game = new TicTacToeGame(_resultsRepo);
        //    game.Players.Add(player1);
        //    game.Players.Add(player2);
        //    //var game = _fixture.Build<TicTacToeGame>().With(g => g.Players[0], playerX).With(g => g.Players[1], playerO).Create();
        //    var responseDto = _gameMapper.ConvertToResponseDto(game);   

        //    _gameManager.Setup(g => g.TicTacToeMakeMoveAsync(playerId, gameId, rowPosition, colPosition)).ReturnsAsync(game);

        //    //Act
        //    var result = await _controller.MakeMove(playerId, gameId, rowPosition, colPosition);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<ObjectResult>(result);

        //    var okResult = result as ObjectResult;
        //    Assert.That(okResult.Value, Is.EqualTo(responseDto));
        //}

        [Test]
        public async Task MakeMove_Should_Catch_Exception_If_GameManager_Throws_Exception()
        {
            int playerId = 1;
            int gameId = 1;
            int rowPosition = 0;
            int colPosition = 0;
            _gameManager.Setup(g => g.TicTacToeMakeMoveAsync(playerId, gameId, rowPosition, colPosition)).Throws<UnauthorizedAccessException>();

            //Act
            var result = await _controller.MakeMove(playerId, gameId, rowPosition, colPosition);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }


        //[Test]
        //public async Task TicTacToeSelectMark_Should_Return_ResponseDto()
        //{
        //    //Arrage
        //    var gameId = 1;
        //    var playerId = 1;
        //    var mark = "O";
        //    var player1 = new Player { Name = "to" };
        //    var player2 = new Player { Name = "no" };
        //    var game = new TicTacToeGame();
        //    game.Players.Add(player1);
        //    game.Players.Add(player2);
        //    var responseDto = _gameMapper.ConvertToResponseDto(game);
        //    //var responseDto = _fixture.Build<TicTacToeGameResponseDto>().With(r => r.PlayerX, playerX).With(r => r.PlayerO, playerO).Create();

        //    _gameManager.Setup(g => g.SelectMarkAsync(gameId, playerId, mark)).ReturnsAsync(game);

        //    //Act
        //    var result = await _controller.SelectMark(playerId, gameId, mark);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<ObjectResult>(result);

        //    var okResult = result as ObjectResult;
        //    Assert.That(okResult.Value, Is.EqualTo(responseDto));
        //}

        [Test]
        public async Task TicTacToeSelectMark_Should_Catch_Unauthorized_Exception_If_GameManager_Throws_Exception()
        {
            //Arrange
            var gameId = 1;
            var playerId = 1;
            var mark = "O";
            _gameManager.Setup(g => g.SelectMarkAsync(gameId, playerId, mark)).Throws<UnauthorizedAccessException>();

            //Act
            var result = await _controller.SelectMark(playerId, gameId, mark);

            //Assert
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        //[Test]
        //public async Task RestartGame_Should_Return_ResponseDto()
        //{
        //    var player1 = new Player { Name = "to" };
        //    var player2 = new Player { Name = "no" };
        //    var game = new TicTacToeGame();
        //    game.Players.Add(player1);
        //    game.Players.Add(player2);

        //    var responseDto = _gameMapper.ConvertToResponseDto(game);

        //    _gameManager.Setup(g => g.RestartGameAsync(game.Id, player1.Id)).ReturnsAsync(game);

        //    //Act
        //    var result = await _controller.RestartGame(game.Id, player1.Id);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<ObjectResult>(result);

        //    var okResult = result as ObjectResult;
        //    Assert.That(okResult.Value, Is.EqualTo(responseDto));
        //}

        [Test]
        public async Task RestartGame_Should_Catch_Exception_If_GameManager_Throws_Exception()
        {
            var gameId = 1;
            int playerId = 1;

            _gameManager.Setup(g => g.RestartGameAsync(gameId, playerId)).Throws<UnauthorizedAccessException>();

            //Act
            var result = await _controller.RestartGame(gameId, playerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
