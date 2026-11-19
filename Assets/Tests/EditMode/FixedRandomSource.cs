
namespace MathMathCard.Core.Tests
{
    public sealed class FixedRandomSource : IRandomSource
    {
        private readonly int _value;

        public FixedRandomSource(int value)
        {
            _value = value;
        }

        public int Next(int maxExclusive)
        {
            return _value;
        }
    }
}
