using System;
using System.IO;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // Use the Environment class to get the user's Desktop path (language-independent)
        string desktopPath = string.Empty;
        try
        {
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to get the desktop path: " + ex.Message);
            return;
        }

        // Define the new folder path (GibbleGobble on the Desktop)
        string folderPath = string.Empty;
        try
        {
            folderPath = Path.Combine(desktopPath, "GibbleGobble");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to define the folder path: " + ex.Message);
            return;
        }

        // Define the file path inside the GibbleGobble folder
        string filePath = string.Empty;
        try
        {
            filePath = Path.Combine(folderPath, "output.txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to define the file path: " + ex.Message);
            return;
        }

        // Check if the path length exceeds the limit
        if (filePath.Length >= 260)
        {
            Console.WriteLine("Error: The file path exceeds the maximum length of 260 characters.");
            return;
        }

        // Text to be written into the file
        string textToWrite = "Get Gibble Gobbled Lmao.";

        try
        {
            // Check if the folder exists, if not, create it
            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                    Console.WriteLine("Folder 'GibbleGobble' successfully created on desktop!");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine("Failed to create the folder due to insufficient permissions: " + ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to create the folder: " + ex.Message);
                    return;
                }
            }

            // Create and write to the file
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(textToWrite);
                    Console.WriteLine("Text written to file successfully!");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Failed to write to the file due to insufficient permissions: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to write to the file: " + ex.Message);
                return;
            }

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: File not found.");
                return;
            }

            // Open the file in the default text editor
            try
            {
                Console.WriteLine("Attempting to open the file...");
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                Console.WriteLine("File opened successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to open the file: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        // Wait for user input to keep the console open
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();  // This will pause the program and keep the window open until a key is pressed
    }
}