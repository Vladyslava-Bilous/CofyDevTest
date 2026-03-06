using System.Text.Json;
using CofyDevTest.Core.API.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace CofyDevTest.Core.API.User
{
    public class UserApi(AuthHandler authHandler)
    {
        private static readonly TestConfiguration TestConfiguration =
            TestBootstrap.Instance.ServiceProvider.GetRequiredService<TestConfiguration>();

        private readonly HttpClient _httpClient = new(authHandler)
        {
            BaseAddress = new Uri(TestConfiguration.APIURL)
        };

        public async Task<UserResponseModel?> GetUser(string userEmail)
        {
            var response = await _httpClient.GetAsync($"/user?email={userEmail}");
            var content = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<UserResponseModel>(content);
        }
    }
}
