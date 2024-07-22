using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string UserId { get; set; }
        public int StockId { get; set; }
        public User User { get; set; } // nav props used inside db context for creating the db relations
        public Stock Stock { get; set;  }
    }
}