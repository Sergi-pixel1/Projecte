using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BloquesJuego3 : MonoBehaviour
{
    [SerializeField]
    private GameObject panelDerrota;
    [SerializeField]
    private GameObject puntacionUi;
    [SerializeField]
    private GameObject recordUI;

    private int puntuacion;
    private int record;




    public void ActualizarPuntuacion(int incremento)
    {
        puntuacion = puntuacion + incremento;
        ActualizarPuntacionUI(puntuacion);

        //Actualizar la puntacuion del record
        record = PlayerPrefs.GetInt("RECORDJuego3");

        if (puntuacion > record)

        {
            PlayerPrefs.SetInt("RECORDJuego3", puntuacion);
            record = PlayerPrefs.GetInt("RECORDJuego3");
            ActualizarRecordUI(record);


        }
    }
    public void MostarPanelDerrota()
    {
        panelDerrota.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }


    public void Reiniciar()
    {
        SceneManager.LoadScene("Juego");
        Time.timeScale = 1f;
    }







    public void ActualizarPuntacionUI(int puntos)

    {
        puntacionUi.gameObject.GetComponent<TMP_Text>().text = puntos.ToString();

    }

    public void ActualizarRecordUI(int puntosRecord)

    {
        recordUI.gameObject.GetComponent<TMP_Text>().text = puntosRecord.ToString();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        puntuacion = 0;
        record = 0;
        ActualizarPuntacionUI(puntuacion);
        ActualizarRecordUI(record);

        //PlayerPrefs.DeleteAll(); AIXÒ HO BORRA TOT!!
        //PlayerPrefs.DeleteKey("NOM DE LA CLAU A BORRAR") AIXÒ BORRA LA CLAU DINS DE LES ""

        if (PlayerPrefs.HasKey("RECORDJuego3"))
        {
            //Existe
            record = PlayerPrefs.GetInt("RECORDJuego3");
            ActualizarRecordUI(record);

        }
        else
        {
            PlayerPrefs.SetInt("RECORDJuego3", 0);
            record = PlayerPrefs.GetInt("RECORDJuego3");
            ActualizarRecordUI(record);


        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
