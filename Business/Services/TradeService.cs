using Business.Interfaces;
using Repository.Entity;
using Repository.Interface;
using Repository.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using Utility.Exceptions;

namespace Business.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository traderRepository;
        private readonly IEquityRepository equityRepository;
        public TradeService(ITradeRepository traderRepository, IEquityRepository equityRepository)
        {
            this.traderRepository = traderRepository;
            this.equityRepository = equityRepository;
        }

        public Trader GetTraderDetails()
        {
            return traderRepository.GetTraderDetails();
        }

        public Trader AddFunds(double amount)
        {
            Trader trader = GetTraderDetails();
            trader.Funds += (amount - Helpers.CalculateFundsSurcharge(amount));
            return traderRepository.UpdateTrader(trader);
        }

        public Trader BuyTransaction(string equityName, int quantity, DateTime transactionDateTime)
        {
            if (!Helpers.IsMarketOpen(transactionDateTime))
            {
                throw new MarketClosedException("Market closed! Please place your order from Mondey to Friday between 9AM to 3PM");
            }

            Trader trader = GetTraderDetails();
            Equity equity = equityRepository.GetEquityByName(equityName);

            if (equity==null)
            {
                throw new InvalidEquityException("Invalid equity name "+equityName);
            }

            double transactionAmount = (equity.CMP * quantity);

            if (transactionAmount > trader.Funds)
            {
                throw new InsufficientFundsException("Insufficient Funds. Please add funds.");
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
            return traderRepository.UpdateTrader(trader);
        }

        public Trader SellTransaction(string equityName, int quantity, DateTime transactionDateTime)
        {
            if (!Helpers.IsMarketOpen(transactionDateTime))
            {
                throw new MarketClosedException("Market closed! Please place your order from Mondey to Friday between 9AM to 3PM");
            }

            Trader trader = GetTraderDetails();
            Equity equity = equityRepository.GetEquityByName(equityName);

            if (equity==null)
            {
                throw new InvalidEquityException("Invalid equity name "+equityName);
            }

            if (!trader.Holdings.Any(e => e.Equity.EquityName.Equals(equityName)))
            {
                throw new EquityNotInHoldingsException(equityName+" Equity not found in portfolio.");
            }

            var portfolio = trader.Holdings.Where(p => p.Equity.EquityName.Equals(equityName)).Select(e => e).FirstOrDefault<Portfolio>() ?? null;

            if (portfolio!=null && portfolio.Quantity<quantity) 
            {
                throw new EquityNotInHoldingsException(equityName+" Equity not in sufficient quantity in portfolio.");
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
