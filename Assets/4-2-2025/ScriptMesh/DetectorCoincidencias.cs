using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectorCoincidencias : MonoBehaviour
{
    public static DetectorCoincidencias instancia;

    [SerializeField] private Tablero tablero;
    private Color colorObjetivo; // Actualizado dinámicamente según la pieza clickeada

    private void Awake()
    {
        instancia = this;
    }

    private bool EsColorObjetivo(GameObject pieza)
    {
        if (pieza == null) return false;
        return pieza.GetComponent<SpriteRenderer>().color == colorObjetivo;
    }

    // Llamado desde Pieza.cs
    public void BuscarYActualizar(Color color)
    {
        colorObjetivo = color;
        tablero.ActualizarTablero();
        BuscarCoincidencias();
    }

    public void BuscarCoincidencias()
    {
        GameObject[,] piezas = tablero.GetPiezasColores();
        int ancho = piezas.GetLength(0);
        int alto = piezas.GetLength(1);

        List<GameObject> piezasAEliminar = new List<GameObject>();

        // --- LINEAS HORIZONTALES ---
        for (int y = 0; y < alto; y++)
        {
            int x = 0;
            while (x < ancho - 2)
            {
                GameObject piezaBase = piezas[x, y];
                if (!EsColorObjetivo(piezaBase)) { x++; continue; }

                List<GameObject> match = new List<GameObject> { piezaBase };
                int x2 = x + 1;

                while (x2 < ancho && EsColorObjetivo(piezas[x2, y]))
                {
                    match.Add(piezas[x2, y]);
                    x2++;
                }

                if (match.Count >= 3) piezasAEliminar.AddRange(match);
                x = x2;
            }
        }

        // --- LINEAS VERTICALES ---
        for (int x = 0; x < ancho; x++)
        {
            int y = 0;
            while (y < alto - 2)
            {
                GameObject piezaBase = piezas[x, y];
                if (!EsColorObjetivo(piezaBase)) { y++; continue; }

                List<GameObject> match = new List<GameObject> { piezaBase };
                int y2 = y + 1;

                while (y2 < alto && EsColorObjetivo(piezas[x, y2]))
                {
                    match.Add(piezas[x, y2]);
                    y2++;
                }

                if (match.Count >= 3) piezasAEliminar.AddRange(match);
                y = y2;
            }
        }

        // --- FORMAS EN L, T, CRUZ ---
        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                GameObject p = piezas[x, y];
                if (!EsColorObjetivo(p)) continue;

                bool arriba = (y + 1 < alto && EsColorObjetivo(piezas[x, y + 1]));
                bool abajo = (y - 1 >= 0 && EsColorObjetivo(piezas[x, y - 1]));
                bool derecha = (x + 1 < ancho && EsColorObjetivo(piezas[x + 1, y]));
                bool izquierda = (x - 1 >= 0 && EsColorObjetivo(piezas[x - 1, y]));

                int vecinos = (arriba ? 1 : 0) + (abajo ? 1 : 0) + (derecha ? 1 : 0) + (izquierda ? 1 : 0);

                // T o cruz
                if (vecinos >= 3)
                {
                    piezasAEliminar.Add(p);
                    if (arriba) piezasAEliminar.Add(piezas[x, y + 1]);
                    if (abajo) piezasAEliminar.Add(piezas[x, y - 1]);
                    if (derecha) piezasAEliminar.Add(piezas[x + 1, y]);
                    if (izquierda) piezasAEliminar.Add(piezas[x - 1, y]);
                }

                // L
                if (arriba && derecha) piezasAEliminar.AddRange(new[] { p, piezas[x, y + 1], piezas[x + 1, y] });
                if (arriba && izquierda) piezasAEliminar.AddRange(new[] { p, piezas[x, y + 1], piezas[x - 1, y] });
                if (abajo && derecha) piezasAEliminar.AddRange(new[] { p, piezas[x, y - 1], piezas[x + 1, y] });
                if (abajo && izquierda) piezasAEliminar.AddRange(new[] { p, piezas[x, y - 1], piezas[x - 1, y] });
            }
        }

        // --- ELIMINAR PIEZAS SIN REPETIR ---
        piezasAEliminar = piezasAEliminar.Distinct().ToList();

        foreach (GameObject pieza in piezasAEliminar)
        {
            float x = pieza.transform.position.x;
            float y = 12.0f;
            pieza.transform.position = new Vector3(x, y, 0);
            Color[] colores = pieza.transform.parent.GetComponent<GrupoPiezas>().Colores;
            pieza.GetComponent<SpriteRenderer>().color = colores[Random.Range(0, colores.Length)]; ;


        }


        if (piezasAEliminar.Count > 0)
        {

        }
            
    }
}


