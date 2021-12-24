using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Transaction
    {
        public string EquityName { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDateTime { get; set; }

        public Transaction()
        {
            TransactionDateTime = DateTime.Now;
        }
    }
}
