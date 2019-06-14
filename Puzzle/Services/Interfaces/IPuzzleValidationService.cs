namespace Puzzle.Services.Interfaces
{
    /// <summary>
    /// Validation service interface.  
    /// </summary>
    public interface IPuzzleValidationService
    {
        /// <summary>
        /// Validate path finder input params.
        /// </summary>
        /// <param name="input">Array of input integers.</param>
        /// <param name="gameField">Generated game field.</param>
        void Validate(int[] input, GameField gameField);
    }
}
