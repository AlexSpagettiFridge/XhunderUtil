using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Godot;

namespace XhunderUtil
{
    public class IntRect2Enumerator : IEnumerator<IntVector2>
    {
        private readonly IntRect2 rect;
        private int index = 0;

        public IntVector2 Current
        {
            get
            {
                int y = (int)Math.Floor((double)(index / rect.Size.X));
                int x = index - y * rect.Size.X;
                return new IntVector2(x, y) + rect.Position;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            index++;
            return index < rect.Size.Product();
        }

        public void Reset()
        {
            index = 0;
        }
    }
}