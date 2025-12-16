using UnityEngine;

[CreateAssetMenu(menuName = "Tactics/Hex Tile Data")]
public class HexTileData : ScriptableObject
{
    public string tileName;
    public Sprite sprite;
    public int moveCost = 1;
    public int defenseBonus = 0;
    public bool blocksMovement = false;
}
