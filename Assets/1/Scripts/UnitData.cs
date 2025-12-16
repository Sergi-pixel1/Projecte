using UnityEngine;

[CreateAssetMenu(menuName = "Tactics/Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public Sprite portrait;
    public int maxHP = 20;
    public int atk = 5;
    public int def = 2;
    public int mov = 5;
    public int rng = 1; // 1: melee; 2: rifle
}

