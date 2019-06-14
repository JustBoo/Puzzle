using Puzzle.Extensions;
using Puzzle.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzle
{
    /// <summary>
    /// Game field generator class.
    /// </summary>
    public class GameFieldCreationService : IGameFieldCreationService
    {
        private const int DefaultEmptyCellIndex = 4;

        private const int DefaultGameFieldSize = 10;

        private List<KeyValuePair<int, int>> DefaultLinks = new List<KeyValuePair<int, int>>(){
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

        private int GameFieldSize { get;  set; }

        private int EmptyCellIndex { get;  set; }

        private List<KeyValuePair<int, int>> Links { get; set; }

        public static GameField GameField { get; set; }

        /// <summary>
        /// Initialization with default values.
        /// </summary>
        public void Init()
        {
            GameFieldSize = DefaultGameFieldSize;
            EmptyCellIndex = DefaultEmptyCellIndex;
            Links = DefaultLinks;
        }

        /// <summary>
        /// Initialization with custom params. Needs for custom game field generating.
        /// </summary>
        /// <param name="gameFieldSize">Size of custom game field.</param>
        /// <param name="emptyCellIndex">Index of empty cell.</param>
        /// <param name="links">Cell's links.</param>
        public void Init(int gameFieldSize, int emptyCellIndex, List<KeyValuePair<int, int>> links)
        {
            if (gameFieldSize < 2)
            {
                throw new ArgumentException($"Unaccaptable game field size. Can not be less then 2 cells. Game field size: {gameFieldSize}");
            }

            if (links.IsNullOrEmpty())
            {
                throw new ArgumentException("Links are empty. Custom game field must be initialized with custom links.");
            }

            if (emptyCellIndex > gameFieldSize)
            {
                throw new ArgumentException("Empty cell index is bigger then game field size.");
            }

            GameFieldSize = gameFieldSize;
            EmptyCellIndex = emptyCellIndex;
            Links = links;
        }

        /// <summary>
        /// Cunstruct game field.
        /// </summary>
        /// <returns>Constructed game field <see cref="GameField"/>.</returns>
        public GameField Construct()
        {
            var gameField = new GameField();

            CreateGameCells(gameField);
            EstablishLinks(gameField);
            gameField.EmptyCellIndex = EmptyCellIndex;

            return gameField;
        }

        private void CreateGameCells(GameField gameField)
        {
            for (int i = 0; i < GameFieldSize; i++)
            {
                gameField.Cells.Add(new FieldCell(i));
            }
        }

        private void EstablishLinks(GameField gameField)
        {
            foreach (var link in Links)
            {
                var firstCell = gameField.Cells.First(x => x.Index == link.Key);
                var secondCell = gameField.Cells.First(x => x.Index == link.Value);

                //Establishing one-way link
                firstCell.Links.Add(secondCell);
                //Establishing two-way link
                secondCell.Links.Add(firstCell);
            }
        }
    }
}
