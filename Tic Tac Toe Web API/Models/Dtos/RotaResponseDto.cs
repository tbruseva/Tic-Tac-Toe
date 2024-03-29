﻿using System.Xml.Linq;
using Tic_Tac_Toe_Web_API.Enums;

namespace Tic_Tac_Toe_Web_API.Models.Dtos
{
    public class RotaResponseDto
    {
        public int Id { get; set; }
        public Mark[] Grid { get; set; } = new Mark[9];
        public Player? PlayerX { get; set; }
        public Player? PlayerO { get; set; }
        public int? CurrentPlayerId { get; set; }
        public GameStatus Status { get; set; }
        public List<int> WinCells { get; set; } = new List<int>();
        public int GameState { get; set; }
        public Dictionary<int, int> CounterWins { get; set; }
        public int CounterTotalGamesPlayed { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as RotaResponseDto;

            if (item == null)
            {
                return false;
            }

            return this.Id == item.Id;
        }

        public override int GetHashCode()
        {
            int hash = Id.GetHashCode();

            return hash;
        }
    }
}
