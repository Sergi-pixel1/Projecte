using UnityEngine;

[CreateAssetMenu(fileName = "MoneyData", menuName = "Game/Data/Money Data")]
public class Dinero : ScriptableObject
{
    [Header("Estado actual de dinero")]
    public int dinero;

    [Header("Límites")]
    public int minimo = 0;
    public int maximo = 999999;

    public void Set(int valor)
    {
        dinero = Mathf.Clamp(valor, minimo, maximo);
    }

    public void Add(int cantidad)
    {
        Set(dinero + cantidad);
    }

    public void Subtract(int cantidad)
    {
        Set(dinero - cantidad);
    }
}
