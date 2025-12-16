using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControler : MonoBehaviour
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
        record = PlayerPrefs.GetInt("RECORD");

        if (puntuacion > record)

        {
            PlayerPrefs.SetInt("RECORD", puntuacion);
            record = PlayerPrefs.GetInt("RECORD");
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
        SceneManager.LoadScene("NaveJuego");
        Time.timeScale = 1f;
    }







    public void ActualizarPuntacionUI(int puntos)

    { 
        puntacionUi.gameObject.GetComponent<TMP_Text>().text= puntos.ToString();
    
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

        if (PlayerPrefs.HasKey("RECORD"))
        {
            //Existe
            record = PlayerPrefs.GetInt("RECORD");
            ActualizarRecordUI(record);

        }
        else 
        {
            PlayerPrefs.SetInt("RECORD", 0);
            record = PlayerPrefs.GetInt("RECORD");
            ActualizarRecordUI(record);


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
