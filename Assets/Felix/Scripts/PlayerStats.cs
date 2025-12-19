using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxHP = 20;
    public int currentHP;

    public Slider hpSlider;

    void Start()
    {
        currentHP = maxHP;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        UpdateUI();
        Debug.Log("Jugador recibe " + damage + " de daño | HP restante: " + currentHP);

        if (currentHP <= 0)
            Die();
    }

    void UpdateUI()
    {
        if (hpSlider != null)
            hpSlider.value = (float)currentHP / maxHP;
    }

    void Die()
    {
        Debug.Log("¡Jugador derrotado!");
    }
}


