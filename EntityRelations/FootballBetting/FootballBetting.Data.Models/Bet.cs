using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.Models
{
    public class Bet
    {
        public int BetId { get; set; }

        public decimal Amount { get; set; }

        public decimal Prediction { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
