using Puzzle.Models;
using Puzzle.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzle.PathFinders.Implementation
{
    /// <summary>
    /// A* algorith path finder class.
    /// </summary>
    public class AStarPathFinder : IPathFinder
    {
        private GameField GameField { get; set; }

        /// <summary>
        /// Searching for sequence of moves to terminal state of puzzle.
        /// </summary>
        /// <param name="input">Array of input integers.</param>
        /// <param name="gameField">Generated game field.</param>
        /// <returns>Array of moved numbers. If there is no solution, then empty array.</returns>
        public int[] SearchPath(int[] input, GameField gameField)
        {
            GameField = gameField;

            var closedSet = new List<PathNode>();
            var openSet = new List<PathNode>();

            openSet.Add(InitStartNode(input));

            while (openSet.Count > 0)
            {
                var currentNode = openSet.OrderBy(x => x.Weight).First();

                if (isTerminalSequence(currentNode.CellsStateMap))
                {
                    return GetMoves(currentNode);
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (var nextLevelNode in GetNextLevelNodes(currentNode))
                {
                    if (closedSet.Any(node => CompareCellStateMaps(node.CellsStateMap, nextLevelNode.CellsStateMap)))
                    {
                        continue;
                    }

                    var openNode = openSet.FirstOrDefault(node =>
                      CompareCellStateMaps(node.CellsStateMap, nextLevelNode.CellsStateMap));

                    if (openNode == null)
                    {
                        openSet.Add(nextLevelNode);
                    }
                    else if (openNode.StepNumber > nextLevelNode.StepNumber)
                    {
                        openNode.ParentNode = currentNode;
                        openNode.StepNumber = nextLevelNode.StepNumber;
                    }
                }
            }

            return Array.Empty<int>();
        }

        private List<PathNode> GetNextLevelNodes(PathNode currentNode)
        {
            if (currentNode == null)
            {
                throw new ArgumentNullException("Unable to get next level nodes of ");
            }

            var nextLevelNode = new List<PathNode>();

            var emptyCellIndex = currentNode.CellsStateMap.First(x => x.CellValue == 0).CellIndex;

            var availableMoves = GameField.Cells
                .First(x => x.Index == emptyCellIndex).Links
                .Select(x => x.Index)
                .ToArray();

            if (currentNode.ParentNode != null)
            {
                availableMoves = availableMoves
                    .Where(x => x != currentNode.ParentNode.CellsStateMap.First(y => y.CellValue == 0).CellIndex)
                    .ToArray();
            }

            foreach (var moveCellIndex in availableMoves)
            {
                var cellStateMap = new List<CellState>();

                currentNode.CellsStateMap.ForEach(x => cellStateMap.Add(x.Copy()));

                var movedNumber = MakeMove(cellStateMap, moveCellIndex);

                nextLevelNode.Add(new PathNode()
                {
                    MovedNumber = movedNumber,
                    ParentNode = currentNode,
                    StepNumber = ++currentNode.StepNumber,
                    TotalDistanceForCompletion = GetDistanceToCompletion(cellStateMap),
                    CellsStateMap = cellStateMap
                });
            }
            return nextLevelNode;
        }

        private int MakeMove(List<CellState> cellStateMap, int moveCellIndex)
        {
            var zeroCell = cellStateMap.First(x => x.CellValue == 0);
            var targetCell = cellStateMap.First(x => x.CellIndex == moveCellIndex);
            var movedNumber = targetCell.CellValue;
            zeroCell.CellValue = movedNumber;
            targetCell.CellValue = 0;

            return movedNumber;
        }

        private bool CompareCellStateMaps(List<CellState> cellsStateMap1, List<CellState> cellsStateMap2)
        {
            foreach (var cellState in cellsStateMap1)
            {
                var cellToCompare = cellsStateMap2.First(x => x.CellIndex == cellState.CellIndex);
                if (cellState.CellValue != cellToCompare.CellValue)
                {
                    return false;
                }
            }

            return true;
        }

        private int[] GetMoves(PathNode currentNode)
        {
            var resultList = new List<int>();
            while (currentNode.ParentNode != null)
            {
                if (currentNode.MovedNumber == null)
                {
                    throw new InvalidOperationException($"Invalid state of the node on step: {currentNode.StepNumber}");
                }

                resultList.Add(currentNode.MovedNumber.Value);
                currentNode = currentNode.ParentNode;
            }

            resultList.Reverse();

            return resultList.ToArray();
        }

        private PathNode InitStartNode(int[] input)
        {
            List<CellState> cellStateMap = SetMapFromInput(input);

            PathNode startNode = new PathNode()
            {
                CellsStateMap = cellStateMap,
                ParentNode = null,
                StepNumber = 0,
                MovedNumber = null,
                TotalDistanceForCompletion = GetDistanceToCompletion(cellStateMap)
            };

            return startNode;
        }

        private List<CellState> SetMapFromInput(int[] input)
        {
            List<CellState> map = new List<CellState>();
            var isZeroFound = false;

            for (var i = 0; i < GameField.Cells.Count; i++)
            {
                if (i == GameField.EmptyCellIndex - 1)
                {
                    map.Add(new CellState(0, input[i]));
                    isZeroFound = true;
                }
                else
                {
                    map.Add(new CellState(isZeroFound ? i : i + 1, input[i]));
                }
            }

            return map;
        }

        private int GetDistanceToCompletion(List<CellState> map)
        {
            int totalLength = 0;

            foreach (var cellState in map)
            {
                if (!cellState.IsTerminal)
                {
                    ++totalLength;
                }
            }

            return totalLength;
        }
        private bool isTerminalSequence(List<CellState> cellsStateMap)
        {
            return GetDistanceToCompletion(cellsStateMap) == 0;
        }
    }
}
