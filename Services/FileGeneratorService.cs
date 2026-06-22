using IntegrityInMicrosoftGraph.Enums;
using IntegrityInMicrosoftGraph.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Services
{
    public class FileGeneratorService : IFileSourceService
    {
        private readonly int _sizeKb;
        private readonly FileType _fileType;
        private readonly IFileService _files;

        private readonly string _path = "original.bin";

        public FileGeneratorService(int sizeKb, FileType fileType, IFileService files)
        {
            _sizeKb = sizeKb;
            _fileType = fileType;
            _files = files;
        }

        public string  FilePath()
        {
            _files.CreateFile(_path, _sizeKb, _fileType);
            return _path;
        }

        public long GetSizeBytes(string path)
            => new FileInfo(path).Length;

        public string GetFileType(string path)
            => _fileType.ToString();
    }
}
