using UnityEngine;

public class PlayerVampire : MonoBehaviour
{
    public GameObject camara;

    [Header("Movimiento")]
    public float speed = 5f;

    [Header("Experiencia")]
    public int xp;
    public int level = 1;
    private int xpToNext = 100;

    [Header("Vidas")]
    public int vidas = 3;

    [Header("UI")]
    public GameObject panelDerrota; // Arrástralo desde el Inspector

    private float minZoom;
    private float maxZoom;

    private void Start()
    {
        minZoom = 3;
        maxZoom = 10;

        panelDerrota.SetActive(false);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(h, v).normalized * speed * Time.deltaTime;
        transform.Translate(move);

        float mouse = Input.GetAxis("Mouse ScrollWheel");
        float zoom = camara.GetComponent<Camera>().orthographicSize + mouse;
        camara.GetComponent<Camera>().orthographicSize =
            Mathf.Clamp(zoom, minZoom, maxZoom);
    }

    public void GainXP(int amount)
    {
        xp += amount;
        while (xp >= xpToNext)
        {
            xp -= xpToNext;
            level++;
            xpToNext = Mathf.RoundToInt(xpToNext * 1.5f);
            WeaponManager.Instance?.OnLevelUp(level);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            vidas--;
            Debug.Log("Vidas restantes: " + vidas);

            if (vidas <= 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        Debug.Log($"¡Game Over! Nivel alcanzado: {level}");
        panelDerrota.SetActive(true);
        Time.timeScale = 0f;
    }
}

