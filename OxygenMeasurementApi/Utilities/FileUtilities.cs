using System.Text;

namespace OxygenMeasurementApi.Utilities;

/// <summary>
/// 
/// </summary>
public static class FileUtilities
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pathFileName"></param>
    /// <param name="encoding"></param>
    public static void CreateFileIfNotExists(this string pathFileName, Encoding encoding)
    {
        if (pathFileName.FileExists()) return;
        File.WriteAllText(pathFileName, string.Empty, encoding);
    }
}