namespace XhunderUtil.ByteGrid
{
    /// <summary>
    /// Class for changing values inside a specific <see cref="ByteGrid"/>,
    /// with the help of different Shapes.
    /// </summary>
    public class ByteGridPainter
    {
        /// <summary>
        /// This class edits one <see cref="ByteGrid"/> . Is assigned in Constructor.
        /// </summary>
        private readonly ByteGrid painted;
        public ByteGridPainter(ByteGrid painted) => this.painted = painted;

        public void Reset(byte value = 0)
        {
            foreach (ByteGridEntry entry in painted)
            {
                entry.SetValue(value);
            }
        }

        public void PaintRect(byte value, IntRect2 rect)
        {
            rect.Start = rect.Start.Max(IntVector2.Zero);
            rect.End = rect.End.Min(painted.Size);
            foreach (IntVector2 vector in rect)
            {
                painted[vector] = value;
            }
        }
    }
}