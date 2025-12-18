using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager Instance;

    [Header("Victoria")]
    public int enemigosMuertos = 0;
    public int enemigosParaGanar = 50;

    [Header("UI")]
    public GameObject panelVictoria;
    public TMP_Text textoEnemigosRestantes;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        panelVictoria.SetActive(false);
        ActualizarUI();
    }

    public void EnemigoEliminado()
    {
        enemigosMuertos++;
        ActualizarUI();

        if (enemigosMuertos >= enemigosParaGanar)
        {
            Victoria();
        }
    }

    void ActualizarUI()
    {
        int restantes = enemigosParaGanar - enemigosMuertos;
        restantes = Mathf.Max(restantes, 0);

        textoEnemigosRestantes.text = $"Enemigos restantes: {restantes}";
    }

    void Victoria()
    {
        panelVictoria.SetActive(true);
        Time.timeScale = 0f;

        StartCoroutine(CambiarAEscenaHex());
    }

    IEnumerator CambiarAEscenaHex()
    {
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f; // importante restaurar el tiempo
        SceneManager.LoadScene("hex");
    }
}


