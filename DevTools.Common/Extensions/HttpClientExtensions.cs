using System.Net.Http;
using System.Net.Http.Headers;

namespace Hastnama.Ekipchi.Api.Core.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient SetToken(
            this HttpClient client,
            string scheme,
            string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);
            return client;
        }
    }
}