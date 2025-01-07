using System;
using System.Collections.Generic;
using MockDataPatterns.Core.Interfaces;

namespace MockDataPatterns.Infrastructure.Factories
{
    /// <summary>
    /// Genel mock veri fabrikası implementasyonu
    /// </summary>
    public class MockDataFactory<T> : IMockDataFactory<T>
    {
        private IMockDataStrategy<T> _strategy;

        public MockDataFactory(IMockDataStrategy<T> defaultStrategy)
        {
            _strategy = defaultStrategy ?? throw new ArgumentNullException(nameof(defaultStrategy));
        }

        public T Create()
        {
            return _strategy.GenerateSingle();
        }

        public IEnumerable<T> CreateMany(int count)
        {
            return _strategy.GenerateMultiple(count);
        }

        public void SetStrategy(IMockDataStrategy<T> strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
    }
}
