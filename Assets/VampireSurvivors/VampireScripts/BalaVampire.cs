using UnityEngine;

public class BalaVampire : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 8f;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            FindObjectOfType<PlayerVampire>()?.GainXP(20);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
