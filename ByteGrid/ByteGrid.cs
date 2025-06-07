using System.Collections;
using System.Collections.Generic;
using Godot;

namespace XhunderUtil.ByteGrid
{
    public class ByteGrid : IEnumerable<ByteGridEntry>
    {
        public byte[,] GridBytes;
        public readonly int Width, Height;
        public IntVector2 Size => new(Width, Height);

        public ByteGrid(int width, int height, byte defaultValue = 0)
        {
            Width = width;
            Height = height;
            GridBytes = new byte[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Height; x++)
                {
                    GridBytes[x, y] = defaultValue;
                }
            }
        }

        public byte this[int x, int y] => GridBytes[x, y];
        public byte this[IntVector2 v] => GridBytes[v.X, v.Y];

        IEnumerator<ByteGridEntry> IEnumerable<ByteGridEntry>.GetEnumerator() => new ByteGridEnumerator(this);

        public IEnumerator GetEnumerator() => new ByteGridEnumerator(this);
    }
}