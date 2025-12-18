using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tiempo : MonoBehaviour
{
    public static Tiempo Instance;

    public float timeRemaining = 180f;
    public bool timerIsRunning = false;
    private int minutes;
    private int seconds;

    [HideInInspector] public TMP_Text timeText; // se asigna desde TiempoUI

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
        }
        else
        {
            Destroy(gameObject); // evita duplicados
        }
    }

    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                minutes = Mathf.FloorToInt(timeRemaining / 60);
                seconds = Mathf.FloorToInt(timeRemaining % 60);

                if (timeText != null)
                {
                    timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                }
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.LoadScene("Derrota");
            }
        }
    }

    //  Método para reiniciar el contador
    public void ResetTimer()
    {
        timeRemaining = 180f;
        timerIsRunning = true;
    }
}



