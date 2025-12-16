using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 5f;
    public float fireRate = 1f;
    public int damage = 1;
    public Transform firePoint;       // Punto de disparo
    public GameObject projectilePrefab;

    private float fireCountdown = 0f;

    void Update()
    {
        EnemigoTower target = FindClosestEnemy();

        if (target != null)
        {
            if (fireCountdown <= 0f)
            {
                Shoot(target);
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    EnemigoTower FindClosestEnemy()
    {
        EnemigoTower[] enemies = FindObjectsOfType<EnemigoTower>();
        EnemigoTower closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (EnemigoTower enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= attackRange)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    void Shoot(EnemigoTower target)
    {
        GameObject projGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile proj = projGO.GetComponent<Projectile>();
        proj.damage = damage;
        proj.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

