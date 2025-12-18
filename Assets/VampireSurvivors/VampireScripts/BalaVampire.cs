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
            // Avisamos al VictoryManager
            VictoryManager.Instance?.EnemigoEliminado();

            // Damos experiencia al jugador
            FindObjectOfType<PlayerVampire>()?.GainXP(20);

            // Destruimos enemigo y bala
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

