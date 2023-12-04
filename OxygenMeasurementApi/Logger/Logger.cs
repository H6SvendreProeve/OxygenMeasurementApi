using System.Text;
using OxygenMeasurementApi.Utilities;

namespace OxygenMeasurementApi.Logger;

public class Logger
{
    private static readonly object LockObj = new();
    private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
    
    public static async Task LogAsync(string message)
    {
        LogDirectory.CreateDirectoryIfNotExists();
        
        string fileName = Path.Combine(LogDirectory, $"log_{DateTime.Now:yyyy_MM_dd}.txt");

        // Use a lock to ensure thread safety when creating the file
        lock (LockObj)
        {
            fileName.CreateFileIfNotExists(Encoding.UTF8);
        }

        await using var writer = new StreamWriter(fileName, true);

        await writer.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd} - {message}");
    }
}