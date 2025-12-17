using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    public TowerSpot selectedSpot;

    [Header("Prefabs de torres")]
    public GameObject towerPrefab1;
    public GameObject towerPrefab2;

    [Header("Precios")]
    public int tower1Price = 50;
    public int tower2Price = 75;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void BuildTower1()
    {
        TryBuild(towerPrefab1, tower1Price);
    }

    public void BuildTower2()
    {
        TryBuild(towerPrefab2, tower2Price);
    }

    void TryBuild(GameObject prefab, int price)
    {
        if (selectedSpot == null)
        {
            Debug.LogWarning("No hay TowerSpot seleccionado");
            return;
        }

        if (prefab == null)
        {
            Debug.LogError("Prefab de torre no asignado");
            return;
        }

        if (GoldManager.instance == null)
        {
            Debug.LogError("GoldManager no existe en la escena");
            return;
        }

        if (!GoldManager.instance.SpendGold(price))
        {
            Debug.Log("No tienes suficiente oro");
            return;
        }

        Instantiate(prefab, selectedSpot.transform.position, Quaternion.identity);

        if (selectedSpot.towerMenuUI != null)
            selectedSpot.towerMenuUI.SetActive(false);

        selectedSpot = null;
    }
}



