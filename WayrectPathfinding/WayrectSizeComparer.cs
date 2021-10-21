using System;
using System.Collections.Generic;

namespace XhunderUtil.WayrectPathfinding
{
    public class WayrectSizeComparer : IComparer<Wayrect>
    {
        public int Compare(Wayrect x, Wayrect y)
        {
            return Math.Sign(x.Size - y.Size);
        }
    }
}