namespace Puzzle.Interface
{
    /// <summary>
    /// Puzzle resolver interface. 
    /// </summary>
    public interface IResolver
    {
        /// <summary>
        /// Find puzzle solution.
        /// </summary>
        /// <param name="input">Array of input integers.</param>
        /// <returns>Array of moved numbers for solution.</returns>
        int[] Solve(int[] input);
    }
}
