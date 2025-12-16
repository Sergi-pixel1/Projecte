using UnityEngine;
using TMPro; // Muy importante

public class ResourceManager : MonoBehaviour
{
    public int gold = 0;
    public int goldPerSecond = 1;
    public TMP_Text goldText; 

    void Start()
    {
        InvokeRepeating("GenerateGold", 1f, 1f);
    }

    void GenerateGold()
    {
        gold += goldPerSecond;
        goldText.text = "Gold: " + gold; 
    }

    public void UpgradeGold()
    {
        if (gold >= 50)
        {
            gold -= 50;
            goldPerSecond += 1;
        }
    }
}


