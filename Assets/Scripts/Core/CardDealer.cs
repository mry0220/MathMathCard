using System;
using System.Collections.Generic;

namespace MathMathCard.Core
{
    public sealed class CardDealer
    {
        private readonly CardCatalog _catalog;
        private readonly IRandomSource _random;
        private readonly WeightedTable _roundTable;
        private int _nextInstanceId = 0;

        public CardDealer(CardCatalog catalog,IRandomSource random)
        {
            if(catalog == null)
            {
                throw new ArgumentNullException(nameof(catalog), "CatalogがNullです");
            }
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random), "randomSouceがNullです");
            }

            _catalog = catalog;
            _random = random;

            var weights = new int[catalog.Definitions.Count];
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = catalog.Definitions[i].RoundDealWeight;
            }
            _roundTable = new WeightedTable(weights);
        }

        public IReadOnlyList<CardInstance> DealInitialHand()
        {
            
            var hand = new List<CardInstance>();
            foreach (var definition in _catalog.Definitions)
            {
                for (int i = 0; i < definition.InitialDealCount; i++)
                {
                    int value = definition.RollValue(_random);
                    hand.Add( new CardInstance(_nextInstanceId++, definition, value));
                }
            }

            return hand;
        }

        public CardInstance DealRoundCard()
        {
            int index = _roundTable.Pick(_random);
            CardDefinition def = _catalog.Definitions[index];
            return new CardInstance(_nextInstanceId++, def, def.RollValue(_random));
        }
    }
}
