using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tic_Tac_Toe_Web_API;
using Tic_Tac_Toe_Web_API.Enums;
using Tic_Tac_Toe_Web_API.Models;
using Tic_Tac_Toe_Web_API.Models.Dtos;
using Tic_Tac_Toe_Web_API.Models.Interfaces;
using Tic_Tac_Toe_Web_API.Models.Mappers;

namespace TicTacToeWebAPI.Tests.ManagerTests
{
    [TestFixture]
    public class GameManagerTests
    {
        private IGameManager _gameManager;
        private AllGamesMapper _gameMapper;
        List<IGame> _allGames = new List<IGame>();

        private static readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            _gameManager = new GameManager();
            _gameMapper = new AllGamesMapper();
        }

        [Test]
        public void GetAllGames_Should_Return_List_Of_All_Games()
        {
            IGame game = new TicTacToeGame();
            var listOfGames = GetListOfAllGames(_gameManager);

            var responseDto = listOfGames.Select(g => _gameMapper.ConvertToResponseDto(g));

            //Act
            var result = _gameManager.GetAllGames();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(responseDto));
        }

        [Test]
        public void GetGameById_Should_Return_Game_If_Game_Exist()
        {
            //Arrange
            var game = _gameManager.CreateGame();

            //Act
            var result = _gameManager.GetGameById(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.GameId, Is.EqualTo(game.GameId));
        }

        [Test]
        public void GetGameByid_Should_Throw_Exception_If_Game_Not_Found()
        {
            Assert.That(() => _gameManager.GetGameById(1), Throws.TypeOf<Exception>()
                .With
                .Property("Message")
                .EqualTo("Game doesn't exist!"));
        }

        [Test]
        public void CreateGame_Should_Return_Created_Game()
        {
            
        }

        [Test]
        public void JoinGame_Should_Add_Player_In_Existing_Game()
        {
            var game = _gameManager.CreateGame();
            var player = _fixture.Create<Player>();

           var result = _gameManager.JoinGame(game.GameId, player);

            Assert.That(result.Players[0].Id, Is.EqualTo(player.Id));
            Assert.That(result.Players.Count, Is.EqualTo(1));
        }

        [Test]
        public void JoinGame_Should_Add_Second_Player_In_Existing_Game()
        {
            var game = _gameManager.CreateGame();
            var player1 = _fixture.Create<Player>();
            var player2 = _fixture.Create<Player>();
            _gameManager.JoinGame(game.GameId, player1);

            var result = _gameManager.JoinGame(game.GameId, player2);

            Assert.That(result.Players[1].Id, Is.EqualTo(player2.Id));
            Assert.That(result.Players.Count, Is.EqualTo(2));
        }

        [Test]
        public void JoinGame_Should_Throw_Exception_If_Max_Players_Reached()
        {
            var game = _gameManager.CreateGame();
            var player1 = _fixture.Create<Player>();
            var player2 = _fixture.Create<Player>();
            var player3 = _fixture.Create<Player>();
            _gameManager.JoinGame(game.GameId, player1);
            _gameManager.JoinGame(game.GameId, player2);

            Assert.That(() => _gameManager.JoinGame(game.GameId, player3), Throws.TypeOf<Exception>()
                .With
                .Property("Message")
                .EqualTo("Game is already started! You can not join this game!"));
        }

        [Test]
        public void TicTacToeSelectMark_Should_Update_First_Player_Mark()
        {
            var player1 = new Player { Name = "to" };
            //var player2 = new Player { Name = "no" };
            var player2 = new Player { Name = "po" };
            string newMark = "O";
            var game = _gameManager.CreateGame();
            game.Players.Add(player1);
            game.Players.Add(player2);
            game.GameStatus = GameStatus.WaitingForOpponent;

            var updatedGame = _gameManager.TicTacToeSelectMark(game.GameId, game.Players[0].Id, newMark);

            Assert.That(updatedGame, Is.Not.Null);
            Assert.That(updatedGame.PlayerX, Is.EqualTo(player2));

        }

        [Test]
        public void TicTacToeSelectMark_Should_Throw_Exception_If_Player_Doesnt_Exist_In_Game_List_Of_Players()
        {
            var player1 = _fixture.Build<Player>().With(p=>p.Name, "to").Create();
            var player2 = _fixture.Build<Player>().With(p=>p.Name, "no").Create();
            int player3Id = 3;
            string mark = "O";

            var game = _gameManager.CreateGame();
            game.Players.Add(player1);
            game.Players.Add(player2);

            Assert.That(() => _gameManager.TicTacToeSelectMark(game.GameId, player3Id, mark), Throws.TypeOf<UnauthorizedAccessException>());
        }

        [Test]
        public void TicTacToeSelectMark_Should_Throw_Exception_If_Mark_Is_Not_Correct_Symbol()
        {
            var player = _fixture.Build<Player>().With(p => p.Name, "to").Create();
            string mark = "P";

            var game = _gameManager.CreateGame();
            game.Players.Add(player);

            Assert.That(() => _gameManager.TicTacToeSelectMark(game.GameId, player.Id, mark), Throws.TypeOf<InvalidDataException>()
                .With
                .Property("Message")
                .EqualTo("Entered symbol is not valid! Please select X or O !"));
        }

        [Test]
        public void TicTacToeSelectMark_Should_Throw_Exception_If_Second_Player_Tries_To_Select_Mark()
        {
            var player1 = _fixture.Build<Player>().With(p => p.Name, "to").Create();
            var player2 = _fixture.Build<Player>().With(p => p.Name, "no").Create();
            string mark = "O";

            var game = _gameManager.CreateGame();
            game.Players.Add(player1);
            game.Players.Add(player2);

            Assert.That(() => _gameManager.TicTacToeSelectMark(game.GameId, player2.Id, mark), Throws.TypeOf<UnauthorizedAccessException>()
                .With
                .Property("Message")
                .EqualTo("Only first player entered the game can select a mark!"));
        }

        [Test]
        public void TicTacToeRestartGame_Should_Restart_Game_If_First_Player_Restart_The_Game()
        {
            var player1 = _fixture.Build<Player>().With(p => p.Name, "to").Create();
            var player2 = _fixture.Build<Player>().With(p => p.Name, "no").Create();
            //Doesnt work
            //var game = _fixture.Build<TicTacToeGame>().With(g => g.Players[0], player1).With(g => g.Players[1], player2).Create();
            var game = _gameManager.CreateGame();
            game.Players.Add(player1);
            game.Players.Add(player2);
                        var restartedGame = _gameManager.TicTacToeRestartGame(game.GameId, player1.Id);

            Assert.That(restartedGame.Grid, Is.Not.Unique);
        }
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

        private List<IGame> GetListOfAllGames(IGameManager gameManager)
        {
            var field = typeof(GameManager).GetField("_allGames", BindingFlags.NonPublic | BindingFlags.Instance);

            return (List<IGame>)field.GetValue(gameManager);
        }
    }
}
