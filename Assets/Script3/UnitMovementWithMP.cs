using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class UnitMovementWithMP : MonoBehaviour
{
    [Header("Tilemap y movimiento")]
    public Tilemap tilemap;
    public float moveSpeed = 5f;

    [Header("Puntos de movimiento")]
    public int maxMovementPoints = 5;   // Puntos máximos por turno
    private int currentMovementPoints;

    [Header("Estado de la unidad")]
    public bool isActive = true;        // Controla si la unidad acepta input

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        // Al inicio, la unidad empieza con todos sus puntos
        currentMovementPoints = maxMovementPoints;
    }

    void Update()
    {
        if (!isActive) return; // Bloquea input si no está activa

        if (Input.GetMouseButtonDown(0) && currentMovementPoints > 0)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedCell = tilemap.WorldToCell(mouseWorldPos);

            Vector3Int currentCell = tilemap.WorldToCell(transform.position);
            List<Vector3Int> neighbors = GetNeighbors(currentCell);

            if (Contains(neighbors, clickedCell))
            {
                int cost = GetMovementCost(clickedCell);

                // Solo mover si hay suficientes puntos
                if (currentMovementPoints >= cost)
                {
                    targetPosition = tilemap.GetCellCenterWorld(clickedCell);
                    isMoving = true;
                    currentMovementPoints -= cost; // Restar coste
                    Debug.Log($"{name} movida. MP restantes: {currentMovementPoints}");
                }
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

    // Comparación de celdas (ignora z)
    bool Contains(List<Vector3Int> list, Vector3Int cell)
    {
        foreach (var c in list)
        {
            if (c.x == cell.x && c.y == cell.y) return true;
        }
        return false;
    }

    // Vecinos básicos (Flat Top, ajusta si usas Pointed Top)
    List<Vector3Int> GetNeighbors(Vector3Int cell)
    {
        return new List<Vector3Int>
        {
            cell + new Vector3Int(1, 0, 0),
            cell + new Vector3Int(-1, 0, 0),
            cell + new Vector3Int(0, 1, 0),
            cell + new Vector3Int(0, -1, 0),
            cell + new Vector3Int(1, -1, 0),
            cell + new Vector3Int(-1, 1, 0)
        };
    }

    // Coste de movimiento según el terreno
    int GetMovementCost(Vector3Int cell)
    {
        TileBase tile = tilemap.GetTile(cell);

        if (tile == null) return 999; // Casilla vacía = imposible

        // Ejemplo: asignar costes según nombre del tile
        if (tile.name.Contains("Forest")) return 2;
        if (tile.name.Contains("Mountain")) return 3;
        return 1; // Llanura por defecto
    }

    // Reiniciar puntos al inicio de cada turno
    public void NewTurn()
    {
        currentMovementPoints = maxMovementPoints;
        Debug.Log($"{name} reinicia turno. MP restaurados: {currentMovementPoints}");
    }

    // Activar/desactivar input
    public void SetActive(bool active)
    {
        isActive = active;
    }
}

