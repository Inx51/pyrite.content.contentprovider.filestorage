using Pyrite.Content.Core.Abstractions.Interfaces;
using Pyrite.Content.Core.Abstractions.Interfaces.Services;
using Pyrite.Content.ContentProvider.FileStorage.Core.Interfaces.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pyrite.Content.ContentProvider.FileStorage.Core.Services
{
    public class FileStorageResourceFileContentProviderService : IResourceContentProviderService
    {
        private readonly IFileService _fileService;
        private readonly IDirectoryService _directoryService;
        private readonly string _baseDirectory;

        public FileStorageResourceFileContentProviderService
        (
            string baseDirectory,
            IFileService fileService,
            IDirectoryService directoryService
        )
        {
            var baseDirectoryInfo = new DirectoryInfo(baseDirectory);
            this._baseDirectory = baseDirectoryInfo.FullName;
            this._fileService = fileService;
            this._directoryService = directoryService;
        }

        public async Task CreateAsync(string identifier, Stream resourceStream)
        {
            var fileInfo = GetFileInfo(identifier);
            this._directoryService.Create(fileInfo.Directory.FullName);

            await SaveFileAsync(fileInfo.FullName, resourceStream);
        }

        public Task DeleteAsync(string identifier)
        {
            var fileInfo = GetFileInfo(identifier);
            fileInfo.Delete();
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(string identifier)
        {
            var fileInfo = GetFileInfo(identifier);
            return Task.FromResult(fileInfo.Exists);
        }

        public Task<Stream> GetAsync(string identifier)
        {
            var fileInfo = GetFileInfo(identifier);
            return Task.FromResult((Stream)fileInfo.OpenRead());
        }

        public Task<bool> IsValidProviderAsync(IResourceHeaders resourceHeaders, Stream resourceStream)
        {
            return Task.FromResult(true);
        }

        public async Task ReplaceAsync(string identifier, Stream resourceStream)
        {
            var fileInfo = GetFileInfo(identifier);
            this._directoryService.Create(fileInfo.Directory.FullName);

            await SaveFileAsync(fileInfo.FullName, resourceStream);
        }

        private FileInfo GetFileInfo(string identifier)
        {
            var uri = new Uri("file://" + identifier);
            var directoryPathSegments = uri.Segments[..(uri.Segments.Length - 1)];
            var directoryPath = string.Join("/", this._baseDirectory, directoryPathSegments);

            var fileName = $"{uri.Segments[(uri.Segments.Length - 1)..].FirstOrDefault()}.bin";
            return new FileInfo($"{directoryPath}/{fileName}");
        }

        private async Task SaveFileAsync(string fullFileName, Stream resourceStream)
        {
            if (resourceStream is MemoryStream ms)
                await this._fileService.CreateAsync(fullFileName, ms.ToArray());
            else
            {
                using (var memStream = new MemoryStream())
                {
                    await resourceStream.CopyToAsync(memStream);
                    await this._fileService.CreateAsync(fullFileName, memStream.ToArray());

                    resourceStream.Close();
                }
            }
        }
    }
}