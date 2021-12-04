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
            while(node.GetTree().Root != node)
            {
                if (node is Control)
                {
                    Theme controlTheme = ((Control)node).Theme;
                    if (controlTheme != null)
                    {
                        return controlTheme;
                    }
                }
                node = node.GetParent();
            }
            return null;
        }
    }
}