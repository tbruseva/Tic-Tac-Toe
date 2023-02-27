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
        public void ConstructorShouldCreateNewInstance()
        {
            // Arrange
            TicTacToeGame game = null;

            // Act
            game = new TicTacToeGame();

            // Assert
            Assert.IsNotNull(game);
        }
    }
}
