using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MathMathCard.Core.Tests
{
    public class ValueRangeTests
    {
        [Test]
        public void テスト3001_最小値1最大値6_期待値6()
        {
            ValueRange range = new ValueRange(1,6);
            Assert.AreEqual(6,range.Count);
        }

        [Test]
        public void テスト3003_最小値3最大値3_期待値1()
        {
            ValueRange range = new ValueRange(3, 3);
            Assert.AreEqual(1, range.Count);
        }

        [Test]
        public void テスト3004_最小値と最大値が反対_例外処理()
        {
            Assert.Throws<ArgumentException>(() => new ValueRange(6,1));
        }
    }
}
