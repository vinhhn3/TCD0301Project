using System.Collections;
using System.Collections.Generic;
using TCD0301Project.Backend.Models;

namespace TCD0301Project.Backend.Repositories.Interfaces
{
  public interface IParkRepository
  {
    ICollection<Park> GetParks();
  }
}
