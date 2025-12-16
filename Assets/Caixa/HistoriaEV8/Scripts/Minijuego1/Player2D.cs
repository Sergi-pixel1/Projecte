using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField]
    private GameObject acumulador;
    [SerializeField] private FloatingJoystick fj;
    [SerializeField, Range(1, 10)] private float velocidad;

    [SerializeField] private GameObject explosiones;
    private int valorRR;
    private int i;

    [SerializeField] private GameObject misilesPlayer;
    private int valorRRmisil;

    [SerializeField]
    private GameObject[] sonidos;

    void Start()
    {
        valorRRmisil = 0;
        valorRR = 0;
        InvokeRepeating("Disparar", 0.0f, 1.0f);
    }

    void Update()
    {
        
        transform.Translate(
            fj.Horizontal * Time.deltaTime * velocidad,
            fj.Vertical * Time.deltaTime * velocidad,
            0.0f
        );
    }

    public void Disparar()
    {
        sonidos[1].gameObject.GetComponent<AudioSource>().Play();
        misilesPlayer.transform.GetChild(valorRRmisil).gameObject.transform.position = this.gameObject.transform.position;
        misilesPlayer.transform.GetChild(valorRRmisil).gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0.0f,10.0f);
        valorRRmisil++;

        if (valorRRmisil >= misilesPlayer.transform.childCount)
        {
            valorRRmisil = 0;
        }
    }

    private void OcultarExplosion()
    {
        for (i = 0; i < explosiones.transform.childCount; i++)
        {
            explosiones.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "MisilPlayer")
        {
            sonidos[0].gameObject.GetComponent<AudioSource>().Play();
            explosiones.transform.GetChild(valorRR).position = transform.position = this.gameObject.transform.position;
            explosiones.transform.GetChild(valorRR).gameObject.SetActive(true);
            Invoke("OcultarExplosion", 0.3f);

            valorRR++;
            if (valorRR >= explosiones.transform.childCount)
                valorRR = 0;

            Destroy(other.gameObject);
            acumulador.gameObject.GetComponent<UiControler>().MostarPanelDerrota();
            Destroy(gameObject);
        }
    }
}
