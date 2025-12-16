using UnityEngine;

public class MisilPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject acumulador;
    private int posDestruccion;
    private int incremento;
    [SerializeField]
    private GameObject explosionesMisiles;
    private int valorRR;
    private int i;
    [SerializeField]
    private GameObject []sonidos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        valorRR = 0;
        posDestruccion = 300;
        incremento = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OcultarExplosion()
    {
        for (i = 0; i < explosionesMisiles.gameObject.transform.childCount; i++)
        {
            explosionesMisiles.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        acumulador.gameObject.GetComponent<UiControler>().ActualizarPuntuacion(1);
        sonidos[0].gameObject.GetComponent<AudioSource>().Play();
        explosionesMisiles.gameObject.transform.GetChild(valorRR).gameObject.transform.position = other.gameObject.transform.position;
        explosionesMisiles.gameObject.transform.GetChild(valorRR).gameObject.SetActive(true);
        Invoke("OcultarExplosion", 1f);
        valorRR++;
        if (valorRR >= explosionesMisiles.gameObject.transform.childCount) { valorRR = 0; }
        other.gameObject.transform.position = new Vector2(posDestruccion, 300);
        posDestruccion = posDestruccion+ incremento;
        this.gameObject.transform.position = new Vector2(posDestruccion, 400);
    }
}
