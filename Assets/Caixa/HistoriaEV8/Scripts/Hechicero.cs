using UnityEngine;
using Fungus;

public class Hechicero : MonoBehaviour
{
    [SerializeField]
    private GameObject protagonista;
    private float distanciaPro;

    [SerializeField]
    private float distanciaActivacion;

    private void OnMouseDown()
    {
        distanciaPro = Vector3.Distance(this.gameObject.transform.position, protagonista.gameObject.transform.position);
        if (distanciaPro > distanciaActivacion)
        { }
        else 
        {


            this.gameObject.transform.LookAt(protagonista.gameObject.transform.position);
            Fungus.Flowchart.BroadcastFungusMessage("Hechicero");
        
        
        }
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
