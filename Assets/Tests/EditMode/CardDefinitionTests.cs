using System;
using NUnit.Framework;

namespace MathMathCard.Core.Tests
{
    public class CardDefinitionTests
    {
        [Test]
        public void テスト4001_正常な定義_生成できる()
        {
            var definition = new CardDefinition(
                1,
                CardTarget.Self,
                MoveKind.Walk,
                2,
                10,
                new[] { 30, 30, 20, 10, 5, 5 },
                new ValueRange(1, 6)
                );

            Assert.AreEqual(1, definition.Id);
            Assert.AreEqual(6, definition.ValueWeights.Count);
        }

        [Test]
        public void テスト4002_重みの範囲が違う_例外処理()
        {

            Assert.Throws<ArgumentException>(() => new CardDefinition(
                1,
                CardTarget.Self,
                MoveKind.Walk,
                2,
                10,
                new[] { 30, 30, 20, 10, 5 },
                new ValueRange(1, 6)
                ));
        }

        [Test]
        public void テスト4003_初期配布枚数が負_例外処理()
        {

            Assert.Throws<ArgumentException>(() => new CardDefinition(
                1,
                CardTarget.Self,
                MoveKind.Walk,
                -2,
                10,
                new[] { 30, 30, 20, 10, 5, 5 },
                new ValueRange(1, 6)
                ));
        }

        [Test]
        public void テスト4004_重みが全部0_例外処理()
        {

            Assert.Throws<ArgumentException>(() => new CardDefinition(
                1,
                CardTarget.Self,
                MoveKind.Walk,
                2,
                10,
                new[] { 0, 0, 0, 0, 0, 0 },
                new ValueRange(1, 6)
                ));
        }

        [Test]
        public void テスト4005_期待したカードが選ばれる_カード効果の範囲1から6_期待する添え字1()
        {
            var definition = new CardDefinition(
                1,
                CardTarget.Self,
                MoveKind.Walk,
                2,
                10,
                new[] { 1, 1, 1, 1, 1, 1 },
                new ValueRange(1, 6)
                );

            Assert.AreEqual(1,definition.RollValue(new FixedRandomSource(0)));
        }

        [Test]
        public void テスト4006_期待したカードが選ばれる_カード効果の範囲1から6_期待する添え字6()
        {
            var definition = new CardDefinition(
                1,
                CardTarget.Self,
                MoveKind.Walk,
                2,
                10,
                new[] { 1, 1, 1, 1, 1, 1 },
                new ValueRange(1, 6)
                );

            Assert.AreEqual(6, definition.RollValue(new FixedRandomSource(5)));
        }

        [Test]
        public void テスト4007_期待したカードが選ばれる_カード効果の範囲3から5_期待する添え字4()
        {
            var definition = new CardDefinition(
                1,
                CardTarget.Self,
                MoveKind.Walk,
                2,
                10,
                new[] { 1, 1, 1},
                new ValueRange(3, 5)
                );

            Assert.AreEqual(4, definition.RollValue(new FixedRandomSource(1)));
        }

       
    }
}
