using UnityEngine;

public class EnemigoVampire : MonoBehaviour
{
    public float speed = 2f;
    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerVampire>()?.transform;
        if (!player) Destroy(gameObject);
    }

    void Update()
    {
        if (player)
        {
            Vector2 dir = (Vector2)player.position - (Vector2)transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime);
        }
    }
}
