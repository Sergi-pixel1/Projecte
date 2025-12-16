using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugar : MonoBehaviour
{
    public void CambiarEscena()
    {
        Debug.Log("Voy a cambiar de escena");
        SceneManager.LoadScene("Juego");

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
