using System;
using System.IO;

namespace Helpers
{
    public class Reader
    {
        public string getContent(string fileName)
        {
            // Get the current working directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Navigate to the parent directory
            string parentDirectory = Directory.GetParent(currentDirectory).FullName;
            string dir = Directory.GetParent(parentDirectory).FullName;
            string d = Directory.GetParent(dir).FullName;

            // Construct the file path relative to the parent directory
            string filePath = Path.Combine(d, "Examples", fileName + ".txt");

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