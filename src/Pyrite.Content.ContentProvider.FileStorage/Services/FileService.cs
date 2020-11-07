using Pyrite.Content.ContentProvider.FileStorage.Interfaces.Services;
using System.IO;
using System.Threading.Tasks;

namespace Pyrite.Content.ContentProvider.FileStorage.Services
{
    public class FileService : IFileService
    {
        public async Task CreateAsync(string path, byte[] content)
        {
            using (var fileStream = File.Create(path))
            {
                await fileStream.WriteAsync(content, 0, content.Length);
            }
        }
    }
}