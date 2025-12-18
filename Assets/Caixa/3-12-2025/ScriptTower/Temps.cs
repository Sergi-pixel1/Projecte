using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Temps : MonoBehaviour
{
    public static Temps Instance;

    [Header("Configuración de Tiempo")]
    public float tiempoMaximo = 60f;
    private float tiempoActual;
    private bool terminado = false;

    [Header("UI")]
    public TMP_Text timeText;
    public GameObject panelVictoria;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        tiempoActual = tiempoMaximo;

        if (panelVictoria != null)
            panelVictoria.SetActive(false);
    }

    void Update()
    {
        if (terminado || timeText == null) return;

        tiempoActual -= Time.deltaTime;
        tiempoActual = Mathf.Max(tiempoActual, 0);

        timeText.text = Mathf.CeilToInt(tiempoActual).ToString();

        if (tiempoActual <= 0)
        {
            terminado = true;
            StartCoroutine(MostrarVictoria());
        }
    }

    IEnumerator MostrarVictoria()
    {
        if (panelVictoria != null)
            panelVictoria.SetActive(true);

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("hex");
    }
}


