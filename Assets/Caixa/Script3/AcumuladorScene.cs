using UnityEngine;
using UnityEngine.SceneManagement;

public class AcumuladorScene : MonoBehaviour
{
    public void CambiarEscena()
    {





    }
    public void CambiarEscena1() { SceneManager.LoadScene("MenuPrincipal"); }
    public void CambiarEscenaCreditos() { SceneManager.LoadScene("Creditos"); }
    public void CambiarEscenaNave() { SceneManager.LoadScene("NaveJuego"); }
    public void CambiarEscenaRebote() { SceneManager.LoadScene("ReboteJuego"); }
    public void CambiarEscenaJuego4() { SceneManager.LoadScene("Juego4"); }
    public void CambiarEscenaHistoria() { SceneManager.LoadScene("Historia"); }

    public void CambiarEscenaMinijuegos() { SceneManager.LoadScene("MiniJuegos"); }

    public void CambiarEscenaProyectoFinalDemo() { SceneManager.LoadScene("Intro"); }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
