using System;

namespace MathMathCard.Core
{
    public sealed class CardInstance
    {
        public int InstanceId { get; }
        public CardDefinition Definition { get; }
        public int Value { get; }

        public CardInstance(int instanceId, CardDefinition definition, int value)
        {
            if(definition  == null)
                throw new ArgumentNullException(nameof(definition),$"definitionがNullです。instanceId {instanceId}");

            InstanceId = instanceId;
            Definition = definition;
            Value = value;
        }
    }
}
