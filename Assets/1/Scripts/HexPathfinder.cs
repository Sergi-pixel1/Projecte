using UnityEngine;
using System.Collections.Generic;

public static class HexPathfinder
{
    public static HashSet<Vector2Int> MovementRange(HexGridManager grid, Vector2Int start, int movePoints)
    {
        var visited = new HashSet<Vector2Int> { start };
        var frontier = new Queue<(Vector2Int, int)>();
        frontier.Enqueue((start, movePoints));

        while (frontier.Count > 0)
        {
            var (pos, mp) = frontier.Dequeue();
            foreach (var n in grid.Neighbors(pos))
            {
                var tile = grid.GetTile(n);
                int cost = tile.data ? tile.data.moveCost : 1;
                if (mp - cost < 0) continue;
                if (tile.data && tile.data.blocksMovement) continue;
                if (tile.occupied) continue; // evita atravesar ocupados (ajustable)
                if (visited.Contains(n)) continue;
                visited.Add(n);
                frontier.Enqueue((n, mp - cost));
            }
        }
        visited.Remove(start);
        return visited;
    }
}

