using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float tiempoEspera = 3f;
    private bool escenaCambiada = false;

    void Update()
    {
        if (escenaCambiada) return;

        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");

        if (enemigos.Length == 0)
        {
            escenaCambiada = true;
            StartCoroutine(CambiarEscena());
        }
    }

    IEnumerator CambiarEscena()
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene("hex");
    }
}


