using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entity
{
    public class Trader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TraderId { get; set; }
        public string TraderName { get; set; }
        public ICollection<Portfolio> Holdings { get; set; }
        public double Funds { get; set; }
    }
}
