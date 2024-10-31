using ApplicationCore.Validators;

namespace ApplicationCore.Entities
{
    public class Purchase
    {
        public required int MovieId { get; set; }
        public required int UserId { get; set; }
        [FutureDate]
        public required DateTime PurchaseDateTime { get; set; }
        public required Guid PurchaseNumber { get; set; }
        public required decimal TotalPrice { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
