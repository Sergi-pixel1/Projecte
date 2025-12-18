using UnityEngine;

public class PlayerVampire : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject camara;
    public GameObject panelDerrota;

    [Header("Movimiento")]
    public float speed = 5f;

    [Header("Experiencia")]
    public int xp;
    public int level = 1;
    private int xpToNext = 100;

    [Header("Vidas")]
    public int vidas = 3;

    private float minZoom;
    private float maxZoom;

    private Animator animator;          // Animator del personaje
    private SpriteRenderer spriteRenderer; // Para voltear el sprite según dirección

    private void Start()
    {
        minZoom = 3;
        maxZoom = 10;

        panelDerrota.SetActive(false);

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleMovement();
        HandleCameraZoom();
    }

    private void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 input = new Vector2(h, v);

        // Movimiento del personaje
        Vector2 move = input.normalized * speed * Time.deltaTime;
        transform.Translate(move);

        // Actualizamos la animación con umbral mínimo
        if (animator != null)
        {
            float speedValue = input.magnitude < 0.1f ? 0f : input.magnitude;
            animator.SetFloat("Speed", speedValue);
            // Debug.Log("Speed: " + speedValue); // Puedes descomentar para depurar
        }

        // Flip del sprite según dirección
        if (spriteRenderer != null)
        {
            if (h > 0) spriteRenderer.flipX = true;
            else if (h < 0) spriteRenderer.flipX = false;
        }
    }

    private void HandleCameraZoom()
    {
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        float zoom = camara.GetComponent<Camera>().orthographicSize + mouse;
        camara.GetComponent<Camera>().orthographicSize = Mathf.Clamp(zoom, minZoom, maxZoom);
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

    private void OnTriggerEnter2D(Collider2D col)
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

    private void GameOver()
    {
        Debug.Log($"¡Game Over! Nivel alcanzado: {level}");
        panelDerrota.SetActive(true);
        Time.timeScale = 0f;
    }
}




