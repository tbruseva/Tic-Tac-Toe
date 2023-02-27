using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tic_Tac_Toe_Web_API;
using Tic_Tac_Toe_Web_API.Controllers;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models;
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
            var games = new List<IGame> { new TicTacToeGame { Id = 1, Name = "Tic-Tac-Toe", CurrentMark = Tic_Tac_Toe_Web_API.Enums.Mark.X, GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Grid = new Tic_Tac_Toe_Web_API.Enums.Mark[9], Players = new List<Player>() } };
            _gameManager.Setup(g => g.GetAllGames()).Returns(games);

            //Act
            var result = _controller.AllGames();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.AreEqual(games, okResult.Value);  
        }

        [Test]
        public void AllGames_Should_Return_Empty_List_If_No_Games_Are_Created()
        {
            //Arrange
            var games = new List<IGame>();
            _gameManager.Setup(g => g.GetAllGames()).Returns(games);

            //Act
            var result = _controller.AllGames();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.AreEqual(games, okResult.Value);
        }

        [Test]
        public void GetGameById_Should_Return_Game_If_Game_Exists()
        {
            //Arrange
            int gameId = 1;
            var game = new TicTacToeGame { Id = 1, Name = "Tic-Tac-Toe", CurrentMark = Tic_Tac_Toe_Web_API.Enums.Mark.X, GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Grid = new Tic_Tac_Toe_Web_API.Enums.Mark[9], Players = new List<Player>() };

            _gameManager.Setup(g=>g.GetGameById(gameId)).Returns(game);

            //Act
            var result = _controller.GetGameById(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.AreEqual(game, okResult.Value);

        }

        [Test]
        public void GetGameById_Should_Catch_Exception_If_Game_Doesnot_Exists()
        {
            //Arrange
            int gameId = 1;

            //Act
            var result = _controller.GetGameById(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void SelectFirstOrSecondPlayer_Should_Return_Updated_Player_Details()
        { 
            //Arrage
            var gameId = 1;
            var username = "to";
            var mark = "O";
            var player = new Player {Name = "to", Mark = Tic_Tac_Toe_Web_API.Enums.Mark.O};
            var game = new TicTacToeGame { Id = 1, Name = "Tic-Tac-Toe", CurrentMark = Tic_Tac_Toe_Web_API.Enums.Mark.X, GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Grid = new Tic_Tac_Toe_Web_API.Enums.Mark[9], Players = new List<Player> { player } };
            _gameManager.Setup(g => g.SelectFirstOrSecondPlayer(gameId, username, mark)).Returns(player);

            //Act
            var result = _controller.SelectFirstOrSecondPlayer(username, gameId, mark);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.AreEqual(player, okResult.Value);
        }
    }
}
