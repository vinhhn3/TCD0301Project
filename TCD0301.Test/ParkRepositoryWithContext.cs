using Microsoft.EntityFrameworkCore;
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
  internal class ParkRepositoryWithContext
  {
    private IParkRepository _parkRepository;
    private ApplicationDbContext _mockContext;

    [OneTimeSetUp]
    public void Setup()
    {
      // Config the DbContextInMemory, it will be used for testing
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

      // Init the mock context
      _mockContext = new ApplicationDbContext(options);

      // Add some data to the context
      _mockContext.Parks.Add(new Park
      {
        Id = 1,
        Name = "Name 1",
        State = "State 1",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      });
      _mockContext.Parks.Add(new Park
      {
        Id = 2,
        Name = "Name 2",
        State = "State 2",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      });

      _mockContext.SaveChanges();

      // Init the parkRepository
      _parkRepository = new ParkRepository(_mockContext);
    }
    /// <summary>
    /// First Test case: Can receive all the parks 
    /// </summary>
    [Test]
    public void GetParks_ReturnAllParks()
    {
      // Arrange
      // Act
      var outputParks = _parkRepository.GetParks();
      // Assert
      Assert.AreEqual(2, outputParks.Count);
      Assert.IsTrue(outputParks.Any(t => t.Name.Equals("Name 1")));
      Assert.IsTrue(outputParks.Any(t => t.Name.Equals("Name 2")));
    }

    /// <summary>
    /// Second Test Case: Can get the park with Id
    /// </summary>
    [Test]
    public void GetPark_IdExistsInTheDatabase_ReturnsTheParkItemWithCorrectId()
    {
      // Arrange
      var id = 1;
      // Act
      var outputPark = _parkRepository.GetPark(id);
      // Assert
      Assert.AreEqual(id, outputPark.Id);
      Assert.AreEqual("Name 1", outputPark.Name);
    }

    /// <summary>
    /// 3rd Test Case: If Id does not exists in the database, returns null
    /// </summary>
    [Test]
    public void GetPark_IdNotExistInTheDatabase_ReturnsNull()
    {
      // Arrange
      var id = 10;
      // Act
      var park = _parkRepository.GetPark(id);
      // Assert
      Assert.IsNull(park);
    }
    /// <summary>
    /// Fourth Test Case: If item with Id exists, return true
    /// </summary>
    [Test]
    public void ParkExists_ItemWithIdExists_ReturnTrue()
    {
      // Arrange
      var id = 1;
      // Act
      var result = _parkRepository.ParkExists(id);
      // Assert
      Assert.IsTrue(result);
    }
    /// <summary>
    /// Fifth Test Case: If item with Id not exists, return false
    /// </summary>
    [Test]
    public void ParkExists_ItemWithIdNotExists_ReturnFalse()
    {
      // Arrange
      var id = 10;
      // Act
      var result = _parkRepository.ParkExists(id);
      // Assert
      Assert.IsFalse(result);
    }

    /// <summary>
    /// Sixth Test Case: 
    ///   If item with Name Exists, return true
    ///   If item with Name does not Exists, return False
    /// </summary>
    [TestCase("Name 1", true)]
    [TestCase("Name 10", false)]
    public void ParkExists_CheckIfItemWithNameExists(string inputName, bool expectedResult)
    {
      // Arrange

      // Act
      var actualResult = _parkRepository.ParkExists(inputName);
      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    /// <summary>
    /// Seventh Test Case: Add new Park successfully, return true
    /// </summary>
    [Test]
    public void CreatePark_Succesfully_ReturnsTrue()
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
      var result = _parkRepository.CreatePark(newPark);
      // Assert
      var parks = _parkRepository.GetParks();
      Assert.AreEqual(3, parks.Count);

      var isExist = _parkRepository.ParkExists(newPark.Name);
      Assert.IsTrue(isExist);
    }

    /// <summary>
    /// Eighth Test Case: If update existed item successfully, returns true
    /// </summary>
    [Test]
    public void UpdatePark_Successfully_ReturnsTrue()
    {
      // Arrange
      var parkToUpdated = new Park
      {
        Id = 1,
        Name = "Name 1 updated ...",
        State = "State 1 updated ..."
      };
      // Act
      var result = _parkRepository.UpdatePark(parkToUpdated);
      // Assert
      Assert.IsTrue(result);
    }

    /// <summary>
    /// 9th Test Case: If update not existed item successfully, returns false
    /// </summary>
    [Test]
    public void UpdatePark_Failed_ReturnsFalse()
    {
      // Arrange
      var parkToUpdated = new Park
      {
        Id = 10,
        Name = "Name 1 updated ...",
        State = "State 1 updated ..."
      };
      // Act
      var result = _parkRepository.UpdatePark(parkToUpdated);
      // Assert
      Assert.IsFalse(result);
    }
    [TestCase(1, true)]
    [TestCase(10, false)]
    public void Deletepark_ReturnsCorrectValue(int id, bool expectedResult)

    {
      // Arrange

      // Act
      var actualResult = _parkRepository.DeletePark(id);
      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }


  }
}
