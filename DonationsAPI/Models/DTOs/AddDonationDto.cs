namespace DonationsAPI.Models.DTOs
{
    public class AddDonationDto
    {
        public string? Donor { get; set; } = "Anonymous";
        public required decimal Amount { get; set; }
        public string? Message { get; set; } = "No message";
    }
}
