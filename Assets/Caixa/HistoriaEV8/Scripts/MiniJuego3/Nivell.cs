using UnityEngine;
using UnityEngine.SceneManagement;


public class Nivell : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //CambiarEscena();
        Invoke("CambiarEscena", 3.0f);

    }

    public void CambiarEscena()
    { //Debug.Log("Voy a cambiar de escena");
        SceneManager.LoadScene("Juego");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
