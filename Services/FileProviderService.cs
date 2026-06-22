using IntegrityInMicrosoftGraph.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Services
{
    public class FileProviderService : IFileSourceService
    {
        public Task<string> FilePath()
        {
            throw new NotImplementedException();
        }

        public string GetFileType(string path)
        {
            throw new NotImplementedException();
        }

        public long GetSizeBytes(string path)
        {
            throw new NotImplementedException();
        }
    }
}
