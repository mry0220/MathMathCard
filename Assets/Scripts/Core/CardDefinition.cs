using System;
using System.Collections.Generic;
using System.Linq;

namespace MathMathCard.Core
{
    public sealed class CardDefinition
    {
        private readonly int[] _valueWeights;

        private readonly WeightedTable _valueTable;
        private readonly ValueRange _range;

        public CardDefinition(
            int id,
            CardTarget target,
            MoveKind moveKind,
            int initialDealCount,
            int roundDealWeight,
            IReadOnlyList<int> valueWeights,
            ValueRange range
            )
        {
            if (valueWeights == null)
                throw new ArgumentNullException(nameof(valueWeights),$"valueWeightsがnullです。 ID{id}");
            if (valueWeights.Count != range.Count)
                throw new ArgumentException($"ValueWeightsとRangeの長さが一致しません　Id{id} 期待する値{range.Count} 実際の長さ{valueWeights.Count}",nameof(valueWeights));
            if (initialDealCount < 0)
                throw new ArgumentException($"initialDealCountが負の値です　Id{id} InitialDealCount{initialDealCount}", nameof(initialDealCount));
            if(roundDealWeight < 0)
                throw new ArgumentException($"roundDealWeightが負の値です Id{id} roundDealWeight{roundDealWeight}",nameof(roundDealWeight));
            
            _valueTable =  new WeightedTable(valueWeights);
            _range = range;

            Id = id;
            Target = target;
            MoveKind = moveKind;
            InitialDealCount = initialDealCount;
            RoundDealWeight = roundDealWeight;
            _valueWeights = valueWeights.ToArray();
        }

        public int RollValue(IRandomSource random)
        {
            int index = _valueTable.Pick(random);
            return _range.Min + index;
        }

        public int Id { get; }
        public CardTarget Target { get; }
        public MoveKind MoveKind { get; }
        public int InitialDealCount { get; }
        public int RoundDealWeight { get; }
        public IReadOnlyList<int> ValueWeights => _valueWeights;
    }
}
