using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefabb;
    public float spawnIntervall = 5f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, spawnIntervall);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefabb, transform.position, Quaternion.identity);
    }
}

