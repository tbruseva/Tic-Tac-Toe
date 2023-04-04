//using AutoFixture;
//using Tic_Tac_Toe_Web_API.Managers.Interfaces;


//namespace TicTacToeWebAPI.Tests.ManagerTests
//{
//    public class PlayerManager
//    {
//        private IPlayerManager _playerManager;
//        private static readonly Fixture _fixture = new Fixture();

//        [SetUp]
//        public void Setup()
//        {
//            _playerManager = new PlayerManager();
//        }
//        [Test]
//        public async Task GetPlayer_Should_Throw_Exception_When_Player_Doesnt_Exist()
//        {
//            var name = "Guest25";
//            Assert.That(() => GetPlayerAsync(name), Throws.InstanceOf<Exception>()
//                    .With
//                    .Property("Message")
//                    .EqualTo("Player doesn't exist!"));
//        }
//    }
//}
