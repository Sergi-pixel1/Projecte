using UnityEngine;

public class Tierra : MonoBehaviour
{
    [SerializeField]
    private GameObject acumulador;
    [SerializeField]
    private GameObject explosiones;
    private int valorRR;
    private int i;

    [SerializeField]
    private GameObject sonidos; // Este objeto debe tener un AudioSource en el Inspector

    void Start()
    {
        valorRR = 0;
    }

    void Update()
    {

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
        // Reproduce el sonido de explosión
        sonidos.GetComponent<AudioSource>().Play();

        // Mueve y activa la explosión en la posición del impacto
        explosiones.transform.GetChild(valorRR).position = other.transform.position;
        explosiones.transform.GetChild(valorRR).gameObject.SetActive(true);

        // Oculta la explosión después de 0.3 segundos
        Invoke("OcultarExplosion", 0.3f);

        // Cicla entre las explosiones disponibles
        valorRR++;
        if (valorRR >= explosiones.transform.childCount)
        {
            valorRR = 0;
        }

        // Destruye el objeto que colisionó
        Destroy(other.gameObject);

        // Muestra la pantalla de derrota
        acumulador.GetComponent<UiControler>().MostarPanelDerrota();

        // Destruye este objeto (la Tierra)
        Destroy(this.gameObject);
    }
}
