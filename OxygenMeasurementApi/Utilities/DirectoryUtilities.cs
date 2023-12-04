namespace OxygenMeasurementApi.Utilities;

public static class DirectoryUtilities
{
    public static bool DirectoryExists(this string directory)
    {
        bool exists = false;

        if (!string.IsNullOrEmpty(directory) || !string.IsNullOrWhiteSpace(directory))
        {
            exists = Directory.Exists(directory);
        }

        return exists;
    }

    public static void CreateDirectoryIfNotExists(this string directory)
    {
        if (!DirectoryExists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}