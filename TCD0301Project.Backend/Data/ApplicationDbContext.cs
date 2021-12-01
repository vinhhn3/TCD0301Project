using Microsoft.EntityFrameworkCore;
using TCD0301Project.Backend.Models;

namespace TCD0301Project.Backend.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {

    }

    public DbSet<Park> Parks { get; set; }
  }
}
