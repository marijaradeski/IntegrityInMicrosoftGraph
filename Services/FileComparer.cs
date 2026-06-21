using IntegrityInMicrosoftGraph.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Services
{
    public class FileComparer : IFileComparer
    {
        public bool Compare(string firstFilePath, string secondFilePath)
        {
            using var fs1 = new FileStream(firstFilePath, FileMode.Open, FileAccess.Read);
            using var fs2 = new FileStream(secondFilePath, FileMode.Open, FileAccess.Read);
            if (fs1.Length != fs2.Length)
                return false;
            int byte1, byte2;
            do
            {
                byte1 = fs1.ReadByte();
                byte2 = fs2.ReadByte();
                if (byte1 != byte2)
                    return false;
            } while (byte1 != -1);
            return true;
        }
    }
}
