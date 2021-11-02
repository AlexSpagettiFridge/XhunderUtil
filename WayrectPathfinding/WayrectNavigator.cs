using System.Collections.Generic;
using Godot;
using System;

namespace XhunderUtil.WayrectPathfinding
{
    /// <summary>
    /// The main way to interact with the WayrectPathfinding system.
    /// </summary>
    public class WayrectNavigator
    {
        private List<Wayrect> wayrects = new List<Wayrect>();
        private Queue<Wayrect> freedWayrects = new Queue<Wayrect>();
        private int gridSize;

        public WayrectNavigator(int gridSize, byte[,] mapData)
        {
            this.gridSize = gridSize;
            for (int x = 0; x < mapData.GetLength(0); x++)
            {
                for (int y = 0; y < mapData.GetLength(1); y++)
                {
                    if (mapData[x, y] == 0)
                    {
                        Wayrect nn = new Wayrect(new IntVector2(x, y), new IntVector2(x + 1, y + 1));
                        nn.removal += OnWaypointRemoved;
                        wayrects.Add(nn);
                    }
                }
            }
            foreach (Wayrect waypoint in wayrects)
            {
                waypoint.TryFindNeighbours(wayrects);
            }
            bool nonew = false;
            while (!nonew)
            {
                nonew = true;
                foreach (Wayrect waypoint in wayrects)
                {
                    if (waypoint.TryJoinNeighbour())
                    {
                        nonew = false;
                        break;
                    }
                }
                FreeQueuedWaypoints();
            }
        }

        /// <summary>
        /// Returns the way rect at the given <paramref name="point">.
        /// <param name="point">The point at which a Wayrect should be searched.</param>
        /// <returns>Returns the Wayrect at the <paramref name="point">. When there isn't one returns null.</returns>
        /// </summary>
        public Wayrect GetWayrectAtPoint(Vector2 point)
        {
            point /= gridSize;
            foreach (Wayrect waypoint in wayrects)
            {
                if (point.x > waypoint.start.x && point.x < waypoint.end.x && point.y > waypoint.start.y && point.y < waypoint.end.y)
                {
                    return waypoint;
                }
            }
            return null;
        }

        /// <summary>
        /// Calculates the fastest path from <paramref name="startPoint"> to <paramref name="endPoint">.
        /// <param name="startPoint">The point the path should start on or the current position of the navigator</param>
        /// <param name="endpoint">The Goal the path should reach</param>
        /// <param name="agentWidth">The radius width of the navigator. The path will hold this distance from walls.</param>
        /// <returns>Returns a path as in a collection of points.</returns>
        /// </summary>
        public List<Vector2> CalculatePath(Vector2 startPoint, Vector2 endPoint, float agentWidth = 0)
        {
            Wayrect startRect = GetWayrectAtPoint(startPoint);
            Wayrect endRect = GetWayrectAtPoint(endPoint);
            List<Wayrect> visitedWayrects = new List<Wayrect>();
            List<TheoreticalPath> paths = new List<TheoreticalPath>();
            TheoreticalPath? validPath = null;
            float maxDistance = Mathf.Inf;
            paths.Add(new TheoreticalPath(startRect, startPoint / gridSize));

            if (startRect == endRect)
            {
                List<Vector2> path = new List<Vector2>();
                path.Add(endPoint);
                return path;
            }
            while (paths.Count > 0)
            {
                List<TheoreticalPath> newPathList = new List<TheoreticalPath>();
                foreach (TheoreticalPath tPath in paths)
                {
                    foreach (Wayrect nb in tPath.LatestWayrect.neighbours)
                    {
                        if (visitedWayrects.Contains(nb))
                        {
                            continue;
                        }
                        TheoreticalPath newPath = tPath.Copy();
                        newPath.AddWaypoint(nb, agentWidth / gridSize);
                        if (newPath.distance > maxDistance)
                        {
                            continue;
                        }
                        if (nb == endRect)
                        {
                            validPath = newPath;
                            maxDistance = newPath.distance;
                        }
                        newPathList.Add(newPath);
                    }
                    visitedWayrects.Add(tPath.LatestWayrect);
                }
                paths = newPathList;
            }


            if (validPath != null)
            {
                List<Vector2> path = new List<Vector2>();
                foreach (Vector2 point in ((TheoreticalPath)validPath).points)
                {
                    if (point * gridSize != startPoint)
                    {
                        path.Add(point * gridSize);
                    }
                }
                path.Add(endPoint);
                return path;
            }
            return null;
        }

        private void OnWaypointRemoved(object sender, EventArgs args)
        {
            freedWayrects.Enqueue((Wayrect)sender);
        }

        private void FreeQueuedWaypoints()
        {
            while (freedWayrects.Count > 0)
            {
                wayrects.Remove(freedWayrects.Dequeue());
            }
        }
    }
}