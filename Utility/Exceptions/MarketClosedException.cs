using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Exceptions
{
    public class MarketClosedException : Exception
    { 

        public MarketClosedException(string message) : base(message)
        {

        }

    }
}
