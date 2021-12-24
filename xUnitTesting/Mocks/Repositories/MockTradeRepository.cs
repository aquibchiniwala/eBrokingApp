using Moq;
using Repository.Entity;
using Repository.Interface;
using Repository.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace xUnitTesting.Mocks.Repositories
{
    public class MockTradeRepository : Mock<ITradeRepository>
    {
        public MockTradeRepository MockGetTraderDetails(Trader result)
        {
            Setup(x => x.GetTraderDetails()).Returns(result);

            return this;
        }

        public MockTradeRepository MockUpdateTrader(Trader result)
        {
            Setup(x => x.UpdateTrader(result)).Returns(result);

            return this;
        }

        public MockTradeRepository VerifyGetTraderDetails(Times times)
        {
            Verify(x => x.GetTraderDetails(),times);

            return this;
        }
    }
}
