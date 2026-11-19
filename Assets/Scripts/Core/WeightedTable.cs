using System;
using System.Collections.Generic;
using System.Linq;

namespace MathMathCard.Core
{
    public sealed class WeightedTable
    {
        private readonly int[] _weights;
        private readonly int _total;

        public WeightedTable(IReadOnlyList<int> weights)
        {
            if(weights == null)
                throw new ArgumentNullException(nameof(weights),"WeightsがNullです！！！！");
            if (weights.Count == 0)
                throw new ArgumentException("Weightsの要素が0です",nameof(weights));
            int total = 0;
            foreach(var weight in weights)
            {
                if (weight < 0)
                    throw new ArgumentException("重みがマイナスの要素があります", nameof(weights));
                total += weight;
            }

            _total = total;
            if(_total <= 0)
            {
                throw new ArgumentException("重みの合計が0以下です", nameof(total));
            }

            _weights = weights.ToArray();
        }

        public int Pick(IRandomSource random)
        {
            if(random == null)
                throw new ArgumentNullException(nameof(random),"RandomSourceがNullです!!!!");

            int r = random.Next(_total);


            for (int i = 0; i < _weights.Length; i++) 
            {
                if ((r -= _weights[i]) < 0)
                {
                    return i;
                }
            }

            throw new InvalidOperationException($"randomValue : {r} total : {_total}");
           
        }
    }
}
