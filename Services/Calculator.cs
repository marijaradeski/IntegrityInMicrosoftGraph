using IntegrityInMicrosoftGraph.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Services
{
    public class Calculator : ICalculator
    {
        public double CalculateBytesPerSecond(long bytes, long milliseconds)
        {
            if (milliseconds <= 0)
                return 0;

            return bytes / (milliseconds / 1000.0);
        }

        public double CalculateMbPerSecond(long bytes, long milliseconds)
        {
            if (milliseconds <= 0)
                return 0;

            double bytesPerSecond = CalculateBytesPerSecond(bytes, milliseconds);
            return bytesPerSecond / (1024.0 * 1024.0);
        }
    }
}
