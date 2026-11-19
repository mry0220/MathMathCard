using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MathMathCard.Core.Tests
{
    public class CardDealerTests
    {
        private static CardDefinition MakeDefinition(int id, int initialDealCount,int roundDealWeight)
        {
            return new CardDefinition(
                id,CardTarget.Self,MoveKind.Walk,
                initialDealCount, roundDealWeight,
                new[] {1,1,1},new ValueRange(3,5));
        }

        [Test]
        public void テスト6001_初期配布定義A2定義B3_期待値_5_2_3()
        {
            var catalog = new CardCatalog(new[]
            {
                MakeDefinition(1,2,10),
                MakeDefinition(2,3,10),
            });
            var dealer = new CardDealer(catalog, new FixedRandomSource(0));

            var hand = dealer.DealInitialHand();

            int countA = 0;
            int countB = 0;
            foreach(var card in hand)
            {
                if (card.Definition.Id == 1) countA++;
                if (card.Definition.Id == 2) countB++;
            }

            Assert.AreEqual(5, hand.Count);
            Assert.AreEqual(2, countA);
            Assert.AreEqual(3, countB);
        }
        [Test]
        public void テスト6002_二人分配る_InstanceIdが重複しない()
        {
            var catalog = new CardCatalog(new[]
            {
                MakeDefinition(1,2,10),
                MakeDefinition(2,3,10),
            });
            var dealer = new CardDealer(catalog, new FixedRandomSource(0));

            var ids = new System.Collections.Generic.HashSet<int>();
            foreach (var card in dealer.DealInitialHand())
                Assert.IsTrue(ids.Add(card.InstanceId));
            foreach (var card in dealer.DealInitialHand())
                Assert.IsTrue(ids.Add(card.InstanceId));

            Assert.AreEqual(10, ids.Count);
        }

        [Test]
        public void テスト6003_巡の配布_重み0の定義は呼ばれない()
        {
            var catalog = new CardCatalog(new[]
            {
                MakeDefinition(1,0,0),
                MakeDefinition(2,0,10),
            });
            var dealer = new CardDealer(catalog, new FixedRandomSource(0));

            var card = dealer.DealRoundCard();

            Assert.AreEqual(2, card.Definition.Id);
        }

        [Test]
        public void テスト6004_効果範囲が最小になる_乱数に最小値より小さい値を入れたとき最小が選ばれる()
        {
            var catalog = new CardCatalog(new[]
            {
                MakeDefinition(1,1,10),
                MakeDefinition(2,1,10),
            });
            var dealer = new CardDealer(catalog, new FixedRandomSource(0));

            var hand = dealer.DealInitialHand();
            int valuesIsMinCounter = 0;
            foreach(var card in hand)
            {
                if(card.Value == 3)
                    valuesIsMinCounter++;
            }

            Assert.AreEqual(2, valuesIsMinCounter);
        }

        [Test]
        public void テスト6005_カタログがNull_例外処理()
        {
            Assert.Throws<ArgumentNullException>(() => new CardDealer(null, new FixedRandomSource(0)));
        }

        [Test]
        public void テスト6006_乱数がNull_例外処理()
        {
            var catalog = new CardCatalog(new[]
            {
                MakeDefinition(1,1,10),
                MakeDefinition(2,1,10),
            });
            Assert.Throws<ArgumentNullException>(() => new CardDealer(catalog, null));
        }

        [Test]
        public void テスト6007_初期配布が0枚の定義のみ_初期手札が空()
        {
            var catalog = new CardCatalog(new[]
            {
                MakeDefinition(1,0,10),
                MakeDefinition(2,0,10),
                MakeDefinition(3,0,10),
            });

            var dealer = new CardDealer(catalog, new FixedRandomSource(3));
            int initcialCardCount = dealer.DealInitialHand().Count;

            Assert.AreEqual(0, initcialCardCount);
        }
    }
}
