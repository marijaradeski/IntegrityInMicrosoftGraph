using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Interfaces
{
    public interface ICalculator
    {
        double CalculateBytesPerSecond(long bytes, long milliseconds);
        double CalculateMbPerSecond(long bytes, long milliseconds);
    }
}
