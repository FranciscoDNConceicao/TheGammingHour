using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UTAD.LAB._2022.TheGammingHour.Models;

namespace UTAD.LAB._2022.TheGammingHour.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UTAD.LAB._2022.TheGammingHour.Models.Jogo> Jogo { get; set; }
    }
}