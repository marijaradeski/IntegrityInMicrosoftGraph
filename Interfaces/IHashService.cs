using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Interfaces
{
    public interface IHashService
    {
        string ComputeHash(string filePath);
    }
}
