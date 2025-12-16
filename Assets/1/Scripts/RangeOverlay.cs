using UnityEngine;
using System.Collections.Generic;

public class RangeOverlay : MonoBehaviour
{
    public GameObject markerPrefab;
    private readonly List<GameObject> pool = new();

    public void Show(HashSet<Vector2Int> axialTiles, HexGridManager grid)
    {
        Clear();
        foreach (var a in axialTiles)
        {
            var pos = HexGridManager.AxialToWorld(a.x, a.y, grid.hexSize);
            var go = Instantiate(markerPrefab, pos, Quaternion.identity, transform);
            pool.Add(go);
        }
    }

    public void Clear()
    {
        foreach (var go in pool) Destroy(go);
        pool.Clear();
    }
}

