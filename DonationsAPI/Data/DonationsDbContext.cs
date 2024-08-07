using DonationsAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DonationsAPI.Data
{
    public class DonationsDbContext : DbContext
    {
        public DonationsDbContext(DbContextOptions<DonationsDbContext> options) :base(options)
        {
        }

        public DbSet<Donation> Donations { get; set; }
    }
}
