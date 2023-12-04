namespace OxygenMeasurementApi.Tests.MockTestData;

public static class RequestHeaderProvider
{
    public static void SetRequestHeaders(this HttpClient httpClient, string apiKey,int systemId)
    {
        httpClient.DefaultRequestHeaders.Add("X-Api-Key",apiKey);
        httpClient.DefaultRequestHeaders.Add("X-System-Id", systemId.ToString());
    }
}