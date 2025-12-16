
using UnityEngine;

public class Pieza : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Obtener el color de la pieza clickeada
        Color colorClickeado = GetComponent<SpriteRenderer>().color;

        // Avisar al DetectorCoincidencias
        DetectorCoincidencias.instancia.BuscarYActualizar(colorClickeado);
        float x = this.transform.position.x;
        float y = 12.0f;
        this.transform.position = new Vector3(x, y, 0);
        Color[] colores = transform.parent.GetComponent<GrupoPiezas>().Colores;
        GetComponent<SpriteRenderer>().color= colores[Random.Range(0, colores.Length)]; ;

        // Destruir la pieza clickeada
        //Destroy(gameObject);
    }
}

