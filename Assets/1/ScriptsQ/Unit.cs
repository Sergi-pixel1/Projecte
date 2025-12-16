using UnityEngine;

public class Unit : MonoBehaviour
{
    public int moveRange = 5;
    public Vector3Int currentCell;
    GridManager grid;

    private void Start()
    {
        grid = FindObjectOfType<GridManager>();
        currentCell = grid.WorldToCell(transform.position);
    }

    public void MoveTo(Vector3Int targetCell)
    {
        currentCell = targetCell;
        transform.position = grid.CellToWorld(targetCell);
    }
}

