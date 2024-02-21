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
            Console.WriteLine("The current directory is {0}", currentDirectory);
            string navigateBack = "../../../";
        
            // Construct the file path relative to the parent directory
            string filePath = Path.Combine(currentDirectory, navigateBack, "Examples", fileName + ".txt");

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