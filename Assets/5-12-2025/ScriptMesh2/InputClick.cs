using UnityEngine;

public class InputClick : MonoBehaviour
{
    public Board board;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                Piece p = hit.collider.GetComponent<Piece>();
                if (p != null)
                {
                    board.RevisarTablero();
                }
            }
        }
    }
}

