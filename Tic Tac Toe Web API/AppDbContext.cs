using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tic_Tac_Toe_Web_API.Database_Models;

namespace Tic_Tac_Toe_Web_API
{
    public class AppDbContext : DbContext
    {
        public DbSet<PlayerDbModel> Players { get; set; }
        public string DbPath { get; }

        public AppDbContext()
        {
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = @"C:\repos\Tic Tac Toe Web API\Database";
            DbPath = System.IO.Path.Join(path, "TicTacToe.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}

