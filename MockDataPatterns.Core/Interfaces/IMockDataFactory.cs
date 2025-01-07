using System.Collections.Generic;

namespace MockDataPatterns.Core.Interfaces
{
    /// <summary>
    /// Mock veri fabrikası için temel interface
    /// </summary>
    public interface IMockDataFactory<T>
    {
        T Create();
        IEnumerable<T> CreateMany(int count);
        void SetStrategy(IMockDataStrategy<T> strategy);
    }
}
