using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Model
{
    public class FileTransferResult
    {
        // inputs 
        public int SizeKb { get; set; }
        public string FileType { get; set; } = string.Empty;

        // file size info
        public long FileSizeBytes { get; set; }

        // performance 
        public long UploadTime { get; set; }
        public long DownloadTime { get; set; }

        public double UploadBytesPerSecond { get; set; }
        public double DownloadBytesPerSecond { get; set; }

        // integrity
        public bool HashMatch { get; set; }
        public bool FilesMatch { get; set; }

        // metrics
        public string RemotePath { get; set; } = string.Empty;
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
    }
}
