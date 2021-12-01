using AutoMapper;
using TCD0301Project.Backend.Dtos;
using TCD0301Project.Backend.Models;

namespace TCD0301Project.Backend.Mapping
{
  public class ApiMapping : Profile
  {
    public ApiMapping()
    {
      CreateMap<Park, ParkDto>().ReverseMap();
    }
  }
}
