using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public static class DbInitializer
    {
        public static void Initialize(DBContext context)
        {
            context.Database.EnsureCreated();

            // Look for equities.
            if (context.Equity.Any())
            {
                return;   // DB has been seeded
            }

            var equities = new Equity[]
            {
            new Equity{EquityName="TCS".ToUpper(),CMP=2000},
            new Equity{EquityName="Infy".ToUpper(),CMP=1200},
            new Equity{EquityName="Reliance".ToUpper(),CMP=2300},
            new Equity{EquityName="Wipro".ToUpper(),CMP=620},
            new Equity{EquityName="SBI".ToUpper(),CMP=430},

            };
            foreach (Equity e in equities)
            {
                context.Equity.Add(e);
            }
            context.SaveChanges();

            if (context.Trader.Any())
            {
                return;   // DB has been seeded
            }
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = equities[0], CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 20000, Holdings = holdings };
            context.Trader.Add(trader);
            context.SaveChanges();
        }
    }
}
