using Pyrite.Content.ContentProvider.FileStorage.Core.Interfaces.Services;
using System.IO;

namespace Pyrite.Content.ContentProvider.FileStorage.Core.Services
{
    public class DirectoryService : IDirectoryService
    {
        public void Create(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}