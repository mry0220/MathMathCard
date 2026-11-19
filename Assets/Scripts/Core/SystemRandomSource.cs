
using System;

namespace MathMathCard.Core
{
    public sealed class SystemRandomSource : IRandomSource
    {
        private readonly Random _random;

        public SystemRandomSource(int seed)
        {
            _random = new Random(seed);
        }

        public int Next(int maxExclusive)
        {
            if(maxExclusive <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxExclusive));
            }

           return _random.Next(0,maxExclusive);
        }
    }
}
