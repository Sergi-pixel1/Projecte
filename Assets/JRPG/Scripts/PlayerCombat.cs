using UnityEngine;

public class PlayerCombat : Combatant
{
    public BattleManager battleManager;

    protected override void Start()
    {
        base.Start();

        if (battleManager == null)
            battleManager = Object.FindFirstObjectByType<BattleManager>();
    }

    public void Attack()
    {
        if (battleManager.currentUnit != this)
        {
            Debug.Log("No es tu turno");
            return;
        }

        if (battleManager.playerActions <= 0)
        {
            Debug.Log("No quedan acciones");
            return;
        }

        battleManager.StartTargetSelection();
    }
}






