using System.Collections.Generic;

namespace Puzzle
{
    /// <summary>
    /// Class that represent game field cell.
    /// </summary>
    public class FieldCell
    {
        /// <summary>
        /// Index of game field cell.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Contains cells connected to current field cell. 
        /// </summary>
        public List<FieldCell> Links { get; set; }

        public FieldCell(int index)
        {
            Index = index;
            Links = new List<FieldCell>();
        }
    }
}
