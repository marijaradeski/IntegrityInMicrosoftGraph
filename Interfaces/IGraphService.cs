using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Interfaces
{
    public interface IGraphService
    {
        Task UploadAsync(string local, string remote);
        Task DownloadAsync(string remote, string local);
    }
}
