using System;
using System.Collections.Generic;

namespace MathMathCard.Core
{
    public sealed class BoardDefinition
    {
        private readonly List<CellDefinition> _cells;
        private readonly int _startCellId;
        private readonly int _goalCellId;

        private readonly Dictionary<int, CellDefinition> _cellDefs = new();

        public BoardDefinition(
            IReadOnlyList<CellDefinition> cells,
            int startCellId,
            int goalCellId
            )
        {
            if (cells == null)
               throw new ArgumentNullException(nameof(cells),"Cellの一覧情報がありません");

            _cells = new (cells);
            _startCellId = startCellId;
            _goalCellId = goalCellId;
            int increment = 0;
            foreach(var cell in cells)
            {
                if (cell == null)
                    throw new ArgumentNullException(nameof(cells), $"Cell[{increment}]がありませんでした");

                if (_cellDefs.ContainsKey(cell.Id))
                    throw new ArgumentException($"{cell.Id}はすでに登録されています",nameof(cells));
                _cellDefs.Add(cell.Id, cell);
                increment++;
            }
            ValidateNeighbors(cells);
        }

        private void ValidateNeighbors(IReadOnlyList<CellDefinition> cells)
        {
            foreach (var cell in cells)
            {
                HashSet<int> neighborIDs = new();
                foreach (var neighbor in cell.Neighbors)
                {
                    if (!_cellDefs.ContainsKey(neighbor))
                        throw new ArgumentException($"{cell.Id}のマスに隣接したCell[{neighbor}]がありません", nameof(cells));
                    if (cell.Id == neighbor)
                        throw new ArgumentException($"隣接したCellに自分自身が登録されています MyID : {cell.Id} neighborID : {neighbor}", nameof(cells));
                    if (!neighborIDs.Add(neighbor))
                        throw new ArgumentException($"隣接したCellに２重で登録されています DuplicationID : {neighbor} myID : {cell.Id}", nameof(cells));
                    var neighborCell = _cellDefs[neighbor];

                    bool isIncludeSelf = false;
                    foreach (int neighborNeibor in neighborCell.Neighbors)
                    {
                        if (neighborNeibor == cell.Id)
                        {
                            isIncludeSelf = true;
                            break;
                        }
                    }

                    if (!isIncludeSelf)
                        throw new ArgumentException( $"隣接したマスに自身が登録されていませんでした。neighborID : {neighbor} myID :{cell.Id}", nameof(cells));
                }
            }
        }

        public IReadOnlyList<CellDefinition> Cells => _cells;
        public int StartCellId => _startCellId;
        public int GoalCellId => _goalCellId;

        public CellDefinition GetCell(int id)
        {
            if (!_cellDefs.TryGetValue(id, out var cellDef))
                throw new ArgumentException($"お探しのCellが見つかりませんでした CellId{id}", nameof(id));
            return cellDef;
        }
    }
}
