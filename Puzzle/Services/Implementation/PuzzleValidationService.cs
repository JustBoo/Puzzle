﻿using Puzzle.Services.Interfaces;
using System;
using System.Linq;

namespace Puzzle.Services.Implementation
{
    /// <summary>
    /// Validation service class.  
    /// </summary>
    public class PuzzleValidationService : IPuzzleValidationService
    {
        /// <summary>
        /// Validate path finder input params.
        /// </summary>
        /// <param name="input">Array of input integers.</param>
        /// <param name="gameField">Generated game field <see cref="GameField"/>.</param>
        public void Validate(int[] input, GameField gameField)
        {

            if (input.Length != gameField.Cells.Count)
            {
                throw new ArgumentException($"Incorrect number of input values or number of game field cells. Input leangth: {input.Length}. Game field size: {gameField.Cells.Count}.");
            }

            if (input.Length != input.Distinct().Count())
            {
                throw new ArgumentException("Input contains duplicates. It's prohibited by the rules.");
            }

            if(gameField.Cells.Any(x => x.Links.Count == 0))
            {
                throw new ArgumentException($"One or more cell without links (Cell index: {gameField.Cells.First().Index})");
            }
        }
    }
}
