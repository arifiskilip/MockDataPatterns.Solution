using System;
using System.Collections.Generic;
using System.Linq;
using MockDataPatterns.Core.Interfaces;

namespace MockDataPatterns.Infrastructure.Strategies.Base
{
    /// <summary>
    /// Temel mock data stratejisi implementasyonu
    /// </summary>
    public abstract class BaseMockStrategy<T> : IMockDataStrategy<T>
    {
        protected readonly Random Random = new();

        public abstract T GenerateSingle();

        public virtual IEnumerable<T> GenerateMultiple(int count)
        {
            if (count < 0)
                throw new ArgumentException("Count cannot be negative", nameof(count));

            return Enumerable.Range(0, count).Select(_ => GenerateSingle());
        }
    }
}
