using Pyrite.Content.ContentProvider.FileStorage.Interfaces.Services;
using System.IO;

namespace Pyrite.Content.ContentProvider.FileStorage.Services
{
    public class DirectoryService : IDirectoryService
    {
        public void Create(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}