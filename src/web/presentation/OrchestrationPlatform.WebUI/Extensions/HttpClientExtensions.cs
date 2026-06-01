using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrchestrationPlatform.WebUI.Extensions;

public static class HttpClientExtensions
{
    private static readonly JsonSerializerOptions GlobalJsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static Task<T?> GetJsonAsync<T>(this HttpClient client, string url)
    {
        return client.GetFromJsonAsync<T>(url, GlobalJsonOptions);
    }

    public static Task<HttpResponseMessage> PostJsonAsync<TValue>(this HttpClient client, string url, TValue value)
    {
        return client.PostAsJsonAsync(url, value, GlobalJsonOptions);
    }

    public static Task<HttpResponseMessage> PutJsonAsync<TValue>(this HttpClient client, string url, TValue value)
    {
        return client.PutAsJsonAsync(url, value, GlobalJsonOptions);
    }
}