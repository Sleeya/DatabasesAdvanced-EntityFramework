using FootballBetting.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballBetting
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (FootballBettingContext context = new FootballBettingContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
