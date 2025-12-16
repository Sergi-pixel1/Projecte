using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    public GameObject towerMenuUI; // Panel que aparecerá para seleccionar torre

    void OnMouseDown()
    {
        // Mostrar el panel de selección
        towerMenuUI.SetActive(true);

        // Opcional: guardar la referencia de este spot para colocar la torre después
        TowerManager.instance.selectedSpot = this;
    }
}

