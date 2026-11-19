using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MathMathCard.Core.Tests
{
    public class WeightedTableTests
    {
        [Test]
        public void テスト2001_重みが均一の場合_期待する値0()
        {
            WeightedTable table = new WeightedTable(new List<int> {1,1});

            FixedRandomSource randomSource = new FixedRandomSource(0);
            

            Assert.AreEqual(0, table.Pick(randomSource));
        }

        [Test]
        public void テスト2002_重みが均一の場合_期待する値1()
        {
            WeightedTable table = new WeightedTable(new List<int> { 1, 1 });

            FixedRandomSource randomSource = new FixedRandomSource(1);
            Assert.AreEqual(1,table.Pick(randomSource));
        }

        [Test]
        public void テスト2003_重みが違う場合_期待する値1()
        {
            WeightedTable table = new WeightedTable(new List<int> { 30, 30, 20 });

            FixedRandomSource randomSource = new FixedRandomSource(45);
            Assert.AreEqual(1,table.Pick(randomSource));
        }

        [Test]
        public void テスト2004_重みの1つめが0の場合_期待する値1()
        {
            WeightedTable table = new WeightedTable(new List<int> { 0, 5 });

            FixedRandomSource randomSource = new FixedRandomSource(0);
            Assert.AreEqual(1, table.Pick(randomSource));
        }

        [Test]
        public void テスト2005_合計が0_例外処理()
        {
            Assert.Throws<ArgumentException>(() =>  new WeightedTable(new List<int> { 0, 0 }));
        }

        [Test]
        public void テスト2006_負の重み_例外処理()
        {
            Assert.Throws<ArgumentException>(() => new WeightedTable(new List<int> { -1, 5 }));
        }

        [Test]
        public void テスト2007_要素が空_例外処理()
        {
            Assert.Throws<ArgumentException>(() => new WeightedTable(new List<int> { }));
        }

    }
}
