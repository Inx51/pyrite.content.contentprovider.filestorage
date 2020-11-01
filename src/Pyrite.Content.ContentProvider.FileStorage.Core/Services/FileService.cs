using Pyrite.Content.ContentProvider.FileStorage.Core.Interfaces.Services;
using System.IO;
using System.Threading.Tasks;

namespace Pyrite.Content.ContentProvider.FileStorage.Core.Services
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