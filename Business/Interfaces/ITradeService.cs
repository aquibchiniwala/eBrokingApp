using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface ITradeService
    {
        public Trader GetTraderDetails();
        public Trader AddFunds(double amount);
        public Trader BuyTrasaction(string equityName, int quantity, DateTime transactionDateTime);
        public Trader SellTrasaction(string equityName, int quantity, DateTime transactionDateTime);
    }
}
