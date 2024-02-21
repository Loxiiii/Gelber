using GelberAssignment.Classes;
using GelberAssignment;
namespace Tests;
[TestClass]
public class ProgramTests
{
	[TestMethod]
	public void TestParseFile()
	{
		// Arrange
		string content = "";

		// Act
		string[][] result = Program.ParseFile("");

        // Assert
        Assert.AreEqual(0, result.Length);
	}
}

