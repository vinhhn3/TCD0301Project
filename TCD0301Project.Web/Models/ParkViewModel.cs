using System;
using System.ComponentModel.DataAnnotations;

namespace TCD0301Project.Web.Models
{
  public class ParkViewModel
  {
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    [Required]
    [StringLength(255)]
    public string State { get; set; }
    [Required]
    public DateTime Established { get; set; }
  }
}
