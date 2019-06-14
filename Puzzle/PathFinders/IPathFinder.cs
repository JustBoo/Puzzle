namespace Puzzle.Services.Interfaces
{
    /// <summary>
    /// Interface for path finding algorithms. 
    /// </summary>
    public interface IPathFinder
    {
        /// <summary>
        /// Searching for sequance of moves to terminal state of puzzle.
        /// </summary>
        /// <param name="input">Array of input integers.</param>
        /// <param name="gameField">Generated game field.</param>
        /// <returns>Array of moved numbers. If there is no solution, then empty array.</returns>
        int[] SearchPath(int[] input, GameField gameField); 
    }
}
