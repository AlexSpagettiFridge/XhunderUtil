using System.Collections;
using System.Collections.Generic;
using Godot;

namespace XhunderUtil.ByteGrid
{
    public struct ByteGridEntry
    {
        public readonly ByteGrid Grid;
        public readonly int X, Y;
        public readonly byte Value;
        public readonly Rect2 GetRect(int size) => new(new Vector2(X, Y) * size, Vector2.One * size);

        public ByteGridEntry(ByteGrid grid, int x, int y, byte value)
        {
            Grid = grid;
            X = x;
            Y = y;
            Value = value;
        }
    }
}