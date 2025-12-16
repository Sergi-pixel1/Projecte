using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class VictoriaMesh2 : MonoBehaviour
{
    public TextMeshProUGUI textoPuntuacion;
    public int puntuacionVictoria = 99999;

    public GameObject panelVictoria;

    private bool victoriaActivada = false;

    void Start()
    {
        panelVictoria.SetActive(false);
    }

    void Update()
    {
        int puntuacionActual = ObtenerPuntuacion();

        if (!victoriaActivada && puntuacionActual >= puntuacionVictoria)
        {
            victoriaActivada = true;
            Victoria();
        }
    }

    int ObtenerPuntuacion()
    {
        // Ejemplo: "Score: 15"  "15"
        string texto = textoPuntuacion.text;
        string soloNumero = "";

        foreach (char c in texto)
        {
            if (char.IsDigit(c))
                soloNumero += c;
        }

        return int.Parse(soloNumero);
    }

    void Victoria()
    {
        panelVictoria.SetActive(true);
        StartCoroutine(CargarEscenaB());
    }

    IEnumerator CargarEscenaB()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("hex");
    }
}

