using UnityEngine;

public class HexTile : MonoBehaviour
{
    public Vector2Int axial; // (q, r)
    public HexTileData data;
    public bool occupied;

    void OnValidate()
    {
        name = $"HexTile_{axial.x}_{axial.y}";
    }
}

