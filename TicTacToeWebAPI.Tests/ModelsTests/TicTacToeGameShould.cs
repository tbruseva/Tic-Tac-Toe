using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tic_Tac_Toe_Web_API.Models;
using static NUnit.Framework.Constraints.Tolerance;

namespace TicTacToeWebAPI.Tests.ModelsTests
{
    [TestFixture]
    public class TicTacToeGameShould
    {
        [Test]
        public void Constructor_Should_Create_New_Instance()
        {
            // Arrange
            TicTacToeGame game = null;

            // Act
            game = new TicTacToeGame();

            // Assert
            Assert.IsNotNull(game);
        }

        [Test]
        public void Constructor_Should_Create_New_Instance_With_Correct_Name()
        {
            var name = "Tic-Tac-Toe";
            var game = new TicTacToeGame();

            Assert.That(name, Is.EqualTo(game.Name));
        }

        [Test]
        public void Constructor_Should_Create_New_Instance_With_Incremented_Id()
        {
            var game1 = new TicTacToeGame();
            var game2 = new TicTacToeGame();

            Assert.That(game2.Id, Is.EqualTo(game1.Id + 1));
        }


    }
}
