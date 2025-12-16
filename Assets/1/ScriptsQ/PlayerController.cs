using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Tilemap highlightTilemap;
    public Tile highlightTile;

    Unit selectedUnit;
    GridManager grid;
    MovementRange movement;

    void Start()
    {
        grid = FindObjectOfType<GridManager>();
        movement = FindObjectOfType<MovementRange>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cell = grid.WorldToCell(worldPos);

            TrySelectUnit(cell);
            TryMoveUnit(cell);
        }
    }

    void TrySelectUnit(Vector3Int cell)
    {
        if (selectedUnit != null) return;

        Collider2D hit = Physics2D.OverlapPoint(grid.CellToWorld(cell));

        if (hit != null && hit.GetComponent<Unit>())
        {
            selectedUnit = hit.GetComponent<Unit>();
            HighlightMovementRange();
        }
    }

    void TryMoveUnit(Vector3Int cell)
    {
        if (selectedUnit == null) return;

        if (highlightTilemap.HasTile(cell))
        {
            selectedUnit.MoveTo(cell);
            ClearHighlights();
            selectedUnit = null;
        }
    }

    void HighlightMovementRange()
    {
        ClearHighlights();
        var tiles = movement.GetMovableTiles(selectedUnit.currentCell, selectedUnit.moveRange);

        foreach (var t in tiles)
            highlightTilemap.SetTile(t, highlightTile);
    }

    void ClearHighlights()
    {
        highlightTilemap.ClearAllTiles();
    }
}
