using UnityEngine;
using System.Linq;

public class SimpleHexAI : MonoBehaviour
{
    public void Act(HexUnitController enemy, HexGridManager grid, TurnManager tm)
    {
        var targets = tm.playerUnits.Where(u => u != null).ToList();
        if (targets.Count == 0) return;
        var closest = targets.OrderBy(t => HexUnitController.HexDistance(enemy.axial, t.axial)).First();

        int dist = HexUnitController.HexDistance(enemy.axial, closest.axial);
        if (dist <= enemy.data.rng)
        {
            enemy.Attack(closest);
            return;
        }
        var range = HexPathfinder.MovementRange(grid, enemy.axial, enemy.data.mov);
        var walkables = range.Where(p => !grid.GetTile(p).occupied);
        var best = walkables.OrderBy(p => HexUnitController.HexDistance(p, closest.axial)).FirstOrDefault();
        if (best != default) enemy.MoveTo(best);
        dist = HexUnitController.HexDistance(enemy.axial, closest.axial);
        if (dist <= enemy.data.rng) enemy.Attack(closest);
    }
}

