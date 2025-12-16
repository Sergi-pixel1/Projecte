using UnityEngine;
using System.Collections.Generic;

public class MovementRange : MonoBehaviour
{
    GridManager grid;

    private void Start()
    {
        grid = FindObjectOfType<GridManager>();
    }

    public List<Vector3Int> GetMovableTiles(Vector3Int origin, int range)
    {
        List<Vector3Int> result = new List<Vector3Int>();
        Queue<(Vector3Int cell, int dist)> frontier = new Queue<(Vector3Int, int)>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        frontier.Enqueue((origin, 0));
        visited.Add(origin);

        Vector3Int[] directions = {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, -1, 0)
        };

        while (frontier.Count > 0)
        {
            var (cell, dist) = frontier.Dequeue();
            result.Add(cell);

            if (dist >= range)
                continue;

            foreach (var dir in directions)
            {
                Vector3Int next = cell + dir;

                if (!visited.Contains(next) && grid.IsWalkable(next))
                {
                    visited.Add(next);
                    frontier.Enqueue((next, dist + 1));
                }
            }
        }

        return result;
    }
}

