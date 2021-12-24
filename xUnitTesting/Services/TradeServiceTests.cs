using Business.Services;
using Moq;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using xUnitTesting.Mocks.Repositories;

namespace xUnitTesting.Services
{
    public class TradeServiceTests
    {
        // Test Scenario case 1: Not Null Trader

        [Fact]
        public void TradeService_GetTraderDetails_NotNullTrader()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 20000, Holdings = holdings };
            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);

            TradeService tradeService = new TradeService(mockTradeRepo.Object);
            //Act
            Trader traderDetails = tradeService.GetTraderDetails();

            //Assert
            Assert.NotNull(traderDetails);
            Assert.IsType<Trader>(traderDetails);
            mockTradeRepo.VerifyGetTraderDetails(Times.AtLeastOnce());
        }
    }
}
