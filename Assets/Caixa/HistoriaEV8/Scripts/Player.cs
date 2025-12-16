using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject camara;

    [SerializeField]
    private GameObject maga;

    [SerializeField]
    private GameObject guerrero;

    [SerializeField]
    private GameObject hechicero;

    private Ray rayo;
    private RaycastHit hit;
    private Vector3 puntoASeguir;
    private float distancia;
    private bool bloquarProtagonista;

    private int pocimasRecogidas;

    private bool bloquearPro;

    private NavMeshAgent agente;
    private Animator anim;

    void Start()
    {
        bloquarProtagonista = true;
        pocimasRecogidas = 0;
        bloquearPro = false;

        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (bloquearPro == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rayo = camara.GetComponent<Camera>().ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camara.transform.position.z));

                if (Physics.Raycast(rayo, out hit))
                {
                    if (hit.collider.gameObject.tag == "Terreno")
                    {
                        puntoASeguir = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
                        agente.SetDestination(puntoASeguir); // <-- MOVEMOS AQUÍ SetDestination
                        anim.SetBool("caminando", true);
                        bloquarProtagonista = false;
                    }
                }
            }

            
            if (!bloquarProtagonista)
            {
                if (!agente.pathPending && agente.remainingDistance <= agente.stoppingDistance)
                {
                    agente.SetDestination(this.transform.position);
                    bloquarProtagonista = true;
                    anim.SetBool("caminando", false);
                }
            }

            // Cámara sigue al personaje
            camara.transform.position = new Vector3(this.transform.position.x, camara.transform.position.y, this.transform.position.z - 4.0f);
        }
    }

    public void BloquearProtagonista()
    {
        bloquarProtagonista = true;
    }

    public void DesBloquearProtagonista()
    {
        bloquarProtagonista = false;
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PocimaRoja")
        {
            other.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            pocimasRecogidas++;
            if (pocimasRecogidas >= 3)
            {
                anim.SetBool("victoria", true);
                Invoke("CambiarEscena", 2.0f);
            }
        }

        if (other.gameObject.tag == "PocimaVerde")
        {
            other.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            pocimasRecogidas++;
            if (pocimasRecogidas >= 3)
            {
                anim.SetBool("victoria", true);
                Invoke("CambiarEscena", 2.0f);
            }
        }

        if (other.gameObject.tag == "PocimaAzul")
        {
            other.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            pocimasRecogidas++;
            if (pocimasRecogidas >= 3)
            {
                anim.SetBool("victoria", true);
                Invoke("CambiarEscena", 2.0f);
            }
        }
    }
}

