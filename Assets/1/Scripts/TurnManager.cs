using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum GameState { SelectUnit, ShowMove, Move, SelectTarget, EnemyTurn }

public class TurnManager : MonoBehaviour
{
    public GameState state = GameState.SelectUnit;
    public List<HexUnitController> playerUnits = new();
    public List<HexUnitController> enemyUnits = new();
    private int actingIndex = 0;

    void Awake()
    {
        // Auto-registrar unidades en escena
        var all = FindObjectsOfType<HexUnitController>();
        foreach (var u in all)
        {
            if (u.isPlayer) playerUnits.Add(u);
            else enemyUnits.Add(u);
        }
    }

    public HexUnitController CurrentUnit()
    {
        return actingIndex < playerUnits.Count ? playerUnits[actingIndex] : null;
    }

    public void BeginPlayerTurn()
    {
        state = GameState.SelectUnit;
        actingIndex = 0;
    }

    public void NextUnit()
    {
        actingIndex++;
        if (actingIndex >= playerUnits.Count)
        {
            state = GameState.EnemyTurn;
            ActEnemyTurn();
        }
        else
        {
            state = GameState.SelectUnit;
        }
    }

    void ActEnemyTurn()
    {
        var grid = FindObjectOfType<HexGridManager>();
        foreach (var enemy in enemyUnits.ToList())
        {
            if (enemy == null) continue;
            var targets = playerUnits.Where(u => u != null).ToList();
            if (targets.Count == 0) break;
            var closest = targets.OrderBy(t => HexUnitController.HexDistance(enemy.axial, t.axial)).First();
            int dist = HexUnitController.HexDistance(enemy.axial, closest.axial);

            if (dist <= enemy.data.rng)
            {
                enemy.Attack(closest);
            }
            else
            {
                var range = HexPathfinder.MovementRange(grid, enemy.axial, enemy.data.mov);
                var walkables = range.Where(p => !grid.GetTile(p).occupied);
                var best = walkables.OrderBy(p => HexUnitController.HexDistance(p, closest.axial)).FirstOrDefault();
                if (best != default) enemy.MoveTo(best);
                dist = HexUnitController.HexDistance(enemy.axial, closest.axial);
                if (dist <= enemy.data.rng) enemy.Attack(closest);
            }
        }
        BeginPlayerTurn();
    }
}

