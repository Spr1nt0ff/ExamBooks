using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDB
{
    public class SalesReport
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
