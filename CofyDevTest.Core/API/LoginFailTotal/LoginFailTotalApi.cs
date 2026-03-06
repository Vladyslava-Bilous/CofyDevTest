using CofyDevTest.Core.API.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CofyDevTest.Core.API.LoginFailTotal
{
    public class LoginFailTotalApi(AuthHandler authHandler)
    {
        private static readonly TestConfiguration TestConfiguration =
            TestBootstrap.Instance.ServiceProvider.GetRequiredService<TestConfiguration>();

        private readonly HttpClient _httpClient = new(authHandler)
        {
            BaseAddress = new Uri(TestConfiguration.APIURL)
        };

        public async Task<HttpResponseMessage> GetLoginFailTotal(string? userEmail = null, int? failCount = null, int? fetchLimit = null)
        {
            var uri = new QueryString("/loginfailtotal")
                .Add("userEmail", userEmail)
                .Add("failCount", failCount.ToString())
                .Add("fetchLimit", fetchLimit.ToString())
                .ToUriComponent();
            return await _httpClient.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> ResetLoginFailTotal(string userName)
        {
            var response = await _httpClient.PutAsync("/resetloginfailtotal", new StringContent(userName));
            return response;
        }
    }
}
