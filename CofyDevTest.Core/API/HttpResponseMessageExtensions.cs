using System.Text.Json;

namespace CofyDevTest.Core.API
{
    public  static class HttpResponseMessageExtensions
    {
        public static async Task<T?> DeserializeContentAsAsync<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(content);
        }
    }
}
