using UnityEngine;
using System.Collections.Generic;

public class HexGridManager : MonoBehaviour
{
    public Vector2Int size = new Vector2Int(10, 10); // q width, r height
    public GameObject tilePrefab;
    public HexTileData defaultData;
    public float hexSize = 1f; // radio del hex (pointy-top)

    private HexTile[,] tiles;

    void Awake()
    {
        Generate();
    }

    public void Generate()
    {
        // Limpia
        foreach (Transform child in transform) Destroy(child.gameObject);

        tiles = new HexTile[size.x, size.y];
        for (int q = 0; q < size.x; q++)
        {
            for (int r = 0; r < size.y; r++)
            {
                Vector3 world = AxialToWorld(q, r, hexSize);
                var go = Instantiate(tilePrefab, world, Quaternion.identity, transform);
                var tile = go.GetComponent<HexTile>();
                tile.axial = new Vector2Int(q, r);
                tile.data = defaultData;
                // Asigna sprite si el prefab tiene SpriteRenderer
                var sr = go.GetComponent<SpriteRenderer>();
                if (sr && defaultData && defaultData.sprite) sr.sprite = defaultData.sprite;
                tiles[q, r] = tile;
            }
        }
    }

    public bool InBounds(Vector2Int a) => a.x >= 0 && a.y >= 0 && a.x < size.x && a.y < size.y;
    public HexTile GetTile(Vector2Int a) => tiles[a.x, a.y];

    public static Vector3 AxialToWorld(int q, int r, float size)
    {
        float x = size * (Mathf.Sqrt(3f) * q + Mathf.Sqrt(3f) / 2f * r);
        float y = size * (3f / 2f * r);
        return new Vector3(x, y, 0);
    }

    public static Vector2Int WorldToAxial(Vector3 world, float size)
    {
        float qf = (Mathf.Sqrt(3f) / 3f * world.x - 1f / 3f * world.y) / size;
        float rf = (2f / 3f * world.y) / size;
        return CubeRound(qf, rf);
    }

    static Vector2Int CubeRound(float q, float r)
    {
        float x = q;
        float z = r;
        float y = -x - z;
        int rx = Mathf.RoundToInt(x);
        int ry = Mathf.RoundToInt(y);
        int rz = Mathf.RoundToInt(z);

        float x_diff = Mathf.Abs(rx - x);
        float y_diff = Mathf.Abs(ry - y);
        float z_diff = Mathf.Abs(rz - z);

        if (x_diff > y_diff && x_diff > z_diff) rx = -ry - rz;
        else if (y_diff > z_diff) ry = -rx - rz;
        else rz = -rx - ry;

        return new Vector2Int(rx, rz);
    }

    public static readonly Vector2Int[] AxialDirs = new Vector2Int[] {
        new Vector2Int(+1, 0), new Vector2Int(+1, -1), new Vector2Int(0, -1),
        new Vector2Int(-1, 0), new Vector2Int(-1, +1), new Vector2Int(0, +1)
    };

    public IEnumerable<Vector2Int> Neighbors(Vector2Int a)
    {
        foreach (var d in AxialDirs)
        {
            var n = a + d;
            if (InBounds(n)) yield return n;
        }
    }
}


