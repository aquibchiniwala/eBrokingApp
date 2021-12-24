using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Utility;
using Xunit;

namespace xUnitTesting.Utility
{
    public class Helper
    {

        [Theory]
        [InlineData(10000,20)]
        [InlineData(1000,20)]
        public void Helper_CalculateBrokerage_MinFlatBrokerage(double amount, double result)
        {

            var brokerage = Helpers.CalculateBrokerage(amount);
            Assert.Equal(brokerage, result);
        }

        [Theory]
        [InlineData(200000, 100)]
        [InlineData(100000, 50)]
        public void Helper_CalculateBrokerage_DynamicBrokerage(double amount, double result)
        {

            var brokerage = Helpers.CalculateBrokerage(amount);
            Assert.Equal(brokerage, result);
        }


        [Theory]
        [InlineData(10000, 0)]
        [InlineData(200, 0)]
        public void Helper_CalculateFundsSurcharge_ZeroSurcharge(double amount, double result)
        {

            var surcharge = Helpers.CalculateFundsSurcharge(amount);
            Assert.Equal(surcharge, result);
        }

        [Theory]
        [InlineData(400000, 200)]
        [InlineData(200000, 100)]
        public void Helper_CalculateFundsSurcharge_DynamicSurcharge(double amount, double result)
        {

            var surcharge = Helpers.CalculateFundsSurcharge(amount);
            Assert.Equal(surcharge, result);
        }

        [Theory]
        [InlineData("2021-12-22 16:00",false)]
        [InlineData("2021-12-25 12:00",false)]
        [InlineData("2021-12-26 12:00",false)]
        public void Helper_IsMarketOpen_Close(string transactionDateTime, bool result)
        {

            var transDateTime = DateTime.ParseExact(transactionDateTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            var market = Helpers.IsMarketOpen(transDateTime);
            Assert.Equal(market, result);
        }

        [Theory]
        [InlineData("2021-12-22 11:00", true)]
        public void Helper_IsMarketOpen_Open(string transactionDateTime, bool result)
        {

            var transDateTime = DateTime.ParseExact(transactionDateTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            var market = Helpers.IsMarketOpen(transDateTime);
            Assert.Equal(market, result);
        }
    }
}
