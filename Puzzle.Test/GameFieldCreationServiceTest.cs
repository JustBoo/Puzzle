using NUnit.Framework;
using Puzzle;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class GameFieldCreationServiceTest
    {
        GameFieldCreationService gameFieldCreationService;

        List<KeyValuePair<int, int>> customLinks = new List<KeyValuePair<int, int>>(){
            new KeyValuePair<int, int>(0,1),
            new KeyValuePair<int, int>(1,2),
            new KeyValuePair<int, int>(2,3),
            new KeyValuePair<int, int>(3,4),
            new KeyValuePair<int, int>(4,5),
            new KeyValuePair<int, int>(5,0)
        };

        [SetUp]
        public void Init()
        {
            gameFieldCreationService = new GameFieldCreationService();
        }

        [Test]
        public void Init_CorrectCustomParams_NoErrors()
        {
            var gameFieldSize = 6;
            var emptyCellIndex = 4;

            Assert.DoesNotThrow(() => gameFieldCreationService.Init(gameFieldSize, emptyCellIndex, customLinks));
        }

        [Test]
        public void Init_SmallGameFieldSize_ArgumentException()
        {
            var gameFieldSize = 1;
            var emptyCellIndex = 4;

            Assert.Throws<ArgumentException>(() => gameFieldCreationService.Init(gameFieldSize, emptyCellIndex, customLinks));
        }

        [Test]
        public void Init_EmptyIndexBiggerThenGameFieldSize_ArgumentException()
        {
            var gameFieldSize = 4;
            var emptyCellIndex = 6;

            Assert.Throws<ArgumentException>(() => gameFieldCreationService.Init(gameFieldSize, emptyCellIndex, customLinks));
        }

        [Test]
        public void Init_LinksListIsEmpty_ArgumentException()
        {
            var gameFieldSize = 6;
            var emptyCellIndex = 4;
            var links = new List<KeyValuePair<int,int>>();

            Assert.Throws<ArgumentException>(() => gameFieldCreationService.Init(gameFieldSize, emptyCellIndex, links));
        }

        [Test]
        public void Init_LinkIndexTargetCellIsInvalid_OperationException()
        {
            var gameFieldSize = 6;
            var emptyCellIndex = 4;
            customLinks.Add(new KeyValuePair<int, int>(7, 0));

            gameFieldCreationService.Init(gameFieldSize, emptyCellIndex, customLinks);

            Assert.Throws<InvalidOperationException>(() => gameFieldCreationService.Construct());
        }
    }
}