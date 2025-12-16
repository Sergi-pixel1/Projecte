using UnityEngine;

public class EnemigoTower : MonoBehaviour
{
    public int health = 10;
    public int goldReward = 5;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        FindObjectOfType<ResourceManager>().gold += goldReward;
        Destroy(gameObject);
    }
}
