using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Interfaces
{
    public interface IFileComparer
    {
        bool Compare(string firstFilePath, string secondFilePath);

    }
}
