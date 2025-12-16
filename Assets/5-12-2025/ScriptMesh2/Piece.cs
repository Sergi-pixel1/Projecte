using UnityEngine;

public class Piece : MonoBehaviour
{
    public int x;
    public int y;
    public Color color;

    private SpriteRenderer sprite;

    public void Setup(int x, int y, Color color)
    {
        this.x = x;
        this.y = y;
        this.color = color;

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    private void OnMouseDown()
    {
        Board board = FindAnyObjectByType<Board>();

        if (board == null)
        {
            Debug.LogError("No se encontró el Board en la escena.");
            return;
        }

        board.EliminarPieza(x, y);
    }
}



