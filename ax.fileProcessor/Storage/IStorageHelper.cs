using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace ax.fileProcessor.Storage
{
    public interface IStorageHelper<T>
    {
        Task<Result> SendContent(T content, AuthCredential auth);
    }
}
