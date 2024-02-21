using System;
using System.IO;

namespace Helpers
{
    public class Reader
    {
        public string getContent(string fileName)
        {
            // Get the directory where the executable is located
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Navigate to the project root directory by going up three directories
            string projectRootDirectory = Path.Combine(executableDirectory, "../../../../");

            // Construct the file path relative to the project root directory
            string filePath = Path.Combine(projectRootDirectory, "GelberAssignment", "Examples", fileName + ".txt");

            try
            {
                // Read the file
                string content = File.ReadAllText(filePath);
                Console.WriteLine("The contents of the file are:");
                Console.WriteLine(content);
                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return string.Empty;
            }
        }
    }
}
