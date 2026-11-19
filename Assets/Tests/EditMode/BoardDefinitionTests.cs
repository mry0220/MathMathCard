using NUnit.Framework;
using System;

namespace MathMathCard.Core.Tests
{
    public class BoardDefinitionTests
    {
        [Test]
        public void テスト1001_正常な一直線_生成できる()
        {
            // 準備: 0-1-2 の一直線(2がゴール)
            var cells = new[]
            {
                new CellDefinition(0,CellType.Normal,new[] { 1 }),
                new CellDefinition(1,CellType.Normal,new[] { 0,2 }),
                new CellDefinition(2,CellType.Goal,new[] { 1 }),
            };

            // 実行
            var board = new BoardDefinition(cells, 0, 2);

            // 確認：マス1を取り出すと、IDが1になっている 
            Assert.AreEqual(1, board.GetCell(1).Id);
        }

        [Test]
        public void テスト1002_ID重複_例外処理()
        {
            // 準備: 0-1-2 の一直線(2がゴール)
            var cells = new[]
            {
                new CellDefinition(0,CellType.Normal,new[] { 0 }),
                new CellDefinition(0,CellType.Normal,new[] { 0 }),
            };


            // 確認：Throwsを使うと、例外が一つでも走ったら成功 
            Assert.Throws<ArgumentException>(() => new BoardDefinition(cells,0, 0));
        }

        [Test]
        public void テスト1003_存在しない隣接_例外処理()
        {
            var cells = new[]
            {
                new CellDefinition(0,CellType.Normal,new[]{9}),
                new CellDefinition(1,CellType.Goal,new[]{0}),
            };

            Assert.Throws<ArgumentException>(() => new BoardDefinition(cells, 0, 1));
        }

        [Test]
        public void テスト1004_自分自身が隣接_例外処理()
        {
            var cells = new[]
            {
                new CellDefinition(0,CellType.Normal,new[]{ 0}),
                new CellDefinition(1,CellType.Goal,new[]{ 0}),
            };

            Assert.Throws<ArgumentException>(() => new BoardDefinition(cells,0,1));
        }

        [Test]
        public void テスト1005_隣接に重複_例外処理()
        {
            var cells = new[]
            {
                new CellDefinition(0,CellType.Normal,new[]{1}),
                new CellDefinition(1,CellType.Goal,new[]{0,0}),
            };

            Assert.Throws<ArgumentException>(() => new BoardDefinition(cells, 0, 1));
        }

        [Test]
        public void テスト1006_片方のみの隣接_例外処理()
        {
            var cells = new[]
            {
                new CellDefinition(0,CellType.Normal,new[]{1}),
                new CellDefinition(1,CellType.Normal,new[]{2}),
                new CellDefinition(2,CellType.Goal,new[]{1}),
            };

            Assert.Throws<ArgumentException>(() => new BoardDefinition(cells, 0, 2));
        }

        [Test]
        public void テスト1007_Startが存在しない_例外処理()
        {
            var cells = new[]
            {
                new CellDefinition(0,CellType.Normal,new[]{1}),
                new CellDefinition(1,CellType.Normal,new[]{0,2}),
                new CellDefinition(2,CellType.Goal,new[]{1}),
            };

            Assert.Throws<ArgumentException>(() => new BoardDefinition(cells, 9, 2));
        }

        [Test]
        public void テスト1008_Goalが存在しない_例外処理()
        {
            var cells = new[]
            {
                new CellDefinition(0,CellType.Normal,new[]{1}),
                new CellDefinition(1,CellType.Normal,new[]{0,2}),
                new CellDefinition(2,CellType.Goal,new[]{1}),
            };

            Assert.Throws<ArgumentException>(() => new BoardDefinition(cells, 0, 9));
        }

        [Test]
        public void テスト1009_GoalマスのタイプがGoalじゃない_例外処理()
        {
            var cells = new[]
            {
                new CellDefinition(0,CellType.Normal,new[]{1}),
                new CellDefinition(1,CellType.Normal,new[]{0,2}),
                new CellDefinition(2,CellType.Normal,new[]{1}),
            };

            Assert.Throws<ArgumentException>(() => new BoardDefinition(cells, 0, 2));
        }

        [Test]
        public void テスト1010_Goalに到達できない_例外処理()
        {
            var cells = new[]
            {
                new CellDefinition(0, CellType.Normal,new[]{1}),
                new CellDefinition(1, CellType.Normal,new[]{0}),
                new CellDefinition(2, CellType.Goal,new int[0]),
            };

            Assert.Throws<InvalidOperationException>(() => new BoardDefinition(cells, 0, 2));
        }
    }
}
