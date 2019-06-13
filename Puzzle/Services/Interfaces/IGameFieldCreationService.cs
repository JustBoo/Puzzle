using System.Collections.Generic;

namespace Puzzle.Services.Interfaces
{
    public interface IGameFieldCreationService
    {
        void Init();
        void Init(int gameFieldSize, int zeroCellIndex, List<KeyValuePair<int, int>> links);
        GameField Construct();
    }
}
