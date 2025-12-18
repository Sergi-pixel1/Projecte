using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenaConDelay : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CambiarEscena());
    }

    System.Collections.IEnumerator CambiarEscena()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MenuPrincipalFinal");
    }
}

