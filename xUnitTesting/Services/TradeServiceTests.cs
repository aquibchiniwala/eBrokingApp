using Business.Services;
using Moq;
using Presentation.Models;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Utility.Exceptions;
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
            var mockEquityRepo = new MockEquityRepository();

            TradeService tradeService = new TradeService(mockTradeRepo.Object,mockEquityRepo.Object);
            //Act
            Trader traderDetails = tradeService.GetTraderDetails();

            //Assert
            Assert.NotNull(traderDetails);
            Assert.IsType<Trader>(traderDetails);
            mockTradeRepo.VerifyGetTraderDetails(Times.AtLeastOnce());
        }


        [Fact]
        public void TradeService_AddFunds_Success()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 20000, Holdings = holdings };
            //Arrange
            var mockTradeRepo = new MockTradeRepository();
            mockTradeRepo.MockGetTraderDetails(trader);
            mockTradeRepo.MockUpdateTrader(trader);
            var mockEquityRepo = new MockEquityRepository();

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            Trader traderDetails = tradeService.AddFunds(1000);

            //Assert
            Assert.NotNull(traderDetails);
            Assert.IsType<Trader>(traderDetails);
        }

        [Fact]
        public void TradeService_BuyTransaction_MarketClosedException()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 20000, Holdings = holdings };
            var equities = new List<Equity> { new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 } };
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 16:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);
            var mockEquityRepo = new MockEquityRepository().MockGetEquityByName(equities[0].EquityName,equities[0]);

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            Action traderDetails = () => tradeService.BuyTransaction(transaction.EquityName,transaction.Quantity,transaction.TransactionDateTime);

            //Assert
            Assert.Throws<MarketClosedException>(traderDetails);
        }

        [Fact]
        public void TradeService_BuyTransaction_InvalidEquityException()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 20000, Holdings = holdings };
            var equities = new List<Equity> { new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 } };
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);
            var mockEquityRepo = new MockEquityRepository().MockGetEquityByName(equities[0].EquityName, equities[0]);

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            Action traderDetails = () => tradeService.BuyTransaction("XYZ", transaction.Quantity, transaction.TransactionDateTime);

            //Assert
            Assert.Throws<InvalidEquityException>(traderDetails);
        }

        [Fact]
        public void TradeService_BuyTransaction_InsufficientFundsException()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 0, Holdings = holdings };
            var equities = new List<Equity> { new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 } };
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);
            var mockEquityRepo = new MockEquityRepository().MockGetEquityByName(equities[0].EquityName, equities[0]);

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            Action traderDetails = () => tradeService.BuyTransaction("TCS", transaction.Quantity, transaction.TransactionDateTime);

            //Assert
            Assert.Throws<InsufficientFundsException>(traderDetails);
        }

        [Fact]
        public void TradeService_BuyTransaction_AddToExistingHolding()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 1000000, Holdings = holdings };
            var equities = new List<Equity> { new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 } };
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);
            var mockEquityRepo = new MockEquityRepository().MockGetEquityByName(equities[0].EquityName, equities[0]);
            mockTradeRepo.MockUpdateTrader(trader);

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            var traderDetails = tradeService.BuyTransaction("TCS", transaction.Quantity, transaction.TransactionDateTime);

            //Assert
            Assert.NotNull(traderDetails);
            Assert.Equal(1, traderDetails.Holdings.Count);
        }

        [Fact]
        public void TradeService_BuyTransaction_AddToNewHolding()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 1000000, Holdings = holdings };
            var equities = new List<Equity> { new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, new Equity { EquityName = "XYZ".ToUpper(), CMP = 2000 } };
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);
            var mockEquityRepo = new MockEquityRepository().MockGetEquityByName(equities[1].EquityName, equities[1]);
            mockTradeRepo.MockUpdateTrader(trader);

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            var traderDetails = tradeService.BuyTransaction("XYZ", transaction.Quantity, transaction.TransactionDateTime);

            //Assert
            Assert.NotNull(traderDetails);
            Assert.Equal(2, traderDetails.Holdings.Count);
        }

        [Fact]
        public void TradeService_SellTransaction_MarketClosedException()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 20000, Holdings = holdings };
            var equities = new List<Equity> { new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 } };
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 16:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);
            var mockEquityRepo = new MockEquityRepository().MockGetEquityByName(equities[0].EquityName, equities[0]);

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            Action traderDetails = () => tradeService.SellTransaction(transaction.EquityName, transaction.Quantity, transaction.TransactionDateTime);

            //Assert
            Assert.Throws<MarketClosedException>(traderDetails);
        }

        [Fact]
        public void TradeService_SellTransaction_InvalidEquityException()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 20000, Holdings = holdings };
            var equities = new List<Equity> { new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 } };
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);
            var mockEquityRepo = new MockEquityRepository().MockGetEquityByName(equities[0].EquityName, equities[0]);

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            Action traderDetails = () => tradeService.SellTransaction("XYZ", transaction.Quantity, transaction.TransactionDateTime);

            //Assert
            Assert.Throws<InvalidEquityException>(traderDetails);
        }

        [Fact]
        public void TradeService_SellTransaction_EquityNotInHoldingsException()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 0, Holdings = holdings };
            var equities = new List<Equity> { new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 } , new Equity { EquityName = "XYZ".ToUpper(), CMP = 2000 } };
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);
            var mockEquityRepo = new MockEquityRepository().MockGetEquityByName(equities[1].EquityName, equities[1]);

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            Action traderDetails = () => tradeService.SellTransaction("XYZ", transaction.Quantity, transaction.TransactionDateTime);

            //Assert
            Assert.Throws<EquityNotInHoldingsException>(traderDetails);
        }


        [Fact]
        public void TradeService_SellTransaction_EquityQuantityNotInHoldingsException()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 0, Holdings = holdings };
            var equities = new List<Equity> { new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 } };
            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 100, TransactionDateTime = DateTime.ParseExact("2021-12-22 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);
            var mockEquityRepo = new MockEquityRepository().MockGetEquityByName(equities[0].EquityName, equities[0]);

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            Action traderDetails = () => tradeService.SellTransaction("TCS", transaction.Quantity, transaction.TransactionDateTime);

            //Assert
            Assert.Throws<EquityNotInHoldingsException>(traderDetails);
        }



        [Fact]
        public void TradeService_SellTransaction_Success()
        {
            var holdings = new List<Portfolio>();
            holdings.Add(new Portfolio { Equity = new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 }, CostPrice = 1500, Quantity = 10 });
            var trader = new Trader { TraderName = "Aquib", Funds = 20000, Holdings = holdings };
            var equities = new List<Equity> { new Equity { EquityName = "TCS".ToUpper(), CMP = 2000 } };

            Transaction transaction = new Transaction { EquityName = "TCS", Quantity = 10, TransactionDateTime = DateTime.ParseExact("2021-12-22 14:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) };

            //Arrange
            var mockTradeRepo = new MockTradeRepository().MockGetTraderDetails(trader);
            var mockEquityRepo = new MockEquityRepository().MockGetEquityByName(equities[0].EquityName, equities[0]);
            mockTradeRepo.MockUpdateTrader(trader);

            TradeService tradeService = new TradeService(mockTradeRepo.Object, mockEquityRepo.Object);
            //Act
            Trader traderDetails = tradeService.SellTransaction(transaction.EquityName, transaction.Quantity, transaction.TransactionDateTime);

            //Assert
            Assert.NotNull(traderDetails);
            Assert.IsType<Trader>(traderDetails);
        }
    }
}
