using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tic_Tac_Toe_Web_API.Controllers;
using Tic_Tac_Toe_Web_API;

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
    }
}
