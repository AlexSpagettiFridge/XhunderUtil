using Godot;
using System;

namespace XhunderUtil
{
    public struct IntVector2
    {
        public int X, Y;

        public int Size => X * Y;

        public IntVector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public IntVector2(Vector2 vector)
        {
            X = (int)Math.Floor(vector.X);
            Y = (int)Math.Floor(vector.Y);
        }

        public override string ToString() => "IntVector2(" + X + "," + Y + ")";

        public IntVector2[] GetNeighbours(bool diagonal = false)
        {
            IntVector2[] neighbours;
            if (diagonal)
            {
                neighbours = new IntVector2[8];
            }
            else
            {
                neighbours = new IntVector2[4];
            }
            neighbours[0] = this + new IntVector2(0, -1);
            neighbours[1] = this + new IntVector2(1, 0);
            neighbours[2] = this + new IntVector2(0, 1);
            neighbours[3] = this + new IntVector2(-1, 0);
            if (diagonal)
            {
                neighbours[4] = this + new IntVector2(1, 1);
                neighbours[5] = this + new IntVector2(-1, 1);
                neighbours[6] = this + new IntVector2(-1, -1);
                neighbours[7] = this + new IntVector2(1, -1);
            }
            return neighbours;
        }

        public override bool Equals(object obj)
        {
            return obj is IntVector2 vector &&
                   X == vector.X &&
                   Y == vector.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        #region Operations
        public static bool operator ==(IntVector2 a, IntVector2 b)
        {
            return (a.X == b.X && a.Y == b.Y);
        }

        public static bool operator !=(IntVector2 a, IntVector2 b)
        {
            return (a.X != b.X || a.Y != b.Y);
        }

        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
        {
            IntVector2 c = a;
            c.X += b.X;
            c.Y += b.Y;
            return c;
        }

        public static IntVector2 operator +(IntVector2 a, Vector2 b)
        {
            return a + new IntVector2(b);
        }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
        {
            IntVector2 c = a;
            c.X -= b.X;
            c.Y -= b.Y;
            return c;
        }

        public static IntVector2 operator -(IntVector2 a, Vector2 b)
        {
            return a - new IntVector2(b);
        }

        public static IntVector2 operator *(IntVector2 a, IntVector2 b)
        {
            IntVector2 c = a;
            c.X *= b.X;
            c.Y *= b.Y;
            return c;
        }

        public static IntVector2 operator *(IntVector2 a, Vector2 b)
        {
            return a * new IntVector2(b);
        }

        public static IntVector2 operator /(IntVector2 a, IntVector2 b)
        {
            IntVector2 c = a;
            c.X /= b.X;
            c.Y /= b.Y;
            return c;
        }

        public static IntVector2 operator /(IntVector2 a, Vector2 b)
        {
            return a / new IntVector2(b);
        }

        public static explicit operator Vector2(IntVector2 a)
        {
            return new Vector2(a.X, a.Y);
        }

        public static explicit operator IntVector2(Vector2 a)
        {
            return new IntVector2(a);
        }

        public static IntVector2 operator *(IntVector2 a, int b)
        {
            IntVector2 c = a;
            c.X *= b;
            c.Y *= b;
            return c;
        }

        public static IntVector2 operator *(IntVector2 a, float b)
        {
            IntVector2 c = a;
            c.X = (int)Math.Round(b * c.X);
            c.Y = (int)Math.Round(b * c.Y);
            return c;
        }

        public static IntVector2 operator /(IntVector2 a, int b)
        {
            IntVector2 c = a;
            c.X *= b;
            c.Y *= b;
            return c;
        }

        public static IntVector2 operator /(IntVector2 a, float b)
        {
            IntVector2 c = a;
            c.X = (int)Math.Round(b * c.X);
            c.Y = (int)Math.Round(b * c.Y);
            return c;
        }

        public static bool operator <(IntVector2 a, IntVector2 b) => (a.X < b.X || a.Y < b.Y);

        public static bool operator >(IntVector2 a, IntVector2 b) => (a.X > b.X && a.X > b.Y);
        #endregion
    }
}
