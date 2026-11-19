using System;
using System.Collections.Generic;

namespace MathMathCard.Core
{
    public sealed class CardCatalog
    {
        private readonly Dictionary<int,CardDefinition> _cardDefs = new();

        public CardCatalog(IReadOnlyList<CardDefinition> definitions)
        {
            if (definitions == null)
                throw new ArgumentNullException(nameof(definitions), "definitionsがNullです");
            if(definitions.Count == 0)
                throw new ArgumentException("definitionsの要素が0です。",nameof(definitions));
            int roundDealWeight = 0;
            
          

            foreach(var definition in definitions)
            {
                if(definition == null)
                {
                    throw new ArgumentNullException(nameof(definitions),"DefinitionsにNULLの要素があります");
                }

                if( _cardDefs.ContainsKey(definition.Id))
                {
                    throw new ArgumentException($"definitionsに重複したIDが登録されています。重複id {definition.Id}",nameof(definitions));
                }
                _cardDefs.Add(definition.Id,definition);
                roundDealWeight += definition.RoundDealWeight;
            }

            if (roundDealWeight <= 0)
                throw new ArgumentException("definitionのRoundDealWeightの合計が0以下です。", nameof(definitions));

            Definitions = new List<CardDefinition>(definitions);
        }

        public IReadOnlyList<CardDefinition> Definitions { get; }

        
        public CardDefinition Get(int id) 
        {
            if(!_cardDefs.TryGetValue(id,out var def))
                throw new ArgumentException($"指定IDのDefinitionは登録されていません 指定ID : {id}",nameof(id));
            return def;            
        }
    }
}
