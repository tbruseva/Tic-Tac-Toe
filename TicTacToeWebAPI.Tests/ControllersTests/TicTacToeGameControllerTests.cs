using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tic_Tac_Toe_Web_API.Controllers;
using Tic_Tac_Toe_Web_API;
using Tic_Tac_Toe_Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Interfaces;
using NUnit.Framework.Internal;
using AutoFixture;

namespace TicTacToeWebAPI.Tests.ControllersTests
{
    [TestFixture]
    public class TicTacToeGameControllerTests
    {
        private TicTacToeController _controller;
        private Mock<IGameManager> _gameManager;
        private Mock<IPlayerManager> _playerManager;

        private static readonly Fixture _fixture = new Fixture();


        [SetUp]
        public void Setup()
        {
            // Arrange
            _gameManager = new Mock<IGameManager>();
            _playerManager = new Mock<IPlayerManager>();
            _controller = new TicTacToeController(_gameManager.Object, _playerManager.Object);
        }

        [Test]
        public void GetGameById_Should_Return_Game_If_Game_Exists()
        {
            //Arrange
            var playerX = new Player { Name = "to" };
            var playerO = new Player { Name = "no" };
            var responseDto = _fixture.Build<TicTacToeGameResponseDto>().With(r => r.PlayerX, playerX).With(r => r.PlayerO, playerO).Create();

            _gameManager.Setup(g => g.GetGameById(responseDto.GameId)).Returns(responseDto);

            //Act
            var result = _controller.GetGameById(responseDto.GameId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }


        [Test]
        public void GetGameById_Should_Catch_Exception_If_GameManager_Throws_Exception()
        {
            //Arrange
            int gameId = 1;
            _gameManager.Setup(g => g.GetGameById(gameId)).Throws<Exception>();

            //Act
            var result = _controller.GetGameById(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void MakeMove_Should_Return_ResponseDto()
        {
            int playerId = 1;
            int gameId = 1;
            int rowPosition = 0;
            int colPosition = 0;
            var playerX = new Player { Name = "to" };
            var playerO = new Player { Name = "no" };
            var responseDto = _fixture.Build<TicTacToeGameResponseDto>().With(r=> r.PlayerX, playerX).With(r=>r.PlayerO, playerO).Create();
            
            _gameManager.Setup(g => g.TicTacToeMakeMove(playerId, gameId, rowPosition, colPosition)).Returns(responseDto);

            //Act
            var result = _controller.MakeMove(playerId, gameId, rowPosition, colPosition);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public void MakeMove_Should_Catch_Exception_If_GameManager_Throws_Exception()
        {
            int playerId = 1;
            int gameId = 1;
            int rowPosition = 0;
            int colPosition = 0;
            _gameManager.Setup(g => g.TicTacToeMakeMove(playerId, gameId, rowPosition, colPosition)).Throws<UnauthorizedAccessException>();

            //Act
            var result = _controller.MakeMove(playerId, gameId, rowPosition, colPosition);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        

        [Test]
        public void TicTacToeSelectMark_Should_Return_ResponseDto()
        {
            //Arrage
            var gameId = 1;
            var playerId = 1;
            var mark = "O";
            var playerX = new Player { Name = "to" };
            var playerO = new Player { Name = "no" };
            var responseDto = _fixture.Build<TicTacToeGameResponseDto>().With(r=> r.PlayerX, playerX).With(r=>r.PlayerO, playerO).Create();

            _gameManager.Setup(g => g.TicTacToeSelectMark(gameId, playerId, mark)).Returns(responseDto);

            //Act
            var result = _controller.SelectMark(playerId, gameId, mark);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public void TicTacToeSelectMark_Should_Catch_Unauthorized_Exception_If_GameManager_Throws_Exception()
        {
            //Arrange
            var gameId = 1;
            var playerId = 1;
            var mark = "O";
            _gameManager.Setup(g => g.TicTacToeSelectMark(gameId, playerId, mark)).Throws<UnauthorizedAccessException>();

            //Act
            var result = _controller.SelectMark(playerId, gameId, mark);

            //Assert
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void RestartGame_Should_Return_ResponseDto_If_Game_Is_Successfuly_Restarted()
        {
            var gameId = 1;
            int playerId = 1;
            var playerX = new Player { Name = "to" };
            var playerO = new Player { Name = "no" };
            var responseDto = _fixture.Build<TicTacToeGameResponseDto>().With(r => r.PlayerX, playerX).With(r => r.PlayerO, playerO).Create();


            _gameManager.Setup(g => g.TicTacToeRestartGame(gameId, playerId)).Returns(responseDto);

            //Act
            var result = _controller.RestartGame(gameId, playerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public void RestartGame_Should_Catch_Exception_If_GameManager_Throws_Exception()
        {
            var gameId = 1;
            int playerId = 1;

            _gameManager.Setup(g => g.TicTacToeRestartGame(gameId, playerId)).Throws<UnauthorizedAccessException>();

            //Act
            var result = _controller.RestartGame(gameId, playerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
