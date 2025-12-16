using UnityEngine;
using System.Linq;

public class HexInputController : MonoBehaviour
{
    public HexGridManager grid;
    public TurnManager tm;
    public RangeOverlay overlay;

    void Awake()
    {
        if (!grid) grid = FindObjectOfType<HexGridManager>();
        if (!tm) tm = FindObjectOfType<TurnManager>();
        if (!overlay) overlay = FindObjectOfType<RangeOverlay>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var a = HexGridManager.WorldToAxial(world, grid.hexSize);
            if (!grid.InBounds(a)) return;

            var tile = grid.GetTile(a);
            var unit = FindObjectsOfType<HexUnitController>().FirstOrDefault(u => u.axial == a);

            switch (tm.state)
            {
                case GameState.SelectUnit:
                    if (unit != null && unit.isPlayer)
                    {
                        var range = HexPathfinder.MovementRange(grid, unit.axial, unit.data.mov);
                        overlay.Show(range, grid);
                        tm.state = GameState.ShowMove;
                    }
                    break;

                case GameState.ShowMove:
                    if (!tile.occupied)
                    {
                        var curr = tm.CurrentUnit();
                        curr.MoveTo(a);
                        overlay.Clear();
                        tm.state = GameState.SelectTarget;
                    }
                    break;

                case GameState.SelectTarget:
                    var currUnit = tm.CurrentUnit();
                    if (unit != null && !unit.isPlayer && currUnit.InAttackRange(unit))
                    {
                        currUnit.Attack(unit);
                    }
                    tm.NextUnit();
                    break;
            }
        }
    }
}

