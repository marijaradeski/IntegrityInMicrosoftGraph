using IntegrityInMicrosoftGraph.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace IntegrityInMicrosoftGraph.Security
{
    public class HashService : IHashService
    {
        public string ComputeHash(string filePath)
        {
            using var sha = SHA256.Create();
            using var stream = File.OpenRead(filePath);
            var hash = sha.ComputeHash(stream);
            return Convert.ToHexString(hash);
        }
    }
}
