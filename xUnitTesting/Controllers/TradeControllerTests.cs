using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using Repository.Entity;
using System;
using System.Collections.Generic;
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

        //[Fact]
        //public void TradeController_AddFunds_UpdateFunds()
        //{
        //    Trader trader = new Trader();
        //    //Arrange

        //    var mockTraderService = new MockTradeService().MockGetTraderDetails(trader);

        //    TradeController tradeController = new TradeController(mockTraderService.Object);
        //    //Act
        //    var traderDetails = tradeController.GetPortfolio();

        //    //Assert
        //    Assert.IsAssignableFrom<ActionResult<Trader>>(traderDetails);
        //}
    }
}
