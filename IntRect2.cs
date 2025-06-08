using System;
using System.Collections;
using System.Collections.Generic;
using Godot;

namespace XhunderUtil
{
    /// <summary>
    /// Godot <see cref="Rect2"/> but with integers instead of floats. 
    /// </summary>
    public class IntRect2 : IEnumerable<IntVector2>
    {
        #region Variables and Constructors
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
        #endregion

        #region overrides
        public IEnumerator<IntVector2> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Mathesque Functions
        public IEnumerable<IntVector2> GetPixelsInsideEllipse()
        {
            //Bresenham midpoint circle algorithm modified for ellipses
            float verticalStretch = Size.Y / Size.X;
            int xradius = (int)Math.Ceiling(Size.X / 2f);
            bool odd = xradius != Size.X;
            bool oddY = Math.Floor(Size.Y / 2f) != Size.Y / 2;

            for (int x = odd ? 0 : 1; x < xradius; x++)
            {
                int verticalLength = (int)Math.Round(xradius * verticalStretch - x);
                for (int y = oddY ? 0 : 1; y < verticalLength; y++)
                {
                    yield return new(x, y);
                    if (y != 0) { yield return new(x, -y); }
                    if (x != 0)
                    {
                        yield return new(-x, y);
                        if (y != 0) { yield return new(-x, -y); }
                    }
                }
            }
        }
        #endregion
    }
}