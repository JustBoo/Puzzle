using Puzzle.Interface;
using Puzzle.Services.Interfaces;

namespace Puzzle
{
    /// <summary>
    /// Puzzle resolver class. 
    /// </summary>
    public class PuzzleResolverService : IResolver
    {
        private readonly IPathFinder _pathFinder;
        private readonly IPuzzleValidationService _puzzleValidationService;
        private readonly IGameFieldCreationService _gameFieldCreationService;

        public PuzzleResolverService(
            IPuzzleValidationService puzzleValidationService,
            IPathFinder pathFinder,
            IGameFieldCreationService gameFieldCreationService)
        {
            _pathFinder = pathFinder;
            _puzzleValidationService = puzzleValidationService;
            _gameFieldCreationService = gameFieldCreationService;
        }

        /// <summary>
        /// Find puzzle solution.
        /// </summary>
        /// <param name="input">Array of input integers.</param>
        /// <returns>Array of moved numbers for solution.</returns>
        public int[] Solve(int[] input)
        {
            var gameField = GetGameField();

            _puzzleValidationService.Validate(input, gameField);

            return _pathFinder.SearchPath(input, gameField);
        }

        private GameField GetGameField()
        {
            _gameFieldCreationService.Init();

            return _gameFieldCreationService.Construct();
        }
    }
}
