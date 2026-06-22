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
            using var firstFileStream = new FileStream(firstFilePath, FileMode.Open, FileAccess.Read);
            using var secondFileStream = new FileStream(secondFilePath, FileMode.Open, FileAccess.Read);
            if (firstFileStream.Length != secondFileStream.Length)
                return false;
            int byte1, byte2;
            do
            {
                byte1 = firstFileStream.ReadByte();
                byte2 = secondFileStream.ReadByte();
                if (byte1 != byte2)
                    return false;
            } while (byte1 != -1);
            return true;
        }
    }
}
