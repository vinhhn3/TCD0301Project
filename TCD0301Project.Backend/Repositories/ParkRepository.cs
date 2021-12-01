using System.Collections.Generic;
using System.Linq;
using TCD0301Project.Backend.Data;
using TCD0301Project.Backend.Models;
using TCD0301Project.Backend.Repositories.Interfaces;

namespace TCD0301Project.Backend.Repositories
{
  public class ParkRepository : IParkRepository
  {
    private ApplicationDbContext _context;
    public ParkRepository(ApplicationDbContext context)
    {
      _context = context;
    }
    public ICollection<Park> GetParks()
    {
      return _context.Parks
        .OrderBy(p => p.Id)
        .ToList();
    }
  }
}
