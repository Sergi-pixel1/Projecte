using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public int comboMultiplicador = 1;

    private void Awake()
    {
        instance = this;
    }

    public void AgregarPuntos(int cantidad)
    {
        int puntosFinales = cantidad * comboMultiplicador;
        score += puntosFinales;

        Debug.Log($"Puntos: +{puntosFinales}  (Combo x{comboMultiplicador})  | Total: {score}");
    }

    public void ResetCombo()
    {
        comboMultiplicador = 1;
    }

    public void SubirCombo()
    {
        comboMultiplicador++;
        Debug.Log("Combo aumentado: x" + comboMultiplicador);
    }
}

