using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3Int currentCell;

    private Vector3 targetWorldPos;
    private bool isMoving = false;

    private GridController grid;

    void Start()
    {
        grid = FindObjectOfType<GridController>();
        currentCell = grid.WorldToCell(transform.position);
        transform.position = grid.CellToWorld(currentCell);
        targetWorldPos = transform.position;
    }

    void Update()
    {
        HandleInput();
        Move();
    }

    void HandleInput()
    {
        if (isMoving) return;

        Vector3Int direction = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow)) direction = new Vector3Int(0, 1, 0);
        if (Input.GetKeyDown(KeyCode.DownArrow)) direction = new Vector3Int(0, -1, 0);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) direction = new Vector3Int(-1, 0, 0);
        if (Input.GetKeyDown(KeyCode.RightArrow)) direction = new Vector3Int(1, 0, 0);

        if (direction != Vector3Int.zero)
        {
            Vector3Int nextCell = currentCell + direction;

            if (grid.IsWalkable(nextCell))
            {
                currentCell = nextCell;
                targetWorldPos = grid.CellToWorld(nextCell);
                isMoving = true;
            }
        }
    }

    void Move()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWorldPos) < 0.01f)
        {
            transform.position = targetWorldPos;
            isMoving = false;
        }
    }
}

