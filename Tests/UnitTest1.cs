using GelberAssignment.Classes;

namespace Tests;
[TestClass]
public class RailwayTests
{
    [TestMethod]
    public void TestRemoveTrains()
    {
        // Arrange
        Railway railway = new Railway(5, 10, 15, 20); // Example values for parameters

        railway.Trains = new List<Train>
            {
                new Train(1, 1, 5), // Train going left, position 1 (remove)
                new Train(2, 1, 5), // Train going left, position 2
                new Train(3, 5, 1), // Train going right, position 5 (remove)
                new Train(4, 5, 1), // Train going right, position 4
                new Train(5, 5, 1), // Train going right, position 1 
                new Train(6, 5, 1), // Train going right, position 2
            };

        // Act
        railway.removeTrains();


        // Assert
        Assert.AreEqual(4, railway.Trains.Count); // Expecting one train to be removed
        Assert.IsFalse(railway.Trains.Exists(t => t.Direction == "Right" && t.CurrentPosition == railway.NumberOfStations)); // No train going right at position 1 should exist

    }
}
