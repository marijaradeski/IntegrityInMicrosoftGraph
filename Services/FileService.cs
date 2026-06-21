using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Services
{
    public class FileService
    {
        public string CreateFile(string fileName, int sizeBytes)
        {
            var basePath = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(basePath, fileName);

            var data = new byte[sizeBytes];
            new Random().NextBytes(data);

            File.WriteAllBytes(filePath, data);

            return filePath;
        }
    }
}
