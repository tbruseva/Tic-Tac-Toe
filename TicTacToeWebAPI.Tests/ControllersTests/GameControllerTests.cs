﻿using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.CompilerServices;
using Tic_Tac_Toe_Web_API.Controllers;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models.Mappers;

namespace TicTacToeWebAPI.Tests.ControllersTests
{
    [TestFixture]
    public class GameControllerTests
    {
        private GameController _controller;
        private Mock<IGameManager> _gameManager;
        private Mock<IPlayerManager> _playerManager;
        private AllGamesMapper _gamesMapper;
        private PlayerMapper _playerMapper;

        private static readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            // Arrange
            _gameManager = new Mock<IGameManager>();
            _playerManager = new Mock<IPlayerManager>();
            _gamesMapper = new AllGamesMapper();
            _playerMapper = new PlayerMapper();
            _controller = new GameController(_gameManager.Object, _playerManager.Object, _gamesMapper, _playerMapper);
        }

        [Test]
        public async Task AllGames_Should_Return_ResponseDto()
        {
            //Arrange
            var listAllGames = new List<IGame>();
            var responseDto = listAllGames.Select(g => _gamesMapper.ConvertToResponseDto(g));

            _gameManager.Setup(g => g.GetAllGamesAsync()).ReturnsAsync(listAllGames);

            //Act
            var result = await _controller.AllGames();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public async Task AllGames_Should_Return_ResonseDto_With_Empty_List_If_No_Games_Are_Created()
        {
            //Arrange
            var listAllGames = new List<IGame>();

            _gameManager.Setup(g => g.GetAllGamesAsync()).ReturnsAsync(listAllGames);

            //Act
            var result = await _controller.AllGames();
            var responseDto = listAllGames.Select(g => _gamesMapper.ConvertToResponseDto(g));

            //Assert
            Assert.IsNotNull(responseDto);
            Assert.IsInstanceOf<IEnumerable<AllGamesResponseDto>>(responseDto);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public async Task CreateGame_Should_Return_ResponseDto()
        {
            //Arrange
            var game = _fixture.Create<TicTacToeGame>();
            _gameManager.Setup(g => g.CreateGameAsync("Tic-Tac-Toe")).ReturnsAsync(game);

            //Act
            var result = await _controller.CreateGame("Tic-Tac-Toe");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
        }

        [Test]
        public async Task Create_Game_Should_return_Created_Game()
        {
            //Arrange
            var game = _fixture.Create<TicTacToeGame>();
            _gameManager.Setup(g => g.CreateGameAsync("Tic-Tac-Toe")).ReturnsAsync(game);

            //Act
            var result = await _controller.CreateGame("Tic-Tac-Toe");
            var responseDto = _gamesMapper.ConvertToResponseDto(game);

            //Assert
            Assert.IsInstanceOf<AllGamesResponseDto>(responseDto);
            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public async Task CreatePlayer_Should_Return_ResponseDto()
        {
            //Arrange
            string username = "to";
            var player = _fixture.Build<Player>().With(p => p.Name, username).Create();

            _playerManager.Setup(g => g.CreatePlayerAsync(username)).ReturnsAsync(player);
            var responseDto = _playerMapper.ConvertToResponseDto(player);

            // Act
            var result = await _controller.CreatePlayer(username);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public async Task CreatePlayer_Should_Catch_Exception_If_PlayerManager_Throws_Exception()
        {
            //Arrange
            string username = "to";
            _playerManager.Setup(g => g.CreatePlayerAsync(username)).Throws<InvalidOperationException>();

            //Act
            var result = await _controller.CreatePlayer(username);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task JoinGame_Should_Return_ResponseDto()
        {
            //Arrange
            Player player = _fixture.Create<Player>();
            var game = _fixture.Create<TicTacToeGame>();
            _playerManager.Setup(g => g.GetPlayerAsync(player.Id)).ReturnsAsync(player);
            _gameManager.Setup(g => g.JoinGameAsync(game.Id, player)).ReturnsAsync(game);
            var responseDto = _gamesMapper.ConvertToResponseDto(game);

            //Act
            var result = await _controller.JoinGame(game.Id, player.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(responseDto));
        }

        [Test]
        public async Task JoinGame_Should_Catch_Exception_When_PlayerManager_Throws_Exception()
        {
            //Arrange
            int gameId = 1;
            int playerId = 3;

            _playerManager.Setup(g => g.GetPlayerAsync(playerId)).Throws<Exception>();

            //Act
            var result = await _controller.JoinGame(gameId, playerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task JoinGame_Should_Catch_Exception_When_GameManager_Throws_Exception()
        {
            //Arrange
            int gameId = 1;
            Player player = _fixture.Create<Player>();

            _playerManager.Setup(g => g.GetPlayerAsync(player.Id)).ReturnsAsync(player);
            _gameManager.Setup(g => g.JoinGameAsync(gameId, player)).Throws<Exception>();

            //Act
            var result = await _controller.JoinGame(gameId, player.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
