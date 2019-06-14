using System.Collections.Generic;

namespace Puzzle.Services.Interfaces
{
    /// <summary>
    /// Game field generator interface.
    /// </summary>
    public interface IGameFieldCreationService
    {
        /// <summary>
        /// Initialization with default values.
        /// </summary>
        void Init();

        /// <summary>
        /// Initialization with custom params. Needs for custom game field generating.
        /// </summary>
        /// <param name="gameFieldSize">Size of custom game field.</param>
        /// <param name="emptyCellIndex">Index of empty cell.</param>
        /// <param name="links">Cell's links.</param>
        void Init(int gameFieldSize, int emptyCellIndex, List<KeyValuePair<int, int>> links);

        /// <summary>
        /// Cunstruct game field.
        /// </summary>
        /// <returns>Constructed game field <see cref="GameField"/>.</returns>
        GameField Construct();
    }
}
