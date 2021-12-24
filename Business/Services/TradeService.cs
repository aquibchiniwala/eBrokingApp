using Business.Interfaces;
using Repository.Entity;
using Repository.Interface;
using Repository.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace Business.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository traderOperations;
        private readonly IEquityRepository equityOperations;
        public TradeService(ITradeRepository traderOperations, IEquityRepository equityOperations)
        {
            this.traderOperations = traderOperations;
            this.equityOperations = equityOperations;
        }

        public TradeService(ITradeRepository traderOperations)
        {
            this.traderOperations = traderOperations;
        }

        public Trader GetTraderDetails()
        {
            return traderOperations.GetTraderDetails();
        }

        public Trader AddFunds(double amount)
        {
            Trader trader = GetTraderDetails();
            trader.Funds += (amount - Helper.CalculateFundsSurcharge(amount));
            return traderOperations.UpdateTrader(trader);
        }

        public Trader BuyTrasaction(string equityName, int quantity, DateTime transactionDateTime)
        {
            if (!Helper.IsMarketOpen(transactionDateTime))
            {
                throw new Exception("Market closed! Please place your order from Mondey to Friday between 9AM to 3PM");
            }

            Trader trader = GetTraderDetails();
            Equity equity = equityOperations.GetEquityByName(equityName);

            if (equity==null)
            {
                throw new Exception("Invalid equity name "+equityName);
            }

            double transactionAmount = (equity.CMP * quantity);

            if (transactionAmount > trader.Funds)
            {
                throw new Exception("Insufficient Funds. Please add funds.");
            }

            // on success
            trader.Funds -= transactionAmount;

            var portfolio = trader.Holdings.Where(p => p.Equity.EquityName.Equals(equityName)).Select(e => e).FirstOrDefault<Portfolio>() ?? null;

            if (portfolio == null)
            {
                trader.Holdings.Add(new Portfolio { Equity = equity, Quantity = quantity, CostPrice = equity.CMP });
            }
            else
            {
                // update existing quantity and average price.

                int oldQuantity = portfolio.Quantity;
                double oldCostPrice = portfolio.CostPrice;
                int newQuantity = oldQuantity + quantity;

                trader.Holdings.Remove(portfolio);

                portfolio.CostPrice = ((oldCostPrice*oldQuantity) + (equity.CMP * quantity)) / newQuantity;
                portfolio.Quantity = newQuantity;

                trader.Holdings.Add(portfolio);

            }
            return traderOperations.UpdateTrader(trader);
        }

        public Trader SellTrasaction(string equityName, int quantity, DateTime transactionDateTime)
        {
            if (!Helper.IsMarketOpen(transactionDateTime))
            {
                throw new Exception("Market closed! Please place your order from Mondey to Friday between 9AM to 3PM");
            }

            Trader trader = GetTraderDetails();
            Equity equity = equityOperations.GetEquityByName(equityName);

            if (string.IsNullOrEmpty(equityName))
            {
                throw new Exception("Invalid equity name "+equityName);
            }

            if (!trader.Holdings.Any(e => e.Equity.EquityName.Equals(equityName)))
            {
                throw new Exception(equityName+" Equity not found in portfolio.");
            }

            var portfolio = trader.Holdings.Where(p => p.Equity.EquityName.Equals(equityName)).Select(e => e).FirstOrDefault<Portfolio>() ?? null;

            if (portfolio!=null && portfolio.Quantity<quantity) 
            {
                throw new Exception(equityName+" Equity not in sufficient quantity in portfolio.");
            }

            // on success
            trader.Funds += equity.CMP * quantity;
            int newQuantity = portfolio.Quantity - quantity;
            trader.Holdings.Remove(portfolio);

            if (newQuantity > 0)
            {
                portfolio.Quantity = newQuantity;
                trader.Holdings.Add(portfolio);
            }
            return trader;
        }
    }
}
