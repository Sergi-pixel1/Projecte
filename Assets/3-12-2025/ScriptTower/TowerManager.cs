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
            return;

        // Verificar oro
        if (!GoldManager.instance.SpendGold(price))
        {
            Debug.Log("No tienes suficiente oro.");
            // Aquí podrías mostrar un panel de aviso en la UI
            return;
        }

        // Construir torre
        Instantiate(prefab, selectedSpot.transform.position, Quaternion.identity);

        // Ocultar menú
        selectedSpot.towerMenuUI.SetActive(false);

        selectedSpot = null;
    }
}


