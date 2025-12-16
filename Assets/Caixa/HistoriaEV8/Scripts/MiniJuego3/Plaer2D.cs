using UnityEngine;
using TMPro;


public class Plaer2D : MonoBehaviour
{
    [SerializeField]
    private float velocidad;
    

    private int puntuacion;
    public int puntosPorColision = 1;
    [SerializeField]
    private GameObject puntuacionUI;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //puntuacionUI = (GameObject)GameObject.FindGameObjectWithTag("PuntuacionUI");
        //if (puntuacionUI == null)
        //{ Debug.Log("Lo hemos encontontrado"); }
        //else { Debug.Log("Lo Siento pero no"); }
            this.puntuacion = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Input.GetAxis("Horizontal") * velocidad * Time.deltaTime, 0.0f, 0.0f);

    
        //Debug.Log(Input.GetAxis("Horizontal"));


    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Planeta")
        { 
           puntuacion = puntuacion + 1;

            puntuacionUI.gameObject.GetComponent<TMP_Text>().text = puntuacion.ToString();
            Debug.Log("Choco con un planeta" + puntuacion); }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Planeta")
        {
            
            Debug.Log("Estoy chocando con un planeta" + puntuacion);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Planeta")
        {
            
            Debug.Log("He dejado de colisionar con un planeta" + puntuacion);
        }
    }

}
