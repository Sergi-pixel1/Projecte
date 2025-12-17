using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;

    public int gold = 100;
    public TMP_Text goldText;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public bool SpendGold(int amount)
    {
        if (gold < amount)
            return false;

        gold -= amount;
        UpdateUI();
        return true;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (goldText == null)
        {
            Debug.LogError("GoldText no asignado en el Inspector");
            return;
        }

        goldText.text = gold + "P";
    }
}


