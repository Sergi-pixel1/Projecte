using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public GameObject bulletPrefab;
    Transform player;
    List<string> weapons = new List<string> { "Garlic" };
    float garlicTimer, knifeTimer;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerVampire>()?.transform;
    }

    void Update()
    {
        if (!player) return;

        // Garlic: dispara en círculo
        if (weapons.Contains("Garlic"))
        {
            if ((garlicTimer -= Time.deltaTime) <= 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    float angle = i * 45 * Mathf.Deg2Rad;
                    Shoot(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
                }
                garlicTimer = 1f;
            }
        }

        // Knife: dispara al mouse
        if (weapons.Contains("Knife"))
        {
            if ((knifeTimer -= Time.deltaTime) <= 0)
            {
                Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)player.position;
                Shoot(dir.normalized);
                knifeTimer = 0.5f;
            }
        }
    }

    public void OnLevelUp(int level)
    {
        if (level == 2 && !weapons.Contains("Knife"))
        {
            weapons.Add("Knife");
            Debug.Log("¡Nueva arma desbloqueada: Knife!");
        }
    }

    void Shoot(Vector2 dir)
    {
        if (bulletPrefab)
            Instantiate(bulletPrefab, player.position, Quaternion.identity).GetComponent<BalaVampire>().direction = dir;
    }
}
