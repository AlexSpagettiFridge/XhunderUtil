using System;
using System.Collections;
using Godot;

namespace XhunderUtil
{
    public static class Util
    {
        /// <summary>
        /// Get the inherited Theme of an <paramref name="node"/>.
        /// </summary>
        /// <param name="node">The node for the search</param>
        /// <returns>Returns the <see cref="Godot.Theme"/> the <paramref name="node"/> inherited from its parent Nodes.</returns>
        public static Theme GetInheritedTheme(Node node)
        {
            while (node.GetTree().Root != node)
            {
                if (node is Control control)
                {
                    Theme controlTheme = control.Theme;
                    if (controlTheme != null)
                    {
                        return controlTheme;
                    }
                }
                node = node.GetParent();
            }
            return null;
        }
        
        public static double SnapValue(double value, int gridSize) => Math.Round(value / gridSize) * gridSize;
        public static float SnapValue(float value, int gridSize) => (float)Math.Round(value / gridSize) * gridSize;
        public static Vector2 SnapVector(Vector2 vector, int gridSize) => (vector / gridSize).Round() * gridSize;
        public static bool IsRound(float value) => value == Math.Round(value);
        public static int IntegerFromBitArrayRange(BitArray bitArray, int start, int lenght = -1)
        {
            if (lenght == -1)
            {
                lenght = bitArray.Length - start;
            }
            int result = 0;
            for (int i = 0; i < lenght; i++)
            {
                if (bitArray[start + i])
                {
                    result += (int)Math.Pow(2, i);
                }
            }
            return result;
        }
        public static void WriteValueToBitArrayRange(ref BitArray bitArray, int value, int start, int lenght = -1)
        {
            if (lenght == -1) { lenght = bitArray.Length - start; }
            BitArray copyMe = new(new int[] { value });
            for (int i = 0; i < lenght; i++)
            {
                bitArray.Set(start + i, copyMe.Get(i));
            }
        }
        public static string BitArrayToBitString(BitArray bitArray)
        {
            string result = "";
            foreach (bool bit in bitArray)
            {
                result += bit ? "1" : "0";
            }
            return result;
        }
    }
}