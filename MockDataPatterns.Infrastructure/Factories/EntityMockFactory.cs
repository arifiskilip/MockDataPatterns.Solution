using System;
using System.Collections.Generic;
using System.Linq;
using MockDataPatterns.Core.Interfaces;

namespace MockDataPatterns.Infrastructure.Factories
{
    public class EntityMockFactory<T> : IMockDataFactory<T>
    {
        private IMockDataStrategy<T> _strategy;

        public EntityMockFactory(IMockDataStrategy<T> defaultStrategy)
        {
            _strategy = defaultStrategy;
        }

        public T Create()
        {
            return _strategy.GenerateSingle();
        }

        public IEnumerable<T> CreateMany(int count)
        {
            return Enumerable.Range(0, count).Select(_ => Create());
        }

        public void SetStrategy(IMockDataStrategy<T> strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
    }
}
