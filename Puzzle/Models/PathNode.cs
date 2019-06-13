using Puzzle.Models;
using System.Collections.Generic;

namespace Puzzle
{
    /// <summary>
    /// Class of the graph's node.
    /// </summary>
    public class PathNode
    {
        /// <summary>
        /// Parant graph's node.
        /// </summary>
        public PathNode ParentNode { get; set; }

        /// <summary>
        /// Value of the moved number to enter current path node.
        /// </summary>
        public int? MovedNumber { get; set; } 

        /// <summary>
        /// Number that represents the level of graph.
        /// </summary>
        public int StepNumber { get; set; }

        /// <summary>
        /// Value that represents the distance to terminate state of the system.
        /// </summary>
        public int TotalDistanceForCompletion { get; set; }

        /// <summary>
        /// The node of graph's weight.
        /// </summary>
        public int Weight
        {
            get
            {
                return TotalDistanceForCompletion + StepNumber;
            }
        }

        /// <summary>
        /// List of the game field cells states <see cref="CellState"/>.
        /// </summary>
        public List<CellState> CellsStateMap { get; set; }
    }
}
