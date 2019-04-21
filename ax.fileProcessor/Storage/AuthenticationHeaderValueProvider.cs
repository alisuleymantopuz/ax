using System;
using System.Net.Http.Headers;
using System.Text;

namespace ax.fileProcessor.Storage
{
    public class AuthenticationHeaderValueProvider : IAuthenticationHeaderValueProvider
    {
        public AuthenticationHeaderValue Get(AuthCredential credential)
        {
            if (credential == null)
                throw new Exception("Credential must not be null!");

            var encodedHeader = Encoding.ASCII.GetBytes($"{credential.Username}:{credential.Password}");

            var header = Convert.ToBase64String(encodedHeader);

            return new AuthenticationHeaderValue("Authorization", header);
        }
    }
}
