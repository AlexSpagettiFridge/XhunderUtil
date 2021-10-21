using System.Collections.Generic;
using Godot;

namespace XhunderUtil.WayrectPathfinding
{
    public struct TheoreticalPath
    {
        public float distance;
        public List<Wayrect> wayrects;
        public List<Vector2> points;

        public Wayrect LatestWayrect => wayrects[wayrects.Count - 1];

        public TheoreticalPath(Wayrect firstRect, Vector2 startPoint)
        {
            distance = 0;
            wayrects = new List<Wayrect>();
            points = new List<Vector2>();
            wayrects.Add(firstRect);
            points.Add(startPoint);
        }

        public void AddWaypoint(Wayrect rect, float agentWidth = 0)
        {
            Vector2 newPoint = LatestWayrect.NearestExitToNeighbour(points[points.Count - 1], rect, agentWidth);
            distance += points[points.Count - 1].DistanceTo(newPoint);
            wayrects.Add(rect);
            points.Add(newPoint);
        }

        public TheoreticalPath Copy()
        {
            TheoreticalPath copy = new TheoreticalPath();
            copy.wayrects = new List<Wayrect>();
            foreach (Wayrect rect in wayrects)
            {
                copy.wayrects.Add(rect);
            }
            copy.points = new List<Vector2>();
            foreach (Vector2 point in points)
            {
                copy.points.Add(point);
            }
            copy.distance = distance;
            return copy;
        }

        public override string ToString()
        {
            return $"tPath(d: {distance}, rC: {wayrects.Count})";
        }
    }
}