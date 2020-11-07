using System.Threading.Tasks;

namespace Pyrite.Content.ContentProvider.FileStorage.Interfaces.Services
{
    public interface IFileService
    {
        Task CreateAsync(string path, byte[] content);
    }
}