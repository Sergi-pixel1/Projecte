using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerJostick : MonoBehaviour
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
        bloquearPro = true;

        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (bloquearPro == false)
        {
            
            if (Input.touchCount > 0)
            {
                Touch toque = Input.GetTouch(0);

                // Solo cuando se toca por primera vez
                if (toque.phase == TouchPhase.Began)
                {
                    rayo = camara.GetComponent<Camera>().ScreenPointToRay(toque.position);

                    if (Physics.Raycast(rayo, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Terreno"))
                        {
                            puntoASeguir = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                            bloquarProtagonista = false;
                        }
                    }
                }
            }

            // Movimiento hacia el punto tocado
            if (!bloquarProtagonista)
            {
                distancia = Vector3.Distance(transform.position, puntoASeguir);
                if (distancia < 0.3f)
                {
                    bloquarProtagonista = true;
                    transform.position = puntoASeguir;
                    anim.SetBool("caminando", false);
                }
                else
                {
                    agente.SetDestination(puntoASeguir);
                    anim.SetBool("caminando", true);
                }
            }

            // Cámara sigue al jugador
            camara.transform.position = new Vector3(
                transform.position.x,
                camara.transform.position.y,
                transform.position.z - 4.0f
            );
        }
    }

    public void BloquearProtagonista()
    {
        bloquearPro = true;
    }

    public void DesBloquearProtagonista()
    {
        bloquearPro = false;
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PocimaRoja") || other.CompareTag("PocimaVerde") || other.CompareTag("PocimaAzul"))
        {
            other.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            pocimasRecogidas++;
            if (pocimasRecogidas >= 3)
            {
                anim.SetBool("victoria", true);
                Invoke(nameof(CambiarEscena), 2.0f);
            }
        }
    }
}

