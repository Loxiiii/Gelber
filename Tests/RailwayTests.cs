using Microsoft.VisualStudio.TestTools.UnitTesting;
using GelberAssignment.Classes;
using System.Collections.Generic;
namespace Tests;

[TestClass]
public class RailwayTests
{
    [TestMethod]
    public void TestRemoveTrains_RemovesTrainsAtEndAndBeginning()
    {
        // Arrange
        Railway railway = new Railway(5, 10, 2, 50); // Example values
        List<Train> trains = new List<Train>
        {
            new Train(1, 0, 5), // goes right and deletes at 5
            new Train(5, 0, 5), // goes left and deletes at 
            new Train(3, 0, 5),
            new Train(2, 0, 5),
            new Train(4, 0, 5),
        };
        railway.Trains = trains;

        // Act
        railway.RemoveTrains();

        // Assert
        // Check if the trains at the beginning and end are removed
        Assert.IsFalse(railway.Trains.Exists(t => t.CurrentPosition == 1 && t.Direction == "Left")
                       || railway.Trains.Exists(t => t.CurrentPosition == railway.NumberOfStations && t.Direction == "Right"));

        // Check if the trains in the middle are not removed
        Assert.IsTrue(railway.Trains.Exists(t => t.CurrentPosition != 1 && t.CurrentPosition != railway.NumberOfStations));
    }

    [TestMethod]
    public void TestRemoveTrains_NoTrainsToRemove()
    {
        // Arrange
        Railway railway = new Railway(5, 10, 2, 50); // Example values
        List<Train> trains = new List<Train>
        {
            new Train(2, 0, 5),
            new Train(3, 0, 5),
            new Train(4, 0, 5),
        };
        railway.Trains = trains;

        // Act
        railway.RemoveTrains();

        // Assert
        // Check if there are no trains removed
        Assert.AreEqual(3, railway.Trains.Count);
    }
}
