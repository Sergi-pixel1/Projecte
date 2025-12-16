using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap groundTilemap;  // Tilemap del suelo
    public Tilemap obstacleTilemap; // Tilemap con obstáculos opcionales

    public bool IsWalkable(Vector3Int cellPos)
    {
        // Si no hay tile de suelo ? no caminable
        if (!groundTilemap.HasTile(cellPos))
            return false;

        // Si hay un obstáculo ? no caminable
        if (obstacleTilemap != null && obstacleTilemap.HasTile(cellPos))
            return false;

        return true;
    }

    public Vector3 CellToWorld(Vector3Int cell)
    {
        return groundTilemap.GetCellCenterWorld(cell);
    }

    public Vector3Int WorldToCell(Vector3 world)
    {
        return groundTilemap.WorldToCell(world);
    }
}

