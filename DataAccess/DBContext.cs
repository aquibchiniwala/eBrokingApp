using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DBContext()
        {
                
        }

        public DbSet<Trader> Trader { get; set; }
        public DbSet<Equity> Equity { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
    }
}
