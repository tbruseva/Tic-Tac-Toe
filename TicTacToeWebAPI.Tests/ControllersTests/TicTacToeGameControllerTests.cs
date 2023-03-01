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

namespace TicTacToeWebAPI.Tests.ControllersTests
{
    [TestFixture]
    public class TicTacToeGameControllerTests
    {
        private TicTacToeController _controller;
        private Mock<IGameManager> _gameManager;
        private Mock<IPlayerManager> _playerManager;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _gameManager = new Mock<IGameManager>();
            _playerManager = new Mock<IPlayerManager>();
            _controller = new TicTacToeController(_gameManager.Object, _playerManager.Object);
        }

        [Test]
        public void CreateGame_Should_Return_Properly_Created_Game()
        {
            //Arrange
            var game = new TicTacToeGame { Id = 1, Name = "Tic-Tac-Toe", CurrentMark = Tic_Tac_Toe_Web_API.Enums.Mark.X, GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Grid = new Tic_Tac_Toe_Web_API.Enums.Mark[9], Players = new List<Player>() };

            _gameManager.Setup(g => g.CreateGame()).Returns(game);

            //Act
            var result = _controller.CreateGame();
            var okResult = result as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            Assert.AreEqual(okResult.Value, game);
        }

        [Test]
        public void JoinGame_Should_Add_Player_To_Existing_Game()
        {
            //Arrange
            var gameId = 1;
            var mark = "O";
            var username = "to";
            var player = new Player { Name = "to", Mark = Tic_Tac_Toe_Web_API.Enums.Mark.O};
            var game = new TicTacToeGame { Id = 1, Name = "Tic-Tac-Toe", CurrentMark = Tic_Tac_Toe_Web_API.Enums.Mark.X, GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Grid = new Tic_Tac_Toe_Web_API.Enums.Mark[9], Players = new List<Player>{player}};

            _playerManager.Setup(p=>p.CreatePlayer(username)).Returns(player);
            _gameManager.Setup(g => g.CreateGame()).Returns(game);
            _gameManager.Setup(g => g.JoinGame(gameId, player, mark)).Returns(game);


            //Act
            var createdGame = _controller.CreateGame();
            var result = _controller.JoinGame(gameId, username, mark);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);

            var okResult = result as ObjectResult;
            Assert.AreEqual(okResult.Value, game);
        }
    }
}
