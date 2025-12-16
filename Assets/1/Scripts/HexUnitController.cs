using UnityEngine;

public class HexUnitController : MonoBehaviour
{
    public UnitData data;
    public Vector2Int axial; // (q,r)
    public int currentHP;
    public bool isPlayer;

    HexGridManager grid;

    void Awake()
    {
        currentHP = data ? data.maxHP : 20;
        grid = FindObjectOfType<HexGridManager>();
    }

    public void Place(Vector2Int a)
    {
        axial = a;
        transform.position = HexGridManager.AxialToWorld(a.x, a.y, grid.hexSize);
        grid.GetTile(a).occupied = true;
    }

    public void MoveTo(Vector2Int a)
    {
        grid.GetTile(axial).occupied = false;
        axial = a;
        transform.position = HexGridManager.AxialToWorld(a.x, a.y, grid.hexSize);
        grid.GetTile(a).occupied = true;
    }

    public static int HexDistance(Vector2Int a, Vector2Int b)
    {
        int dq = a.x - b.x;
        int dr = a.y - b.y;
        int dy = -dq - dr;
        return Mathf.Max(Mathf.Abs(dq), Mathf.Abs(dr), Mathf.Abs(dy));
    }

    public bool InAttackRange(HexUnitController target)
    {
        return HexDistance(axial, target.axial) <= (data ? data.rng : 1);
    }

    public int DamagePreview(HexUnitController target)
    {
        int atk = data ? data.atk : 5;
        int def = target.data ? target.data.def : 2;
        return Mathf.Max(1, atk - def);
    }

    public void Attack(HexUnitController target)
    {
        int dmg = DamagePreview(target);
        target.currentHP -= dmg;
        if (target.currentHP <= 0)
        {
            grid.GetTile(target.axial).occupied = false;
            Destroy(target.gameObject);
        }
        // contraataque simple si está en rango
        if (target.currentHP > 0 && target.InAttackRange(this))
        {
            int counter = Mathf.Max(1, (target.data ? target.data.atk : 5) - (data ? data.def : 2));
            currentHP -= counter;
            if (currentHP <= 0)
            {
                grid.GetTile(axial).occupied = false;
                Destroy(gameObject);
            }
        }
    }
}

