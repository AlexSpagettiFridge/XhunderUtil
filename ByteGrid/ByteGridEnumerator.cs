using System.Collections;
using System.Collections.Generic;

namespace XhunderUtil.ByteGrid
{
    /// <summary>
    /// Enumerator for <see cref="ByteGrid"/>
    /// </summary>
    internal class ByteGridEnumerator : IEnumerator<ByteGridEntry>
    {
        private readonly ByteGrid byteGrid;
        private int x = -1, y = 0;
        public ByteGridEntry Current => new(byteGrid, x, y, byteGrid[x, y]);

        object IEnumerator.Current => Current;

        public ByteGridEnumerator(ByteGrid byteGrid)
        {
            this.byteGrid = byteGrid;
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            x++;
            if (x >= byteGrid.Width)
            {
                x = 0;
                y++;
                if (y >= byteGrid.Height)
                {
                    return false;
                }
            }
            return true;
        }

        public void Reset()
        {
            x = -1; y = 0;
        }
    }
}
