using UnityEngine;
using UnityEngine.SceneManagement;

public class AcumuladorScene : MonoBehaviour
{
    public void CambiarEscena()
    {





    }
    public void CambiarEscena1() { SceneManager.LoadScene("MenuPrincipal"); }
    public void CambiarEscenaCreditos() { SceneManager.LoadScene("Creditos"); }
    public void CambiarEscenahex() { SceneManager.LoadScene("hex"); }
    public void CambiarEscenaRebote() { SceneManager.LoadScene("ReboteJuego"); }
    public void CambiarEscenaJuego4() { SceneManager.LoadScene("Juego4"); }
    public void CambiarEscenaHistoria() { SceneManager.LoadScene("Historia"); }

    public void CambiarEscenaMinijuegos() { SceneManager.LoadScene("MiniJuegos"); }

    public void CambiarEscenaProyectoFinalDemo() { SceneManager.LoadScene("Intro"); }

    public void CambiarEscenaCreditosFinals() { SceneManager.LoadScene("CreditosFinales"); }
    public void CambiarEscenaVampire() { SceneManager.LoadScene("VampireJuego"); }
    public void CambiarEscenaTower() { SceneManager.LoadScene("Tower"); }
    public void CambiarEscenaSplashFinal() { SceneManager.LoadScene("SplashFinal"); }
    public void CambiarEscenaMesh2() { SceneManager.LoadScene("Mesh2"); }

    public void CambiarEscenaMenuPrincipalFinal() { SceneManager.LoadScene("MenuPrincipalFinal"); }

    public void CambiarEscenaRPG() { SceneManager.LoadScene("RPG"); }


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
