namespace Puzzle.Services.Interfaces
{
    public interface IPuzzleValidationService
    {
        void Validate(int[] input, GameField gameField);
    }
}
