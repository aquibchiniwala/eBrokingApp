using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entity
{
    public class Equity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquityId { get; set; }
        public string EquityName { get; set; }
        public double CMP { get; set; }
    }
}
