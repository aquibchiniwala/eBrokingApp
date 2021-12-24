using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entity
{
    public class Portfolio
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PortfolioId { get; set; }
        public Equity Equity { get; set; }
        public double CostPrice { get; set; }
        public int Quantity { get; set; }
    }
}
