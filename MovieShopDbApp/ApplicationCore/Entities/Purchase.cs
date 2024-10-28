using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Purchase
    {
        public required int MovieId { get; set; }
        public required int UserId { get; set; }
        public required DateTime PurchaseDateTime { get; set; }
        public required Guid PurchaseNumber { get; set; }
        public required decimal TotalPrice { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
