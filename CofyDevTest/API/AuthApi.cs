using CofyDevTest.Core.Helpers;
using CofyDevTest.Core.Helpers.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CofyDevTest.API
{
    public class AuthApi
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri(TestConfiguration.APIURL) };
        private static readonly TestConfiguration TestConfiguration =
            TestBootstrap.Instance.ServiceProvider.GetRequiredService<TestConfiguration>();

        public async Task<string> GetToken(UserModel user)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/auth")
            {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new("username", user.UserEmail),
                    new("password", user.UserPassword)
                })
            };

            var response = await _httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            var token = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return token;
        }
    }
}
