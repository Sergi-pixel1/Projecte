using UnityEngine;

public class Combatant : MonoBehaviour
{
    public string unitName;
    public int maxHP = 10;
    public int currentHP;
    public int speed = 5;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public bool IsAlive()
    {
        return currentHP > 0;
    }
}

