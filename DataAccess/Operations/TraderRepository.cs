using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Operations
{
    public class TraderRepository : ITradeRepository
    {
        private readonly DBContext context;

        public TraderRepository(DBContext context)
        {
            this.context = context;
        }

        public Trader GetTraderDetails()
        {
            // As there is only one trader in the app.
            return context.Trader.Include(h => h.Holdings).ThenInclude(e => e.Equity).FirstOrDefault();
        }

        public Trader UpdateTrader(Trader trader)
        {
            context.Trader.Update(trader);
            context.SaveChanges();
            return GetTraderDetails();
        }
    }
}
