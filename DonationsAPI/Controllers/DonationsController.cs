using DonationsAPI.Data;
using DonationsAPI.Models.DTOs;
using DonationsAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationsController : ControllerBase
    {
        private readonly DonationsDbContext dbContext;

        public DonationsController(DonationsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetDonations(string sortBy = "amount", string sortOrder = "asc")
        {
            var donations = dbContext.Donations.AsQueryable();
            bool isSortOrderDescending = sortOrder.Equals("desc", StringComparison.CurrentCultureIgnoreCase);

            donations = sortBy.ToLower() switch
            {
                "donor" => isSortOrderDescending
                    ? donations.OrderByDescending(donation => donation.Donor)
                    : donations.OrderBy(donation => donation.Donor),
                "amount" => isSortOrderDescending
                    ? donations.OrderByDescending(donation => donation.Amount)
                    : donations.OrderBy(donation => donation.Amount),
                "message" => isSortOrderDescending 
                    ? donations.OrderByDescending(donation => donation.Message)
                    : donations.OrderBy(donation => donation.Message),
                _ => isSortOrderDescending
                    ? donations.OrderByDescending(donation => donation.Amount)
                    : donations.OrderBy(donation => donation.Amount), 
            };

            return Ok(donations.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetDonationById(Guid id)
        {
            var donation = dbContext.Donations.Find(id);

            if(donation is null)
            {
                return NotFound();
            }

            return Ok(donation);
        }

        [HttpPost]
        public IActionResult AddDonation(AddDonationDto addDonationDto)
        {
            if (addDonationDto.Amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            var donation = new Donation()
            { 
                Donor = addDonationDto.Donor,
                Amount = addDonationDto.Amount,
                Message = addDonationDto.Message,
            };

            dbContext.Donations.Add(donation);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetDonationById), new { id = donation.Id }, donation);
        }
    }
}
