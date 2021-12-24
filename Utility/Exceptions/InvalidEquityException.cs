using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Exceptions
{
    public class InvalidEquityException : Exception
    {

        public InvalidEquityException(string message) : base(message)
        {

        }

    }
}
