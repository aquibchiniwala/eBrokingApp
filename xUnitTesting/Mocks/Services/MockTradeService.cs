using Business.Interfaces;
using Moq;
using Repository.Entity;
using Repository.Interface;
using Repository.Operations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace xUnitTesting.Mocks.Services
{
    public class MockTradeService : Mock<ITradeService>
    {
        public MockTradeService MockGetTraderDetails(Trader result)
        {
            Setup(x => x.GetTraderDetails()).Returns(result);

            return this;
        }


        public MockTradeService MockAddFunds(Trader result)
        {
            Setup(x => x.AddFunds(It.IsAny<double>())).Returns(result);

            return this;
        }

        public MockTradeService MockBuyTransaction(DateTime transactionDateTime, Trader result)
        {

            Setup(x => x.BuyTransaction(It.IsAny<string>(), It.IsAny<int>(), transactionDateTime)).Returns(result);

            return this;
        }

        public MockTradeService MockSellTransaction(DateTime transactionDateTime, Trader result)
        {
            Setup(x => x.SellTransaction(It.IsAny<string>(), It.IsAny<int>(), transactionDateTime)).Returns(result);

            return this;
        }
    }
}
