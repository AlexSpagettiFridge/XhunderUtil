using Godot;
using System;

namespace XhunderUtil
{
    public struct IntVector2
    {
        public int x, y;

        public int Size => x * y;

        public IntVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public IntVector2(Vector2 vector)
        {
            x = (int)Math.Floor(vector.x);
            y = (int)Math.Floor(vector.x);
        }

        public override string ToString() => "IntVector2(" + x + "," + y + ")";

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
                   x == vector.x &&
                   y == vector.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(IntVector2 a, IntVector2 b)
        {
            return (a.x == b.x && a.y == b.y);
        }

        public static bool operator !=(IntVector2 a, IntVector2 b)
        {
            return (a.x != b.x || a.y != b.y);
        }

        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
        {
            IntVector2 c = a;
            c.x += b.x;
            c.y += b.y;
            return c;
        }

        public static IntVector2 operator +(IntVector2 a, Vector2 b)
        {
            return a + new IntVector2(b);
        }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
        {
            IntVector2 c = a;
            c.x -= b.x;
            c.y -= b.y;
            return c;
        }

        public static IntVector2 operator -(IntVector2 a, Vector2 b)
        {
            return a - new IntVector2(b);
        }

        public static IntVector2 operator *(IntVector2 a, IntVector2 b)
        {
            IntVector2 c = a;
            c.x *= b.x;
            c.y *= b.y;
            return c;
        }

        public static IntVector2 operator *(IntVector2 a, Vector2 b)
        {
            return a * new IntVector2(b);
        }

        public static IntVector2 operator /(IntVector2 a, IntVector2 b)
        {
            IntVector2 c = a;
            c.x /= b.x;
            c.y /= b.y;
            return c;
        }

        public static IntVector2 operator /(IntVector2 a, Vector2 b)
        {
            return a / new IntVector2(b);
        }

        public static explicit operator Vector2(IntVector2 a)
        {
            return new Vector2(a.x, a.y);
        }

        public static explicit operator IntVector2(Vector2 a)
        {
            return new IntVector2(a);
        }

        public static IntVector2 operator *(IntVector2 a, int b)
        {
            IntVector2 c = a;
            c.x *= b;
            c.y *= b;
            return c;
        }

        public static IntVector2 operator *(IntVector2 a, float b)
        {
            IntVector2 c = a;
            c.x = (int)Math.Round(b * c.x);
            c.y = (int)Math.Round(b * c.y);
            return c;
        }

        public static IntVector2 operator /(IntVector2 a, int b)
        {
            IntVector2 c = a;
            c.x *= b;
            c.y *= b;
            return c;
        }

        public static IntVector2 operator /(IntVector2 a, float b)
        {
            IntVector2 c = a;
            c.x = (int)Math.Round(b * c.x);
            c.y = (int)Math.Round(b * c.y);
            return c;
        }
    }
}
