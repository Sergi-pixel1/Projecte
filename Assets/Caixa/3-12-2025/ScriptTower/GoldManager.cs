using UnityEngine;
using TMPro; // si usas TextMeshPro

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;

    public int gold = 100;             // Oro inicial
    public TMP_Text goldText;          // Texto en el Canvas

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        goldText.text = gold.ToString() + "P";
    }
}

