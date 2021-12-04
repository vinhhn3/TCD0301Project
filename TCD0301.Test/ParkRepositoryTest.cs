using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCD0301Project.Backend.Data;
using TCD0301Project.Backend.Models;
using TCD0301Project.Backend.Repositories;
using TCD0301Project.Backend.Repositories.Interfaces;

namespace TCD0301.Test
{
  public interface IService
  {
    ICollection<Park> GetAll();
    Park GetPark(int id);
    bool Create(Park p);
    bool Delete(int id);
    bool Update(Park p);
  }

  public class Service : IService
  {
    private IParkRepository _parkRepository;
    public Service(IParkRepository parkRepository)
    {
      _parkRepository = parkRepository;
    }

    public ICollection<Park> GetAll()
    {
      return _parkRepository.GetParks();
    }

    public Park GetPark(int id)
    {
      return _parkRepository.GetPark(id);
    }

    public bool Create(Park p)
    {
      return _parkRepository.CreatePark(p);
    }

    public bool Delete(int id)
    {
      return _parkRepository.DeletePark(id);
    }

    public bool Update(Park p)
    {
      return _parkRepository.UpdatePark(p);
    }


  }

  // This is a test class
  [TestFixture]
  public class ParkRepositoryTest
  {
    private Mock<IParkRepository> _mockParkRepository;
    private IService _service;

    [SetUp]
    public void Setup()
    {
      _mockParkRepository = new Mock<IParkRepository>();
      _service = new Service(_mockParkRepository.Object);
      // Init dummy data
      var parks = new List<Park>
      {
        new Park
        {
          Id = 1,
          Name = "Name 1",
          State = "State 1",
          CreatedAt = DateTime.Now,
          Established = DateTime.Now
        },
        new Park
        {
          Id = 2,
          Name = "Name 2",
          State = "State 2",
          CreatedAt = DateTime.Now,
          Established = DateTime.Now
        }
      };

      // Setup action
      _mockParkRepository.Setup(m => m.GetParks()).Returns(parks);
      _mockParkRepository.Setup(m => m.GetPark(It.IsAny<int>()))
        .Returns((int id) =>
          parks.SingleOrDefault(p => p.Id == id)
        );
      _mockParkRepository.Setup(m => m.CreatePark(It.IsAny<Park>()))
        .Returns((Park p) =>
        {
          if (p.Id.Equals(default(int)))
          {
            p.Id = parks.Count + 1;
            parks.Add(p);
            return true;
          }
          else return false;
        });

      _mockParkRepository.Setup(m => m.DeletePark(It.IsAny<int>()))
        .Returns((int id) =>
        {
          var parkToRemoved = parks.SingleOrDefault(t => t.Id == id);
          return parks.Remove(parkToRemoved);
        });

      _mockParkRepository.Setup(m => m.UpdatePark(It.IsAny<Park>()))
        .Returns((Park p) =>
        {
          var park = parks.SingleOrDefault(t => t.Id == p.Id);
          park.State = p.State;
          park.Name = p.Name;
          park.CreatedAt = p.CreatedAt;
          park.Established = p.Established;
          return true;
        });
    }

    // Test case
    [Test]
    public void CanReturnAllParks()
    {
      // Arrange

      // Act
      var parks = _service.GetAll();
      // Assert
      Assert.AreEqual(2, parks.Count);
      Assert.IsNotNull(parks);
    }

    [Test]
    public void CanReturnSinglePark()
    {
      // Arrange

      // Act
      var park = _service.GetPark(1);
      // Assert
      Assert.IsNotNull(park);
      Assert.AreEqual(1, park.Id);
      Assert.AreEqual("Name 1", park.Name);
    }

    [Test]
    public void CanCreatePark()
    {
      // Arrange
      var newPark = new Park
      {
        Name = "Name 3",
        State = "State 3",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      };
      // Act
      var result = _service.Create(newPark);

      // Assert
      Assert.IsTrue(result);

      var park = _service.GetPark(3);

      Assert.AreEqual(newPark.Name, park.Name);
      Assert.AreEqual(3, park.Id);
      Assert.AreEqual(newPark.State, park.State);
    }

    [Test]
    public void CanRemovePark()
    {
      // Arrange
      var idToRemove = 1;
      // Act
      var result = _service.Delete(idToRemove);
      // Assert
      var parkRemoved = _service.GetPark(idToRemove);
      Assert.IsNull(parkRemoved);
      Assert.IsTrue(result);

      var allParks = _service.GetAll();
      Assert.AreEqual(1, allParks.Count);
    }

    [Test]
    public void CanUpdatePark()
    {
      // Arrange
      var parkToUpdate = new Park
      {
        Id = 2,
        Name = "Name 2 updated ...",
        State = "State 2 updated ..."
      };
      // Act
      var result = _service.Update(parkToUpdate);

      // Assert
      Assert.IsTrue(result);

      var park = _service.GetPark(2);
      Assert.AreEqual(parkToUpdate.Name, park.Name);
      Assert.AreEqual(parkToUpdate.State, park.State);
    }
  }
}
