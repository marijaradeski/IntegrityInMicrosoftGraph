using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Interfaces
{
    public interface IFileSourceService
    {
        Task<string> FilePath();
        long GetSizeBytes(string path);
        string GetFileType(string path);
    }
}
