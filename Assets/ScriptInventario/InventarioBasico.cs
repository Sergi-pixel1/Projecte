using System.Collections.Generic;
using UnityEngine;

public class InventarioBasico : MonoBehaviour
{
    public int dinero = 0;

    // Inventario simple
    public List<string> objetos = new List<string>();

    // ---- DINERO ----
    public void SumarDinero(int cantidad)
    {
        dinero += cantidad;
        Debug.Log("Dinero actual: " + dinero);
    }

    public void RestarDinero(int cantidad)
    {
        dinero -= cantidad;
        if (dinero < 0) dinero = 0;
        Debug.Log("Dinero actual: " + dinero);
    }

    // ---- OBJETOS ----
    public void AgregarObjeto(string nombreObjeto)
    {
        objetos.Add(nombreObjeto);
        Debug.Log("Añadido objeto: " + nombreObjeto);
    }

    public void QuitarObjeto(string nombreObjeto)
    {
        if (objetos.Contains(nombreObjeto))
        {
            objetos.Remove(nombreObjeto);
            Debug.Log("Removido objeto: " + nombreObjeto);
        }
        else
        {
            Debug.Log("El objeto no está en el inventario: " + nombreObjeto);
        }
    }

    // Opcional: método de comprobación
    public bool TieneDinero(int cantidad)
    {
        return dinero >= cantidad;
    }
}


