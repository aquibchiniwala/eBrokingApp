using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface ITradeRepository
    {
        Trader GetTraderDetails();
        Trader UpdateTrader(Trader trader);

    }
}
