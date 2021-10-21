using System.Collections.Generic;
using System;
using Godot;

namespace XhunderUtil.WayrectPathfinding
{
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
            if (start.x == nb.end.x || nb.start.x == end.x)
            {
                int top = Math.Max(start.y, nb.start.y);
                int bottom = Math.Min(end.y, nb.end.y);
                int xExit = (start.x > nb.start.x) ? start.x : end.x;
                return new Vector2(xExit, Mathf.Clamp(entryPoint.y, top + agentWidth, bottom - agentWidth));
            }
            if (start.y == nb.end.y || nb.start.y == end.y)
            {
                int left = Math.Max(start.x, nb.start.x);
                int right = Math.Min(end.x, nb.end.x);
                int yExit = (start.y > nb.start.y) ? start.y : end.y;
                return new Vector2(Mathf.Clamp(entryPoint.x, left + agentWidth, right - agentWidth), yExit);
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
                if (neighbour.start.x == start.x && neighbour.end.x == end.x)
                {
                    neighbour.start.y = Math.Min(start.y, neighbour.start.y);
                    neighbour.end.y = Math.Max(end.y, neighbour.end.y);
                    Remove();
                    PassOnNeighbours(neighbour);
                    return true;
                }
                if (neighbour.start.y == start.y && neighbour.end.y == end.y)
                {
                    neighbour.start.x = Math.Min(start.x, neighbour.start.x);
                    neighbour.end.x = Math.Max(end.x, neighbour.end.x);
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
            if (start.x == other.end.x || end.x == other.start.x)
            {
                return !(start.y >= other.end.y || end.y <= other.start.y);
            }
            if (start.y == other.end.y || end.y == other.start.y)
            {
                return !(start.x >= other.end.x || end.x <= other.start.x);
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