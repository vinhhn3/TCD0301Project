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

    public bool CreatePark(Park park)
    {
      _context.Parks.Add(park);
      return Save();
    }

    public bool DeletePark(int id)
    {
      var park = GetPark(id);
      if (park == null) return false;
      _context.Parks.Remove(park);
      return Save();
    }

    public Park GetPark(int id)
    {
      var park = _context.Parks
        .SingleOrDefault(t => t.Id == id);
      return park;
    }

    public ICollection<Park> GetParks()
    {
      return _context.Parks
        .OrderBy(p => p.Id)
        .ToList();
    }

    public bool ParkExists(int id)
    {
      return _context.Parks
        .Any(p => p.Id == id);

    }

    public bool ParkExists(string name)
    {
      return _context.Parks
        .Any(p => p.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
    }

    public bool UpdatePark(Park park)
    {
      var parkInDb = _context.Parks.SingleOrDefault(t => t.Id == park.Id);
      if (parkInDb == null) return false;
      parkInDb.Name = park.Name;
      parkInDb.State = park.State;
      parkInDb.CreatedAt = park.CreatedAt;
      parkInDb.Established = park.Established;
      return Save();
    }

    private bool Save()
    {
      return _context.SaveChanges() >= 0 ? true : false;
    }
  }
}
