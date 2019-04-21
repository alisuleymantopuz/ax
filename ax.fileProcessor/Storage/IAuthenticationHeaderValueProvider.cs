using System.Net.Http.Headers;

namespace ax.fileProcessor.Storage
{
    public interface IAuthenticationHeaderValueProvider
    {
        AuthenticationHeaderValue Get(AuthCredential credential);
    }
}
