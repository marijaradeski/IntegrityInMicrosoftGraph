using IntegrityInMicrosoftGraph.Enums;
using IntegrityInMicrosoftGraph.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Services
{
    public class FileService : IFileService
    {
        public void CreateFile(string path, int sizeKb, FileType fileType)
        {
            var data = new byte[sizeKb * 1024];

            new Random().NextBytes(data);

            byte[] header = GetHeader(fileType);

            Buffer.BlockCopy(header, 0, data, 0, Math.Min(header.Length, data.Length));

            File.WriteAllBytes(path, data);
        }

        private byte[] GetHeader(FileType fileType)
        {
            return fileType switch
            {
                FileType.Jpg => new byte[] { 0xFF, 0xD8, 0xFF },
                FileType.Png => new byte[] { 0x89, 0x50, 0x4E, 0x47 },
                FileType.Txt => Encoding.UTF8.GetBytes("TEXT FILE\n"),
                FileType.Zip => new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                _ => Array.Empty<byte>()
            };
        }
    }
}
