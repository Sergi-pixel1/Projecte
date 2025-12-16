using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class UnitMovementHex : MonoBehaviour
{
    public enum HexOrientation { FlatTop_OddQ, PointedTop_OddR }

    [Header("Asignaciones")]
    public Tilemap tilemap;
    public HexOrientation orientation = HexOrientation.FlatTop_OddQ;

    [Header("Movimiento")]
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedCell = tilemap.WorldToCell(mouseWorldPos);

            Vector3Int currentCell = tilemap.WorldToCell(transform.position);

            // Limitar a vecinos
            var neighbors = GetNeighbors(currentCell);
            if (Contains(neighbors, clickedCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(clickedCell);
                isMoving = true;
            }
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    // Comparación de celdas (evita problemas de referencia con List.Contains en structs)
    bool Contains(List<Vector3Int> list, Vector3Int cell)
    {
        for (int i = 0; i < list.Count; i++)
            if (list[i].x == cell.x && list[i].y == cell.y)
                return true;
        return false;
    }

    List<Vector3Int> GetNeighbors(Vector3Int cell)
    {
        switch (orientation)
        {
            case HexOrientation.PointedTop_OddR:
                return OddRNeighbors(cell);
            case HexOrientation.FlatTop_OddQ:
            default:
                return OddQNeighbors(cell);
        }
    }

    // Pointed Top (odd-r): paridad por fila (y)
    List<Vector3Int> OddRNeighbors(Vector3Int cell)
    {
        int x = cell.x;
        int y = cell.y;
        bool isOdd = (y & 1) == 1;

        if (isOdd)
        {
            return new List<Vector3Int>
            {
                new Vector3Int(x+1, y, 0),   // E
                new Vector3Int(x-1, y, 0),   // W
                new Vector3Int(x,   y+1, 0), // NW
                new Vector3Int(x+1, y+1, 0), // NE
                new Vector3Int(x,   y-1, 0), // SW
                new Vector3Int(x+1, y-1, 0)  // SE
            };
        }
        else
        {
            return new List<Vector3Int>
            {
                new Vector3Int(x+1, y, 0),   // E
                new Vector3Int(x-1, y, 0),   // W
                new Vector3Int(x-1, y+1, 0), // NW
                new Vector3Int(x,   y+1, 0), // NE
                new Vector3Int(x-1, y-1, 0), // SW
                new Vector3Int(x,   y-1, 0)  // SE
            };
        }
    }

    // Flat Top (odd-q): paridad por columna (x)
    List<Vector3Int> OddQNeighbors(Vector3Int cell)
    {
        int x = cell.x;
        int y = cell.y;
        bool isOdd = (x & 1) == 1;

        if (isOdd)
        {
            return new List<Vector3Int>
            {
                new Vector3Int(x+1, y,   0), // NE
                new Vector3Int(x+1, y-1, 0), // SE
                new Vector3Int(x-1, y,   0), // NW
                new Vector3Int(x-1, y-1, 0), // SW
                new Vector3Int(x,   y+1, 0), // N
                new Vector3Int(x,   y-1, 0)  // S
            };
        }
        else
        {
            return new List<Vector3Int>
            {
                new Vector3Int(x+1, y+1, 0), // NE
                new Vector3Int(x+1, y,   0), // SE
                new Vector3Int(x-1, y+1, 0), // NW
                new Vector3Int(x-1, y,   0), // SW
                new Vector3Int(x,   y+1, 0), // N
                new Vector3Int(x,   y-1, 0)  // S
            };
        }
    }
}
