using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using Xunit;

namespace xUnitTesting.Helpers
{
    public class BrokerageCalculator
    {

        [Theory]
        [InlineData(10000,20)]
        [InlineData(10000,10)]
        public void HelperService_GetBrokerage_ValidCompleteLeague(double amount, double result)
        {

            var brokerage = Helper.CalculateBrokerage(amount);
            Assert.Equal(brokerage, result);
        }
    }
}
