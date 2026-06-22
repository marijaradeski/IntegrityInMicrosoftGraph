using IntegrityInMicrosoftGraph.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Services
{
    public class FileProviderService : IFileSourceService
    {
        private readonly string _path;

        public FileProviderService(string path)
        {
            _path = path;
        }

        public string FilePath()
            => _path;

        public long GetSizeBytes(string path)
            => new FileInfo(path).Length;

        public string GetFileType(string path)
            => Path.GetExtension(path);
    }
}
