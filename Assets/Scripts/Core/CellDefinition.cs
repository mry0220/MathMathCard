using System;
using System.Collections.Generic;

namespace MathMathCard.Core
{
    public sealed class CellDefinition
    {
        private readonly int _id;
        private readonly CellType _type;
        private readonly List<int> _neighbors;

        public CellDefinition(int id, CellType type, IReadOnlyList<int> neighbors)
        {
            if (neighbors == null)
                throw new ArgumentNullException(nameof(neighbors), "隣接マスIDがNullです!!");

            _id = id;
            _type = type;
            _neighbors = new List<int>(neighbors);
        }

        public int Id => _id;
        public CellType Type => _type;
        public IReadOnlyList<int> Neighbors => _neighbors;
    }
}
