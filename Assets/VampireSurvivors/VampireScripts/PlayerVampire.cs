using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerVampire : MonoBehaviour
{
    public GameObject camara;
    public float speed = 5f;
    public int xp;
    public int level = 1;
    private int xpToNext = 100;
    private float minZoom;
    private float maxZoom;

    private void Start()
    {
         minZoom = 3;
     maxZoom = 10; 
}

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(h, v).normalized * speed * Time.deltaTime;
        transform.Translate(move);


        
            float mouse= Input.GetAxis("Mouse ScrollWheel");
        Debug.Log("Mouse" + mouse);
        float zoom = camara.gameObject.GetComponent<Camera>().orthographicSize + mouse;
        camara.gameObject.GetComponent<Camera>().orthographicSize = Mathf.Clamp(zoom, minZoom, maxZoom);






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
            Debug.Log($"¡Game Over! Nivel alcanzado: {level}");
            Time.timeScale = 0;
        }
    }
}
