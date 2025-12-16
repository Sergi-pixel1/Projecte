using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    [Header("Tamaño del tablero")]
    public int width = 8;
    public int height = 8;

    [Header("Prefabs y colores")]
    public GameObject piecePrefab;
    public Color[] colores;

    private Piece[,] grid;

    void Start()
    {
        grid = new Piece[width, height];
        InicializarTablero();
    }

    // --------------------------------------------------------------------
    // CREAR TABLERO
    // --------------------------------------------------------------------
    void InicializarTablero()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                CrearPieza(x, y);
            }
        }
    }

    void CrearPieza(int x, int y)
    {
        GameObject go = Instantiate(piecePrefab, new Vector3(x, y, 0), Quaternion.identity);
        go.transform.SetParent(this.transform);

        Piece p = go.GetComponent<Piece>();
        Color c = colores[Random.Range(0, colores.Length)];
        p.Setup(x, y, c);

        grid[x, y] = p;
    }

    // --------------------------------------------------------------------
    // ELIMINAR PIEZA CON CLICK
    // --------------------------------------------------------------------
    public void EliminarPieza(int x, int y)
    {
        if (grid[x, y] != null)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;

            BajarPiezas();
        }
    }

    // --------------------------------------------------------------------
    // DETECTAR COINCIDENCIAS Y SCORE
    // --------------------------------------------------------------------
    public void RevisarTablero()
    {
        List<Piece> eliminar = BuscarCoincidencias();

        if (eliminar.Count > 0)
        {
            // Sumar puntos y combo
            ScoreManager.instance.AgregarPuntos(eliminar.Count * 10);
            ScoreManager.instance.SubirCombo();

            foreach (Piece p in eliminar)
            {
                grid[p.x, p.y] = null;
                Destroy(p.gameObject);
            }

            BajarPiezas();
        }
        else
        {
            // Resetear combo si no hay coincidencias
            ScoreManager.instance.ResetCombo();
        }
    }

    List<Piece> BuscarCoincidencias()
    {
        List<Piece> lista = new List<Piece>();

        // Horizontal
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width - 2; x++)
            {
                Piece p1 = grid[x, y];
                Piece p2 = grid[x + 1, y];
                Piece p3 = grid[x + 2, y];

                if (p1 != null && p2 != null && p3 != null &&
                    p1.color == p2.color && p2.color == p3.color)
                {
                    lista.Add(p1);
                    lista.Add(p2);
                    lista.Add(p3);
                }
            }
        }

        // Vertical
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height - 2; y++)
            {
                Piece p1 = grid[x, y];
                Piece p2 = grid[x, y + 1];
                Piece p3 = grid[x, y + 2];

                if (p1 != null && p2 != null && p3 != null &&
                    p1.color == p2.color && p2.color == p3.color)
                {
                    lista.Add(p1);
                    lista.Add(p2);
                    lista.Add(p3);
                }
            }
        }

        return lista;
    }

    // --------------------------------------------------------------------
    // BAJAR PIEZAS + RELLENAR
    // --------------------------------------------------------------------
    void BajarPiezas()
    {
        for (int x = 0; x < width; x++)
        {
            int posLibre = 0;

            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null)
                {
                    if (y != posLibre)
                    {
                        grid[x, posLibre] = grid[x, y];
                        grid[x, posLibre].y = posLibre;
                        grid[x, posLibre].transform.position = new Vector3(x, posLibre, 0);

                        grid[x, y] = null;
                    }
                    posLibre++;
                }
            }

            // Crear nuevas piezas encima
            for (int y = posLibre; y < height; y++)
            {
                CrearPieza(x, y);
            }
        }

        // Revisar coincidencias nuevamente (cascada)
        RevisarTablero();
    }
}




