using Fungus;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PJ : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text dineroTexto;

    [Header("Datos")]
    public Flowchart flowchart; // Flowchart asignado desde el Inspector

    public int dinero = 0;
    public int cantidadParaSumar = 0;
    public List<string> objetos = new List<string>();

    public void ActualizarUI()
    {
        if (dineroTexto != null)
        {
            dineroTexto.text = "Dinero: " + flowchart.GetIntegerVariable("Dinero");
            Debug.Log("UI actualizada: Dinero = " + flowchart.GetIntegerVariable("Dinero"));
        }
        else
        {
            Debug.LogWarning("dineroTexto no está asignado en el Inspector.");
        }
    }
}

