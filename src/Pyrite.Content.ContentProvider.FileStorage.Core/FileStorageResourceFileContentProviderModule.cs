using Microsoft.Extensions.DependencyInjection;
using Pyrite.Content.Core.Abstractions.Interfaces;
using Pyrite.Content.Core.Abstractions.Interfaces.Services;
using Pyrite.Content.ContentProvider.FileStorage.Core.Interfaces.Services;
using Pyrite.Content.ContentProvider.FileStorage.Core.Services;

namespace Pyrite.Content.ContentProvider.FileStorage.Core
{
    public class FileStorageResourceFileContentProviderModule : IPyriteResourceContentProviderModule
    {
        private readonly string _baseDirectory;

        public FileStorageResourceFileContentProviderModule(string baseDirectory)
        {
            this._baseDirectory = baseDirectory;
        }

        public void Register(IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDirectoryService, DirectoryService>();
            services.AddTransient<IResourceContentProviderService>
            (
                provider =>
                new FileStorageResourceFileContentProviderService
                (
                    this._baseDirectory,
                    provider.GetService<IFileService>(),
                    provider.GetService<IDirectoryService>()
                )
            );
        }
    }
}