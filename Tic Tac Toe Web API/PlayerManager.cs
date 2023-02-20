using Tic_Tac_Toe_Web_API.Models;

namespace Tic_Tac_Toe_Web_API
{
    public class PlayerManager : IPlayerManager
    {
        private List<Player> players = new List<Player>();

        public Player GetPlayer(string username)
        {
            foreach (var player in players)
            {
                if (player.Name == username)
                {
                    return player;
                }
            }

            throw new Exception("Player doesn't exist!");
        }

        public bool CheckPlayerExist(string username)
        {
            foreach (var player in players)
            {
                if (player.Name == username)
                {
                    return true;
                }
            }

            return false;
        }

        public Player CreatePlayer(string username)
        {
            //if (CheckPlayerExist(username))
            //{
            //    throw new InvalidOperationException("User with this username already exist! Please choose another name!");
            //}

            var player = new Player();
            player.Name = username;
            //players.Add(player);
            return player;
        }
    }
}
