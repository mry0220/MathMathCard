using System;
using NUnit.Framework;

namespace MathMathCard.Core.Tests
{
    public class CardCatalogTests
    {
        [Test]
        public void テスト5001_2種類のカード_期待値2番目が引ける()
        {
            var definitions = new[]
            {
                new CardDefinition(
                1,
                CardTarget.Other,
                MoveKind.Walk,
                2,
                10,
                new[] { 1, 1, 1 },
                new ValueRange(3, 5)
                ),
                new CardDefinition(
                2,
                CardTarget.Self,
                MoveKind.Jump,
                2,
                10,
                new[] { 1, 1, 1 },
                new ValueRange(3, 5)
                ),
            };

            CardCatalog catalog = new CardCatalog( definitions );
            CardDefinition CardHeld = catalog.Get(2);
            Assert.AreEqual(2, CardHeld.Id);
            Assert.AreEqual(CardTarget.Self, CardHeld.Target);
            Assert.AreEqual(MoveKind.Jump, CardHeld.MoveKind);
        }

        [Test]
        public void テスト5002_IDが重複_例外処理()
        {
            var definitions = new[]
            {
                new CardDefinition(
                1,
                CardTarget.Other,
                MoveKind.Walk,
                2,
                10,
                new[] { 1, 1, 1 },
                new ValueRange(3, 5)
                ),
                new CardDefinition(
                1,
                CardTarget.Self,
                MoveKind.Jump,
                2,
                10,
                new[] { 1, 1, 1 },
                new ValueRange(3, 5)
                ),
            };

            Assert.Throws<ArgumentException>(() => new CardCatalog(definitions));
        }

        [Test]
        public void テスト5003_DefinitionsがNULL_例外処理()
        {
            Assert.Throws<ArgumentNullException>(() => new CardCatalog(null));
        }

        [Test]
        public void テスト5004_Definitionsが空_例外処理()
        {
            Assert.Throws<ArgumentException>(() => new CardCatalog(new CardDefinition[0]));
        }

        [Test]
        public void テスト5005_順ごとの重みが全部0_例外処理()
        {
            var definitions = new[]
            {
                new CardDefinition(
                1,
                CardTarget.Other,
                MoveKind.Walk,
                2,
                0,
                new[] { 1, 1, 1 },
                new ValueRange(3, 5)
                ),
                new CardDefinition(
                2,
                CardTarget.Self,
                MoveKind.Jump,
                2,
                0,
                new[] { 1, 1, 1 },
                new ValueRange(3, 5)
                ),
            };

            Assert.Throws<ArgumentException>(() => new CardCatalog(definitions));
        }

        [Test]
        public void テスト5006_存在しない_例外処理()
        {
            var definitions = new[]
            {
                new CardDefinition(
                1,
                CardTarget.Other,
                MoveKind.Walk,
                2,
                10,
                new[] { 1, 1, 1 },
                new ValueRange(3, 5)
                ),
                new CardDefinition(
                2,
                CardTarget.Self,
                MoveKind.Jump,
                2,
                10,
                new[] { 1, 1, 1 },
                new ValueRange(3, 5)
                ),
            };

            CardCatalog catalog = new CardCatalog(definitions);

            Assert.Throws<ArgumentException>(() => catalog.Get(99));
        }
    }
}
