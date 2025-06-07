using System.Collections;
using System.Collections.Generic;

namespace XhunderUtil.ByteGrid
{
    /// <summary>
    /// A Matrix of Bytes, usable as a simplified Tilemap.
    /// </summary>
    public class ByteGrid : IEnumerable<ByteGridEntry>
    {
        /// <summary>
        /// Values for all Tiles
        /// </summary>
        private byte[,] gridBytes;

        /// <summary>
        /// Dimension of ByteGrid
        /// </summary>
        public readonly int Width, Height;

        /// <summary>
        /// Dimension of ByteGrid as <see cref="IntVector2"/> 
        /// </summary>
        /// <returns></returns>
        public IntVector2 Size => new(Width, Height);

        public ByteGrid(int width, int height, byte defaultValue = 0)
        {
            Width = width;
            Height = height;
            gridBytes = new byte[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Height; x++)
                {
                    gridBytes[x, y] = defaultValue;
                }
            }
        }

        public byte this[int x, int y] { get => gridBytes[x, y]; set => gridBytes[x, y] = value; }
        public byte this[IntVector2 v] { get => gridBytes[v.X, v.Y]; set => gridBytes[v.X, v.Y] = value; }

        IEnumerator<ByteGridEntry> IEnumerable<ByteGridEntry>.GetEnumerator() => new ByteGridEnumerator(this);

        public IEnumerator GetEnumerator() => new ByteGridEnumerator(this);
    }
}