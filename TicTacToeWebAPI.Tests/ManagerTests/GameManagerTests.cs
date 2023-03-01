using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tic_Tac_Toe_Web_API;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Interfaces;

namespace TicTacToeWebAPI.Tests.ManagerTests
{
    [TestFixture]
    public class GameManagerTests
    {
        private IGameManager _gameManager;
        //List<IGame> _allGames = new List<IGame>();

        [SetUp]
        public void Setup()
        {
            _gameManager = new GameManager();
        }
        [Test]
        public void GetGameByid_Should_Throw_Exception_If_Game_Not_Found()
        {
            Assert.That(() => _gameManager.GetGameById(1), Throws.TypeOf<Exception>());
        }

        //[Test]
        //public void AddPlayer_Should_Throw_Exception_If_Game_Max_Players_Reached()
        //{
        //    //Arrange
        //    Player player1 = new Player();
        //    Player player2 = new Player();
        //    Player player3 = new Player();
        //    var game = new TicTacToeGame { Id = 1, Name = "Tic-Tac-Toe", CurrentMark = Tic_Tac_Toe_Web_API.Enums.Mark.X, GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Grid = new Tic_Tac_Toe_Web_API.Enums.Mark[9], Players = new List<Player> { player1, player2 } };

           
        //    //Act & Assert
        //    Assert.That(()=> _gameManager.AddPlayer(game, player3), Throws.TypeOf<Exception>());
        //}

        //[Test]
        //public void SelectMark_Should_Throw_Exception_If_Player_Doesnt_Exist_In_Game_List_Of_Players()
        //{
        //    var player1 = new Player { Name = "to" };
        //    var player2 = new Player { Name = "no" };
        //    string username = "po";
        //    string mark = "O";
        //    int gameId = 1;
        //    //var game1 = new TicTacToeGame { Id = 1, Name = "Tic-Tac-Toe", CurrentMark = Tic_Tac_Toe_Web_API.Enums.Mark.X, GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Grid = new Tic_Tac_Toe_Web_API.Enums.Mark[9], Players = new List<Player> { player1, player2 } };


        //    var game = _gameManager.CreateGame();
        //    game.Players.Add(player1);
        //    game.Players.Add(player2);


        //    Assert.That(() => _gameManager.SelectMark(gameId, username, mark), Throws.TypeOf<UnauthorizedAccessException>());
        //}

        //[Test]
        //public void SelectMark_Should_Throw_Exception_If_Mark_Is_Not_Correct_Symbol()
        //{
        //    var player1 = new Player { Name = "to" };
        //    string username = "to";
        //    string mark = "P";
        //    //var game = new TicTacToeGame { Id = 1, Name = "Tic-Tac-Toe", CurrentMark = Tic_Tac_Toe_Web_API.Enums.Mark.X, GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Grid = new Tic_Tac_Toe_Web_API.Enums.Mark[9], Players = new List<Player> { player1} };

        //    var game = _gameManager.CreateGame();
        //    game.Players.Add(player1);

        //    var allGames = _gameManager.GetAllGames();


        //    Assert.That(() => _gameManager.SelectMark(1, username, mark), Throws.TypeOf<InvalidDataException>());
        //}

        //[Test]
        //public void SelectMark_Should_Throw_Exception_If_Second_Player_Tries_To_Select_Mark()
        //{
        //    var player1 = new Player { Name = "to" };
        //    var player2 = new Player { Name = "no" };
        //    string username = "no";
        //    string mark = "O";
        //    var game = new TicTacToeGame { Id = 1, Name = "Tic-Tac-Toe", CurrentPlayerId = Tic_Tac_Toe_Web_API.Enums.Mark.X, GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Grid = new Tic_Tac_Toe_Web_API.Enums.Mark[9], Players = new List<Player> { player1, player2 } };

        //    Assert.That(() => _gameManager.SelectMark(1, username, mark), Throws.TypeOf<Exception>());
        //}

        //[Test]
        //public void RestartGame_Should_Throw_Exception_If_Unauthorized_Player_Tries_To_Restart()
        //{
        //    int gameId = 1;
        //    string username = "po";
        //    var player1 = new Player { Name = "to" };
        //    var player2 = new Player { Name = "no" };
        //    var game = new TicTacToeGame { Id = 1, Name = "Tic-Tac-Toe", CurrentPlayerId = Tic_Tac_Toe_Web_API.Enums.Mark.X, GameStatus = Tic_Tac_Toe_Web_API.Enums.GameStatus.NotStarted, Grid = new Tic_Tac_Toe_Web_API.Enums.Mark[9], Players = new List<Player> { player1, player2 } };

        //    Assert.That(()=>_gameManager.RestartGame(gameId, username),Throws.TypeOf<UnauthorizedAccessException>());
        //}
    }
}
