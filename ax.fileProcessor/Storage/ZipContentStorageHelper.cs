using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace ax.fileProcessor.Storage
{
    public class ZipContentStorageHelper : IZipContentStorageHelper
    {
        public AxSecureUrlConfiguration AxSecureUrlConfiguration { get; set; }
        public IAuthenticationHeaderValueProvider AuthenticationHeaderValueProvider { get; set; }
        public IHttpClientFactory HttpClientFactory { get; set; }

        public ZipContentStorageHelper(AxSecureUrlConfiguration axSecureUrlConfiguration, IAuthenticationHeaderValueProvider authenticationHeaderValueProvider, IHttpClientFactory httpClientFactory)
        {
            AxSecureUrlConfiguration = axSecureUrlConfiguration;
            AuthenticationHeaderValueProvider = authenticationHeaderValueProvider;
            HttpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Sends the content.
        /// </summary>
        /// <returns>The content.</returns>
        /// <param name="content">Content.</param>
        /// <param name="auth">Auth.</param>
        public async Task<Result> SendContent(string content, AuthCredential auth)
        {
            if (auth == null)
                throw new Exception("Authorization must not be null!");

            using (var client = HttpClientFactory.CreateClient())
            {
                client.BaseAddress = new Uri(AxSecureUrlConfiguration.AxSecureUrl);

                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValueProvider.Get(auth);

                using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    using (var response = await client.PostAsync(client.BaseAddress, stringContent))
                    {
                        if (response.IsSuccessStatusCode)
                            return Result.Ok();

                        return Result.Fail($"Message: {response.ReasonPhrase}");
                    }
                }
            }
        }
    }
}
