using Godot;

namespace XhunderUtil
{
    /// <summary>
    /// Godot <see cref="Rect2"/> but with integers instead of floats. 
    /// </summary>
    public class IntRect2
    {
        public IntVector2 Position, Size;
        public IntVector2 Start
        {
            get => Position;
            set
            {
                IntVector2 previousEnd = End;
                Position = value;
                End = previousEnd;
            }
        }
        public IntVector2 End
        {
            get => Position + Size;
            set => Size = value - Position;
        }

        public IntRect2(IntVector2 position, IntVector2 end)
        {
            Position = position;
            End = end;
        }
        public IntRect2(Rect2 floatRect)
        {
            Position = new IntVector2(floatRect.Position);
            End = new IntVector2(floatRect.End);
        }
    }
}