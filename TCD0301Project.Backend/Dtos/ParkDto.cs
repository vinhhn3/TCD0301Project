using System;

namespace TCD0301Project.Backend.Dtos
{
  public class ParkDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public DateTime Established { get; set; }
  }
}
