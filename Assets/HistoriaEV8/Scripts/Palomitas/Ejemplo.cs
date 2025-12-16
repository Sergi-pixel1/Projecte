using UnityEngine;

public class Ejemplo : MonoBehaviour
{

    [SerializeField]
    private Camera camara;
    [SerializeField]
    private GameObject palomita;
    private Vector3 puntoDeToque;
    private Vector3 puntoDelMundo;
    private GameObject palomitaClon;
    private Vector3 puntoDelRayo;
    private Vector3 origenDelRayo;
    private RaycastHit2D hit;
    private float distanciaRayo;
    [SerializeField]
    private GameObject nuemero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distanciaRayo = 100.0f;
        //Screen.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)

            {
                puntoDeToque = Input.GetTouch(i).position;
                puntoDelRayo= camara.ScreenToViewportPoint(puntoDeToque);
                origenDelRayo = new Vector2(puntoDelRayo.x, puntoDelRayo.y);

                hit = Physics2D.Raycast(origenDelRayo, Vector2.up, distanciaRayo);
                if(hit.collider != null) 
                {
                    if(hit.collider.tag=="Balnca")

                    {
                        //nuemeroClon= Instantiate(numero,hit.point,Quaternion.identity);
                        //Destroy(numeroClon, 5.0f);
                        //Destroy(hit.collider.gameObject);
                        hit.collider.gameObject.transform.position = new Vector2(1000, 1000);

                    }
                
                
                }



                //puntoDelMundo= camara.ScreenToViewportPoint(puntoDeToque);
                //puntoDelMundo.z = 0.0f;




            }

        }






    }
}
