using System.Text;

namespace OxygenMeasurementApi.Utilities;

public static class FileUtilities
{
    public static bool FileExists(this string file)
    {
        bool exists = false;

        if (string.IsNullOrEmpty(file) && string.IsNullOrWhiteSpace(file)) return false;

        if (File.Exists(file))
        {
            exists = true;
        }

        return exists;
    }

    public static void CreateFileIfNotExists(this string pathFileName, Encoding encoding)
    {
        if (pathFileName.FileExists()) return;
        File.WriteAllText(pathFileName, string.Empty, encoding);
    }
}