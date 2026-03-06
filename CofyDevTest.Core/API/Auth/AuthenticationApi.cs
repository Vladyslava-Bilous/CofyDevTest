using Microsoft.Extensions.DependencyInjection;

namespace CofyDevTest.Core.API.Auth
{
    public class AuthenticationApi
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri(TestConfiguration.APIURL) };
        private static readonly TestConfiguration TestConfiguration =
            TestBootstrap.Instance.ServiceProvider.GetRequiredService<TestConfiguration>();

        public async Task<string> GetToken(UserModel user)
        {
            return await GetToken(user.UserEmail, user.UserPassword);
        }        
        
        public async Task<string> GetToken(string userEmail, string userPassword)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/auth")
            {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new("username", userEmail),
                    new("password", userPassword)
                })
            };

            var response = await _httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            var token = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return token;
        }
    }
}
