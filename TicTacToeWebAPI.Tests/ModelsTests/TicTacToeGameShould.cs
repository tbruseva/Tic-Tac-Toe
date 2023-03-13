//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Tic_Tac_Toe_Web_API.Models;
//using static NUnit.Framework.Constraints.Tolerance;

//namespace TicTacToeWebAPI.Tests.ModelsTests
//{
//    [TestFixture]
//    public class TicTacToeGameShould
//    {
//        [Test]
//        public void Constructor_Should_Create_New_Instance()
//        {
//            // Arrange
//            TicTacToeGame game = null;

//            // Act
//            game = new TicTacToeGame();

//            // Assert
//            Assert.IsNotNull(game);
//        }

//        [Test]
//        public void Constructor_Should_Create_New_Instance_With_Correct_Name()
//        {
//            var name = "Tic-Tac-Toe";
//            var game = new TicTacToeGame();

//            Assert.That(name, Is.EqualTo(game.Name));
//        }

//        [Test]
//        public void Constructor_Should_Create_New_Instance_With_Incremented_Id()
//        {
//            var game1 = new TicTacToeGame();
//            var game2 = new TicTacToeGame();

//            Assert.That(game2.Id, Is.EqualTo(game1.Id + 1));
//        }

//        [Test]
//        public void JoinGame_Should_Add_First_Player_To_Existing_Game()
//        {
//            //Arrange
//            var game = new TicTacToeGame();
//            var player = new Player();

//            //Act
//            game.JoinGame(player);

//            //Assert
//            Assert.That(game.Players.Count, Is.EqualTo(1));
//            Assert.That(game.Players[0], Is.EqualTo(player));
//        }

//        [Test]
//        public void JoinGame_Should_Add_Second_Player_To_Existing_Game()
//        {
//            //Arrange
//            var player1 = new Player();
//            var player2 = new Player();
//            var game = new TicTacToeGame { };
//            game.Players.Add(player1);
//            game.GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.WaitingForOpponent;

//            //Act
//            game.JoinGame(player2);

//            //Assert
//            Assert.That(game.Players.Count, Is.EqualTo(2));
//            Assert.That(game.Players[1], Is.EqualTo(player2));
//        }

//        [Test]
//        public void JoinGame_Should_Throw_Exception_If_Third_Player_Tries_To_Join_The_Game()
//        {
//            //Arrange
//            var player1 = new Player();
//            var player2 = new Player();
//            var player3 = new Player(); 
//            var game = new TicTacToeGame { };
//            game.Players.Add(player1);
//            game.Players.Add(player2);
//            game.GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.WaitingForOpponent;

//            //Act & Assert
//            Assert.That(() => game.JoinGame(player3), Throws.TypeOf<Exception>()
//                .With
//                .Property ("Message")
//                .EqualTo("Game is already started! You can not join this game!"));
//        }

//        //[Test]
//        //public void JoinGame_Should_Throw_Exception_If_Third_Player_Tries_To_Join_The_Game()
//        //{
//        //    //Arrange
//        //    var player1 = new Player();
//        //    var player2 = new Player();
//        //    var player3 = new Player();
//        //    var game = new TicTacToeGame { };
//        //    game.Players.Add(player1);
//        //    game.Players.Add(player2);
//        //    game.GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.WaitingForOpponent;

//        //    //Act & Assert
//        //    Assert.That(() => game.JoinGame(player3), Throws.TypeOf<Exception>()
//        //        .With
//        //        .Property("Message")
//        //        .EqualTo("Game is already started! You can not join this game!"));
//        //}
//    }
//}
