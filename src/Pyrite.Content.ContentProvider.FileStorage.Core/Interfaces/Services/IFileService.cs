using System.Threading.Tasks;

namespace Pyrite.Content.ContentProvider.FileStorage.Core.Interfaces.Services
{
    public interface IFileService
    {
        Task CreateAsync(string path, byte[] content);
    }
}