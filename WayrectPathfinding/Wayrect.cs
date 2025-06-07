using System.Collections.Generic;
using System;
using Godot;

namespace XhunderUtil.WayrectPathfinding
{
    /// <summary>
    /// Wayrects are rectangles inside of a tilemap used by the WayrectNavigator to calculate paths.
    /// </summary>
    public class Wayrect
    {
        public IntVector2 start, end;
        public List<Wayrect> neighbours = new List<Wayrect>();
        public event EventHandler removal;
        private bool isFree = false;

        public int Size => (end - start).Size;

        public Wayrect(IntVector2 start, IntVector2 end)
        {
            this.start = start;
            this.end = end;
        }

        public void Remove()
        {
            isFree = true;
            removal.Invoke(this, null);
        }

        public Vector2 NearestExitToNeighbour(Vector2 entryPoint, Wayrect nb, float agentWidth = 0)
        {
            if (start.X == nb.end.X || nb.start.X == end.X)
            {
                int top = Math.Max(start.Y, nb.start.Y);
                int bottom = Math.Min(end.Y, nb.end.Y);
                int xExit = (start.X > nb.start.X) ? start.X : end.X;
                return new Vector2(xExit, Mathf.Clamp(entryPoint.Y, top + agentWidth, bottom - agentWidth));
            }
            if (start.Y == nb.end.Y || nb.start.Y == end.Y)
            {
                int left = Math.Max(start.X, nb.start.X);
                int right = Math.Min(end.X, nb.end.X);
                int yExit = (start.Y > nb.start.Y) ? start.Y : end.Y;
                return new Vector2(Mathf.Clamp(entryPoint.X, left + agentWidth, right - agentWidth), yExit);
            }
            throw new Exception($"No connection between {this.ToString()} & {nb.ToString()}");
        }

        public bool TryFindNeighbours(List<Wayrect> possibleNeighbours)
        {
            neighbours = new List<Wayrect>();
            foreach (Wayrect waypoint in possibleNeighbours)
            {
                if (waypoint == this) { continue; }
                if (IsConnected(waypoint)) { AddNeigbour(waypoint); }
            }
            return (neighbours.Count != 0);
        }

        public bool TryJoinNeighbour()
        {
            neighbours.Sort(new WayrectSizeComparer());
            foreach (Wayrect neighbour in neighbours)
            {
                if (neighbour.start.X == start.X && neighbour.end.X == end.X)
                {
                    neighbour.start.Y = Math.Min(start.Y, neighbour.start.Y);
                    neighbour.end.Y = Math.Max(end.Y, neighbour.end.Y);
                    Remove();
                    PassOnNeighbours(neighbour);
                    return true;
                }
                if (neighbour.start.Y == start.Y && neighbour.end.Y == end.Y)
                {
                    neighbour.start.X = Math.Min(start.X, neighbour.start.X);
                    neighbour.end.X = Math.Max(end.X, neighbour.end.X);
                    Remove();
                    PassOnNeighbours(neighbour);
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"WP({start},{end}";
        }

        public void AddNeigbour(Wayrect wayrect)
        {
            if (wayrect.isFree) { return; }
            neighbours.Add(wayrect);
            wayrect.removal += OnNeighbourRemoved;
        }

        public bool IsConnected(Wayrect other)
        {
            if (start.X == other.end.X || end.X == other.start.X)
            {
                return !(start.Y >= other.end.Y || end.Y <= other.start.Y);
            }
            if (start.Y == other.end.Y || end.Y == other.start.Y)
            {
                return !(start.X >= other.end.X || end.X <= other.start.X);
            }
            return false;
        }

        private void PassOnNeighbours(Wayrect other)
        {
            foreach (Wayrect nb in neighbours)
            {
                if (nb == other || !other.IsConnected(nb)) { continue; }

                if (!other.neighbours.Contains(nb))
                {
                    other.AddNeigbour(nb);
                }
                if (!nb.neighbours.Contains(other))
                {
                    nb.AddNeigbour(other);
                }
            }
        }

        private void OnNeighbourRemoved(object sender, EventArgs args)
        {
            Wayrect removedWaypoint = (Wayrect)sender;
            neighbours.Remove(removedWaypoint);
        }
    }
}