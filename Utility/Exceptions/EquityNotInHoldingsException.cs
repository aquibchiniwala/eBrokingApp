using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Exceptions
{
    public class EquityNotInHoldingsException : Exception
    {
        public EquityNotInHoldingsException(string message) : base (message)
        {

        }
    }
}
