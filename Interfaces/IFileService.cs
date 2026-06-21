using IntegrityInMicrosoftGraph.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Interfaces
{
    public interface IFileService
    {
        void CreateFile(string fileName, int sizeBytes, FileType fileType);
    }
}
