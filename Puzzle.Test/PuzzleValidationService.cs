using NUnit.Framework;
using Puzzle;
using Puzzle.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class PuzzleValidationServiceTest
    {
        List<KeyValuePair<int, int>> Links = new List<KeyValuePair<int, int>>(){
            new KeyValuePair<int, int>(1,2),
            new KeyValuePair<int, int>(2,3),
            new KeyValuePair<int, int>(3,4),
            new KeyValuePair<int, int>(4,5),
            new KeyValuePair<int, int>(5,6),
            new KeyValuePair<int, int>(6,7),
            new KeyValuePair<int, int>(7,8),
            new KeyValuePair<int, int>(8,9),
            new KeyValuePair<int, int>(9,0)
        };

        private const int gameFieldSize = 10;

        GameField gameField;

        PuzzleValidationService puzzleValidationService;

        [SetUp]
        public void InitPuzzleValidationService()
        {
            puzzleValidationService = new PuzzleValidationService();

            gameField = new GameField();

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
        }

        [Test]
        public void Validate_InputLengthAndGameFieldSizeEqualAllLinksExist_NoExceptions()
        {
            var testInput = new int[] { 1, 2, 3, 4, 6, 5, 0, 7, 8, 9 };

            Assert.DoesNotThrow(() => puzzleValidationService.Validate(testInput, gameField));
        }

        [Test]
        public void Validate_InputLengthLessThenGameFieldSize_ArgumentException()
        {
            var testInput = new int[] { 1, 2, 3, 4, 6, 5, 0, 7, 8};

            Assert.Throws<ArgumentException>(() => puzzleValidationService.Validate(testInput, gameField));
        }

        [Test]
        public void Validate_InputLengthBiggerThenGameFieldSize_ArgumentException()
        {
            var testInput = new int[] { 1, 2, 3, 4, 6, 5, 0, 7, 8, 9 };
            gameField.Cells.RemoveAt(9);

            Assert.Throws<ArgumentException>(() => puzzleValidationService.Validate(testInput, gameField));
        }

        [Test]
        public void Validate_InputArrayWithDuplicates_ArgumentException()
        {
            var testInput = new int[] { 1, 2, 4, 4, 6, 5, 0, 7, 8, 9 };

            Assert.Throws<ArgumentException>(() => puzzleValidationService.Validate(testInput, gameField));
        }

        [Test]
        public void Validate_GameFieldCellWithoutLinks_ArgumentException()
        {
            var testInput = new int[] { 1, 2, 3, 4, 6, 5, 0, 7, 8, 9 };

            gameField.Cells[0].Links = new List<FieldCell>();

            Assert.Throws<ArgumentException>(() => puzzleValidationService.Validate(testInput, gameField));
        }
    }
}