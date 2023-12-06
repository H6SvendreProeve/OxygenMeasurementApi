namespace OxygenMeasurementApi.Utilities;

/// <summary>
/// 
/// </summary>
public static class DirectoryUtilities
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public static bool DirectoryExists(this string directory)
    {
        bool exists = false;

        if (!string.IsNullOrEmpty(directory) || !string.IsNullOrWhiteSpace(directory))
        {
            exists = Directory.Exists(directory);
        }

        return exists;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    public static void CreateDirectoryIfNotExists(this string directory)
    {
        if (!DirectoryExists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}