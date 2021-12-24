using Business.Interfaces;
using Moq;
using Repository.Entity;
using Repository.Interface;
using Repository.Operations;
using System;
using System.Collections.Generic;
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
    }
}
