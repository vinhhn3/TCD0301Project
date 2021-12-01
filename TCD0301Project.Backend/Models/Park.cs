using System;
using System.ComponentModel.DataAnnotations;

namespace TCD0301Project.Backend.Models
{
  public class Park
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    public string State { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime Established { get; set; }
  }
}
