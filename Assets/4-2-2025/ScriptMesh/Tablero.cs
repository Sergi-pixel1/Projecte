using UnityEngine;

public class Tablero : MonoBehaviour
{
    [SerializeField] private GameObject detector;
    [SerializeField] private int ancho;
    [SerializeField] private int alto;

    private GameObject[,] miTablero;
    private GameObject[,] piezasColores;

    // Getter público para otras clases
    public GameObject[,] GetPiezasColores()
    {
        return piezasColores;
    }

    void Start()
    {
        miTablero = new GameObject[ancho, alto];

        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                miTablero[i, j] = Instantiate(detector, new Vector3(i, j, 0), Quaternion.identity);
                miTablero[i, j].name = "Detector_" + i + "_" + j;
                miTablero[i, j].transform.SetParent(this.gameObject.transform);
            }
        }

        detector.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ActualizarTablero();
        }
    }

    public void ActualizarTablero()
    {
        piezasColores = new GameObject[ancho, alto];

        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                GameObject obj = miTablero[i, j].GetComponent<Detector>().obtenerObjetoDetectado();
                if (obj != null)
                {
                    piezasColores[i, j] = obj;
                }
            }
        }
    }
}


