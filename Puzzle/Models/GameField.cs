using System.Collections.Generic;

namespace Puzzle
{
    /// <summary>
    /// Class that represents game field.
    /// </summary>
    public class GameField
    {
        /// <summary>
        /// Constructor of the <see cref="GameField"/>.
        /// </summary>
        public GameField()
        {
            Cells = new List<FieldCell>();
        }

        /// <summary>
        /// List of the game field cells. <see cref="FieldCell"/>
        /// </summary>
        public List<FieldCell> Cells { get; set; }

        /// <summary>
        /// Position of the empty cell on the game field.
        /// </summary>
        public int EmptyCellIndex { get; set; }
    }
}
