using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Identidad")]
    public string characterName = "Unit";

    [Header("Atributos")]
    public int maxHealth = 100;
    public int currentHealth;
    public int attackPower = 20;
    public int defense = 5;

    [Header("Recursos")]
    public int maxMana = 30;
    public int currentMana;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log($"{characterName} recibe {damage}. HP: {currentHealth}/{maxHealth}");
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"{characterName} se cura {amount}. HP: {currentHealth}/{maxHealth}");
    }

    public bool IsDead() => currentHealth <= 0;
}



