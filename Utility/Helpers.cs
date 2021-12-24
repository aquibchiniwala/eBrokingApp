using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public static class Helpers
    {
        public static double CalculateBrokerage(double transactionAmount)
        {
            if ((transactionAmount * 0.05 / 100) < 20)
            {
                return 20;
            }
            else
            {
                return (transactionAmount * 0.05) / 100;
            }
        }

        public static double CalculateFundsSurcharge(double amount)
        {
            if (amount > 100000)
            {
                return (amount * 0.05) / 100;
            }
            else
            {
                return 0;
            }
        }

        public static bool IsMarketOpen(DateTime transactionDateTime)
        {
            if ((transactionDateTime.DayOfWeek != DayOfWeek.Saturday && transactionDateTime.DayOfWeek != DayOfWeek.Sunday) && (transactionDateTime.Hour >= 9 && transactionDateTime.Hour <= 15))
            {
                return true;
            }
            return false;
        }
    }
}
