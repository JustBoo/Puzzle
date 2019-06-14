using NUnit.Framework;
using Puzzle;
using Puzzle.PathFinders.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class AStarPathFinderTest
    {
        private const int GameFieldSize = 10;

        private const int EmptyCellIndex = 4;

        GameField gameField;

        AStarPathFinder aStarPathFinder;

        private List<KeyValuePair<int, int>> Links = new List<KeyValuePair<int, int>>(){
            new KeyValuePair<int, int>(1,2),
            new KeyValuePair<int, int>(1,3),
            new KeyValuePair<int, int>(2,4),
            new KeyValuePair<int, int>(3,0),
            new KeyValuePair<int, int>(3,5),
            new KeyValuePair<int, int>(4,0),
            new KeyValuePair<int, int>(4,6),
            new KeyValuePair<int, int>(5,7),
            new KeyValuePair<int, int>(6,8),
            new KeyValuePair<int, int>(7,8),
            new KeyValuePair<int, int>(7,9),
            new KeyValuePair<int, int>(8,9)
        };

        [SetUp]
        public void Init()
        {
            aStarPathFinder = new AStarPathFinder();

            gameField = new GameField();

            gameField.EmptyCellIndex = EmptyCellIndex;

            for (int i = 0; i < GameFieldSize; i++)
            {
                gameField.Cells.Add(new FieldCell(i));
            }
            foreach (var link in Links)
            {
                var firstCell = gameField.Cells.First(x => x.Index == link.Key);
                var secondCell = gameField.Cells.First(x => x.Index == link.Value);

                firstCell.Links.Add(secondCell);
                secondCell.Links.Add(firstCell);
            }
        }

        [TestCase(new int[] { 1, 2, 3, 4, 6, 5, 0, 7, 8, 9 }, new int[] { 6, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4, 6, 5, 8, 9, 7, 0 }, new int[] { 9, 7, 8, 6, 4 })]
        public void SearchPath_SolvableInput_ReturnsSolution(int[] inputArray, int[] expectedResultArray)
        {
            var result = aStarPathFinder.SearchPath(inputArray, gameField);

            Assert.AreEqual(result, expectedResultArray);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 6, 5, 0, 7})]
        public void SearchPath_InputWithNoSolution_EmptyArray(int[] input)
        {
            var Links = new List<KeyValuePair<int, int>>(){
                new KeyValuePair<int, int>(1,2),
                new KeyValuePair<int, int>(2,3),
                new KeyValuePair<int, int>(3,4),
                new KeyValuePair<int, int>(5,0),
                new KeyValuePair<int, int>(0,6),
                new KeyValuePair<int, int>(6,1)
            };

            var gameFieldSize = 7;
            var emptyCellIndex = 2;

            gameField = new GameField();
            gameField.EmptyCellIndex = emptyCellIndex;

            for (int i = 0; i < gameFieldSize; i++)
            {
                gameField.Cells.Add(new FieldCell(i));
            }
            foreach (var link in Links)
            {
                var firstCell = gameField.Cells.First(x => x.Index == link.Key);
                var secondCell = gameField.Cells.First(x => x.Index == link.Value);

                firstCell.Links.Add(secondCell);
                secondCell.Links.Add(firstCell);
            }
            
            var result = aStarPathFinder.SearchPath(input, gameField);
            Assert.AreEqual(result, Array.Empty<int>());
        }
    }
}