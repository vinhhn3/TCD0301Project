using System.Collections;
using System.Collections.Generic;
using TCD0301Project.Backend.Models;

namespace TCD0301Project.Backend.Repositories.Interfaces
{
  public interface IParkRepository
  {
    ICollection<Park> GetParks();
    Park GetPark(int id);
    bool ParkExists(int id);
    bool ParkExists(string name);
    bool CreatePark(Park park);
    bool UpdatePark(Park park);
    bool DeletePark(int id);
  }
}
