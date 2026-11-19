using NUnit.Framework;
using System;

namespace MathMathCard.Core.Tests
{
    public class CardInstanceTest
    {
        [Test]
        public void テスト4008_カードが正常に生成される期待値_1_1_4()
        {
            var definition = new CardDefinition(
                1,
                CardTarget.Self,
                MoveKind.Walk,
                2,
                10,
                new[] { 1, 1, 1 },
                new ValueRange(3, 5)
                );

            var instance = new CardInstance(1, definition, 4);
            Assert.AreEqual(1, instance.InstanceId);
            Assert.AreEqual(1, instance.Definition.Id);
            Assert.AreEqual(4, instance.Value);
        }

        [Test]
        public void テスト4009_DefinitionがNull_例外処理()
        {
            Assert.Throws<ArgumentNullException>(() => new CardInstance(1, null, 1));
        }
    }
}
