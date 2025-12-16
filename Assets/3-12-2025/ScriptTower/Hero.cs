using UnityEngine;

public class Hero : MonoBehaviour
{
    public int damagePerSecond = 1;
    public int damageOnClick = 5;
    public float clickRadius = 50f; // radio en píxeles para seleccionar enemigo

    void Start()
    {
        InvokeRepeating("DealDamage", 1f, 1f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ApplyClickDamage();
        }
    }

    void DealDamage()
    {
        EnemigoTower enemy = FindObjectOfType<EnemigoTower>();
        if (enemy != null)
        {
            enemy.TakeDamage(damagePerSecond);
        }
    }

    void ApplyClickDamage()
    {
        EnemigoTower[] enemies = FindObjectsOfType<EnemigoTower>();
        Vector3 mousePos = Input.mousePosition;
        EnemigoTower closestEnemy = null;
        float closestDistance = clickRadius;

        foreach (EnemigoTower enemy in enemies)
        {
            Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
            float distance = Vector2.Distance(mousePos, enemyScreenPos);

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            closestEnemy.TakeDamage(damageOnClick);
        }
    }
}



