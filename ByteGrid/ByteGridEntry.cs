using Godot;

namespace XhunderUtil.ByteGrid
{
    /// <summary>
    /// Information struct that gets generated when iterating through <see cref="ByteGrid"/>.
    /// </summary>
    public readonly struct ByteGridEntry
    {
        /// <summary>
        /// Related ByteGrid
        /// </summary>
        public readonly ByteGrid Grid;

        /// <summary>
        /// Position inside ByteGrid
        /// </summary>
        public readonly IntVector2 Position;

        /// <summary>
        /// <see langword="byte">Byte</see> value
        /// </summary>
        public readonly byte Value => Grid[Position];

        /// <summary>
        /// Get Tile Rect in a Tilemap with given <paramref name="size"/>
        /// </summary>
        /// <param name="size">Size of theoretical Tilemap</param>
        /// <returns></returns>
        public readonly Rect2 GetRect(int size) => new(new Vector2(X, Y) * size, Vector2.One * size);

        /// <summary>
        /// X coordinate inside ByteGrid
        /// </summary>
        public int X => Position.X;

        /// <summary>
        /// Y coordinate inside ByteGrid
        /// </summary>
        public int Y => Position.Y;

        public ByteGridEntry(ByteGrid grid, int x, int y)
        {
            Grid = grid;
            Position.X = x;
            Position.Y = y;
        }

        /// <summary>
        /// Sets value spezified by this class.
        /// </summary>
        /// <param name="value"></param> <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(byte value)
        {
            Grid[Position] = value;
        }
    }
}