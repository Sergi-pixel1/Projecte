using UnityEngine;

public class Punto : MonoBehaviour
{

    
    private GameObject puntosCami;
    private int posActual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posActual = 0;
        puntosCami = (GameObject)GameObject.FindGameObjectWithTag("CaminoDePuntos");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = Vector3.MoveTowards(this.transform.position, puntosCami.transform.GetChild(posActual).transform.position, 3.0f * Time.deltaTime);
        float distancia = Vector3.Distance(this.transform.position, puntosCami.transform.GetChild(posActual).transform.position);

        if (distancia < 0.1f)
        {
            posActual++;
        }


    }
}
