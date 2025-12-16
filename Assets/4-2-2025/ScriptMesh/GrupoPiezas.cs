using UnityEngine;

public class GrupoPiezas : MonoBehaviour
{

    [SerializeField]
    private GameObject pieza;
    [SerializeField]
    private int ancho;
    [SerializeField]
    private int alto;

    private GameObject[,] misPiezas;
    [SerializeField]
    private Color[] colores;

    public Color[] Colores { get => colores; set => colores = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        misPiezas = new GameObject[ancho, alto];
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                misPiezas[i, j] = (GameObject)Instantiate(pieza, new Vector3(i, j, 0), Quaternion.identity);

                misPiezas[i, j].gameObject.name = "Pieza_" + i + "_" + j;
                misPiezas[i, j].gameObject.GetComponent<SpriteRenderer>().color = Colores[Random.Range(0, Colores.Length)];
                misPiezas[i, j].gameObject.transform.SetParent(this.gameObject.transform);

            }

        }
        pieza.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
