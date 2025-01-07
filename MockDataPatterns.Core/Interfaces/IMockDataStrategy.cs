using System.Collections.Generic;

namespace MockDataPatterns.Core.Interfaces
{
    /// <summary>
    /// Mock veri üretim stratejisi için temel interface
    /// </summary>
    public interface IMockDataStrategy<T>
    {
        T GenerateSingle();
        IEnumerable<T> GenerateMultiple(int count);
    }
}
