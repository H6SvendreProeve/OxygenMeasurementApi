using System.Security.Cryptography;

namespace OxygenMeasurementApi.Authorization;

/// <summary>
/// Class ApiKeyGenerator provides a utility for generating new API keys.
/// </summary>
public static class ApiKeyGenerator
{
    /// <summary>
    /// Method GenerateNewApiKey Generates a new API key with the specified length.
    /// </summary>
    /// <param name="keyLength"> the length of the generated API key</param>
    /// <returns>A string representing the newly generated API key.</returns>
    public static string GenerateNewApiKey(int keyLength)
    {
        // Define the characters allowed in the API key.
        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        // Create an array to hold random bytes.
        var randomBytes = new byte[keyLength];

        // Use a secure random number generator to fill the array with random bytes.
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        var chars = new char[keyLength];


        var allowedCharCount = allowedChars.Length;

        // Generate the API key by selecting characters from the allowed set based on random bytes.
        for (var i = 0; i < keyLength; i++)
        {
            chars[i] = allowedChars[randomBytes[i] % allowedCharCount];
        }

        return new string(chars);
    }
}