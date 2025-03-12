using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDB
{
    public class ReservedBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }
        public DateTime ReservedUntil { get; set; }
    }
}
