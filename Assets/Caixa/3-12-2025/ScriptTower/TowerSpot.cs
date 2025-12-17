using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    public GameObject towerMenuUI;

    void OnMouseDown()
    {
        if (towerMenuUI == null)
        {
            Debug.LogError("TowerMenuUI no asignado en " + gameObject.name);
            return;
        }

        if (TowerManager.instance == null)
        {
            Debug.LogError("TowerManager no existe en la escena");
            return;
        }

        towerMenuUI.SetActive(true);
        TowerManager.instance.selectedSpot = this;
    }
}


