using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCombat : Combatant
{
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider == null) return;
            if (hit.collider.gameObject != gameObject) return;

            BattleManager bm = Object.FindFirstObjectByType<BattleManager>();
            if (bm != null && bm.selectingTarget)
                bm.SelectEnemy(this);
        }
    }

    public void TakeTurn(BattleManager bm)
    {
        if (!IsAlive())
        {
            bm.EndCurrentTurn();
            return;
        }

        PlayerStats playerStats = Object.FindFirstObjectByType<PlayerStats>();
        if (playerStats != null)
        {
            int damage = Random.Range(1, 6);
            bm.Log("<color=red>" + unitName + " ataca al jugador y hace " + damage + " de daño</color>");
            playerStats.TakeDamage(damage);
        }
        else
        {
            bm.Log("<color=yellow>Error: No se encontró PlayerStats</color>");
        }

        bm.EndCurrentTurn();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0) Die();
    }

    void Die()
    {
        currentHP = 0;
        gameObject.SetActive(false);
        Debug.Log(unitName + " derrotado");
    }

    public void Highlight(bool active)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = active ? Color.red : Color.white;
    }

    
}






