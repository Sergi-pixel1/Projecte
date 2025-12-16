using UnityEngine;

public class EnemySpawnerr : MonoBehaviour
{
    public GameObject enemyPrefab;

    float timer;
    float spawnInterval = 1f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;

            // Limita la velocidad mínima
            spawnInterval = Mathf.Max(0.3f, spawnInterval * 0.98f);
        }
    }

    void Spawn()
    {
        Debug.Log("Intentando spawnear...");  //  TEST 1

        if (!enemyPrefab)
        {
            Debug.LogError("ERROR: enemyPrefab NO está asignado en el inspector!");  //  TEST 2
            return;
        }

        Camera cam = Camera.main;
        if (!cam)
        {
            Debug.LogError("ERROR: No existe una cámara con el tag MAIN CAMERA");  //  TEST 3
            return;
        }

        float h = cam.orthographicSize * cam.aspect;
        float v = cam.orthographicSize;

        Vector2 pos;

        switch (Random.Range(0, 4))
        {
            case 0: pos = new Vector2(Random.Range(-h, h), -v - 1); break;
            case 1: pos = new Vector2(Random.Range(-h, h), v + 1); break;
            case 2: pos = new Vector2(-h - 1, Random.Range(-v, v)); break;
            default: pos = new Vector2(h + 1, Random.Range(-v, v)); break;
        }

        Debug.Log("Spawneando enemigo en: " + pos);  //  TEST 4

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}


