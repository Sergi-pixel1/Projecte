using UnityEngine;

public class PlanetaNivell3 : MonoBehaviour
{
    [SerializeField] private Vector2 initialVelocity;
    private Rigidbody2D ballrb;
    private GameObject PanellDerrota;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballrb = GetComponent<Rigidbody2D>();
        ballrb.linearVelocity = initialVelocity;
        PanellDerrota = (GameObject)GameObject.FindGameObjectWithTag("PanellDerrota");
       
        PanellDerrota.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("He sido detectado" + other.gameObject.tag);
        if (other.gameObject.tag == "SDerrota")
        {
            Debug.Log("Lo siento has perdido");
            PanellDerrota.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag ("Barra"))
        {
            Destroy(collision.gameObject);
                }
    }

   
    }
