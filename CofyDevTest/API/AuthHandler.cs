using System.Net.Http.Headers;

namespace CofyDevTest.API
{
    public class AuthHandler : DelegatingHandler
    {
        public string Token { get; }

        internal AuthHandler(string token)
        {
            Token = token;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Add renew token when it's expired, if needed.
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
