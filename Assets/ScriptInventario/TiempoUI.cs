using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TiempoUI : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject textoObj = GameObject.Find("TextoTiempo");
        if (textoObj != null)
        {
            Tiempo.Instance.timeText = textoObj.GetComponent<TMP_Text>();
        }
        else
        {
            Tiempo.Instance.timeText = null;
            Debug.Log("No se encontró TextoTiempo en la escena " + scene.name);
        }
    }
}



