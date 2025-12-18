using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene("Intro");
        Tiempo.Instance.ResetTimer();
    }


    public void CambiarEscena()
    {
        //Debug.Log("Voy a cambiar de escena");
        SceneManager.LoadScene("Intro");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
