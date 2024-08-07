namespace DonationsAPI.Models.Entities
{
    public class Donation
    {
        public Guid Id { get; set; }
        public string? Donor { get; set; }
        public required decimal Amount { get; set; }
        public string? Message { get; set; }
    }
}
