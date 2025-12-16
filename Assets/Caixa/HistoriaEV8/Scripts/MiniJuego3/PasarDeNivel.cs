using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarDeNivel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //CambiarEscena();
        Invoke("CambiarEscena", 10.0f);

    }

    public void CambiarEscena()
    { //Debug.Log("Voy a cambiar de escena");
        SceneManager.LoadScene("Nivell3");
    }


    // Update is called once per frame
    void Update()
    {

    }
}
