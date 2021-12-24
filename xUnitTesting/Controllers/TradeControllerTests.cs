using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Presentation.Models;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;
using xUnitTesting.Mocks.Repositories;
using xUnitTesting.Mocks.Services;

namespace xUnitTesting.Controllers
{
    public class TradeControllerTests
    {
        [Fact]
        public void TradeController_GetPortfolio_ValidPortfolio()
        {
            Trader trader = new Trader();
            //Arrange

            var mockTraderService = new MockTradeService().MockGetTraderDetails(trader);

            TradeController tradeController = new TradeController(mockTraderService.Object);
            //Act
            var traderDetails = tradeController.GetPortfolio();

            //Assert
            Assert.IsAssignableFrom<ActionResult<Trader>>(traderDetails);
        }

        [Fact]
        public void TradeController_AddFunds_Success()
        {
            Trader trader = new Trader();
            //Arrange

            var mockTraderService = new MockTradeService().MockAddFunds(trader);

            TradeController tradeController = new TradeController(mockTraderService.Object);
            //Act
            var traderDetails = tradeController.AddFunds(1000);
            var okResult = traderDetails as OkObjectResult;

            //Assert
            Assert.IsAssignableFrom<ActionResult>(traderDetails);
            Assert.NotNull(traderDetails);
            Assert.Equal(200, okResult.StatusCode);
        }           

        [Fact]
        public void TradeController_Buy_Success()
        {
            Trader trader = new Trader();
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 14:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };
         
            //Arrange

            var mockTraderService = new MockTradeService().MockBuyTransaction(transaction.TransactionDateTime,trader);

            TradeController tradeController = new TradeController(mockTraderService.Object);
            //Act
            var traderDetails = tradeController.Buy(transaction);

            var okResult = traderDetails as OkObjectResult;

            //Assert
            Assert.IsAssignableFrom<ActionResult>(traderDetails);
            Assert.NotNull(traderDetails);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void TradeController_Buy_Fail()
        {
            Trader trader = new Trader();
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 16:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange

            var mockTraderService = new MockTradeService().MockBuyTransaction(transaction.TransactionDateTime,trader);

            TradeController tradeController = new TradeController(mockTraderService.Object);
            //Act
            var traderDetails = tradeController.Buy(transaction);


            //Assert
            Assert.IsAssignableFrom<ActionResult>(traderDetails);
            Assert.NotNull(traderDetails);
        }

        [Fact]
        public void TradeController_Sell_Success()
        {
            Trader trader = new Trader();
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 14:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange

            var mockTraderService = new MockTradeService().MockSellTransaction(transaction.TransactionDateTime, trader);

            TradeController tradeController = new TradeController(mockTraderService.Object);
            //Act
            var traderDetails = tradeController.Sell(transaction);

            var okResult = traderDetails as OkObjectResult;

            //Assert
            Assert.IsAssignableFrom<ActionResult>(traderDetails);
            Assert.NotNull(traderDetails);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void TradeController_Sell_Fail()
        {
            Trader trader = new Trader();
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 16:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange

            var mockTraderService = new MockTradeService().MockSellTransaction(transaction.TransactionDateTime, trader);

            TradeController tradeController = new TradeController(mockTraderService.Object);
            //Act
            var traderDetails = tradeController.Sell(transaction);

            var badResult = traderDetails as OkObjectResult;

            //Assert
            Assert.IsAssignableFrom<ActionResult>(traderDetails);
            Assert.NotNull(traderDetails);
        }
    }
}
