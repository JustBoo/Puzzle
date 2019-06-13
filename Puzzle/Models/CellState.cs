namespace Puzzle.Models
{
    /// <summary>
    /// Class that represent current state of game field's cell.
    /// </summary>
    public class CellState
    {
        /// <summary>
        /// Index of the game field's cell related to this state.
        /// </summary>
        public int CellIndex { get; set; }

        /// <summary>
        /// Current value of game field's cell.
        /// </summary>
        public int CellValue { get; set; }

        /// <summary>
        /// If cell index and cell value are equal than number located on it native position,
        /// IsTerminal = <c>true</c>, otherwise <c>false</c>>. 
        /// </summary>
        public bool IsTerminal
        {
            get
            {
                return CellIndex == CellValue;
            }
        }

        public CellState(int cellIndex, int cellValue)
        {
            CellIndex = cellIndex;
            CellValue = cellValue;
        }

        /// <summary>
        /// Copy game field's cell state.
        /// </summary>
        /// <returns>CellState</returns>
        public CellState Copy()
        {
            return new CellState(CellIndex, CellValue);
        }
    }
}
