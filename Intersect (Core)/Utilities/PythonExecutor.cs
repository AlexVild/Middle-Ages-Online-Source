using System;
using System.Diagnostics;

public static class PythonExecutor
{
    public static void ExecutePythonScript(string pythonScriptPath, bool headless = false)
    {
        try
        {
            // Set up the process start info
            var startInfo = new ProcessStartInfo
            {
                FileName = "python",  // or "python3" depending on your Python installation
                Arguments = $"\"{pythonScriptPath}\"",  // Ensure the file path is in quotes
                CreateNoWindow = headless,  // Optionally hide the command window
                UseShellExecute = false  // Use the shell execute option as false for direct process execution
            };

            // Start the process
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing Python script: {ex.Message}");
        }
    }
}
