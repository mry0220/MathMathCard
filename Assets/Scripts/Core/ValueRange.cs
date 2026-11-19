using System;

namespace MathMathCard.Core
{
    public sealed class ValueRange
    {
        public int Min { get; } 
        public int Max { get; }
        public int Count => Max - Min + 1;

        public ValueRange(int min = 1, int max = 6)
        {
            if(min > max)
            {
                throw new ArgumentException("最小値が最大値を超えています",nameof(min));

            }    
            Min = min;
            Max = max;
        }
    }
}
