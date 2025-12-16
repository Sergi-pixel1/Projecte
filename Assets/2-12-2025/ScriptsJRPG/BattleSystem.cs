using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    public enum BattleState { Start, PlayerTurn, EnemyTurn, Won, Lost }

    [Header("Referencias")]
    public CharacterStats player;
    public CharacterStats enemy;

    [Header("UI")]
    public Slider playerHealthBar;
    public Slider enemyHealthBar;
    public Text battleLog;
    public Text turnIndicator;   // ?? Nuevo texto para mostrar turno
    public Button attackButton;
    public Button defendButton;
    public Button healButton;

    [Header("Eventos")]
    public UnityEvent onPlayerWin;
    public UnityEvent onPlayerLose;

    [Header("Ajustes de combate")]
    public float turnDelaySeconds = 0.6f;
    public float critChance = 0.15f;
    public float critMultiplier = 1.5f;
    public int defendReductionPercent = 50;
    public int healAmount = 20;
    public int healManaCost = 10;

    private BattleState state = BattleState.Start;
    private bool playerDefendingThisTurn = false;

    void Start()
    {
        InitBars();
        SetButtonsListeners();
        StartCoroutine(StartBattle());
    }

    void InitBars()
    {
        playerHealthBar.maxValue = player.maxHealth;
        enemyHealthBar.maxValue = enemy.maxHealth;
        UpdateBars();
    }

    void SetButtonsListeners()
    {
        // ?? Ahora puedes usar los métodos puente directamente en el Inspector si lo prefieres
        attackButton.onClick.AddListener(PlayerAttack);
        defendButton.onClick.AddListener(PlayerDefend);
        healButton.onClick.AddListener(PlayerHeal);
    }

    IEnumerator StartBattle()
    {
        battleLog.text = "¡Comienza el combate!";
        turnIndicator.text = "Preparando...";
        DisablePlayerInput();
        yield return new WaitForSeconds(turnDelaySeconds);
        state = BattleState.PlayerTurn;
        battleLog.text = "Tu turno.";
        turnIndicator.text = "? Turno del Jugador";
        EnablePlayerInput();
    }

    // ---- Métodos puente para Inspector
    public void PlayerAttack() => StartCoroutine(HandlePlayerAttack());
    public void PlayerDefend() => StartCoroutine(HandlePlayerDefend());
    public void PlayerHeal() => StartCoroutine(HandlePlayerHeal());

    // ---- Turno del jugador
    IEnumerator HandlePlayerAttack()
    {
        DisablePlayerInput();
        state = BattleState.EnemyTurn;

        int damage = CalculateDamage(attacker: player, defender: enemy);
        enemy.TakeDamage(damage);
        battleLog.text = $"Atacas con fuerza. Daño infligido: {damage}";
        UpdateBars();

        yield return new WaitForSeconds(turnDelaySeconds);

        if (CheckEnd()) yield break;
        yield return EnemyTurn();
    }

    IEnumerator HandlePlayerDefend()
    {
        DisablePlayerInput();
        state = BattleState.EnemyTurn;

        playerDefendingThisTurn = true;
        battleLog.text = "Te cubres. Reducirás el daño del próximo golpe.";
        yield return new WaitForSeconds(turnDelaySeconds);

        if (CheckEnd()) yield break;
        yield return EnemyTurn();
    }

    IEnumerator HandlePlayerHeal()
    {
        DisablePlayerInput();
        state = BattleState.EnemyTurn;

        if (player.currentMana >= healManaCost)
        {
            player.currentMana -= healManaCost;
            player.Heal(healAmount);
            battleLog.text = $"Recuperas {healAmount} HP. Mana restante: {player.currentMana}/{player.maxMana}";
            UpdateBars();
        }
        else
        {
            battleLog.text = "No tienes suficiente mana.";
        }

        yield return new WaitForSeconds(turnDelaySeconds);

        if (CheckEnd()) yield break;
        yield return EnemyTurn();
    }

    // ---- Turno del enemigo
    IEnumerator EnemyTurn()
    {
        battleLog.text = "Turno del enemigo...";
        turnIndicator.text = "? Turno del Enemigo";
        yield return new WaitForSeconds(turnDelaySeconds);

        int baseDamage = CalculateDamage(attacker: enemy, defender: player);

        if (playerDefendingThisTurn)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * (1f - defendReductionPercent / 100f));
            playerDefendingThisTurn = false;
        }

        player.TakeDamage(baseDamage);
        battleLog.text = $"El enemigo te golpea: {baseDamage} de daño.";
        UpdateBars();

        yield return new WaitForSeconds(turnDelaySeconds);

        if (CheckEnd()) yield break;
        state = BattleState.PlayerTurn;
        battleLog.text = "Tu turno.";
        turnIndicator.text = "? Turno del Jugador";
        EnablePlayerInput();
    }

    // ---- Utilidades
    int CalculateDamage(CharacterStats attacker, CharacterStats defender)
    {
        int baseDamage = Mathf.Max(1, attacker.attackPower - defender.defense);

        bool isCrit = Random.value < critChance;
        if (isCrit)
            baseDamage = Mathf.RoundToInt(baseDamage * critMultiplier);

        return baseDamage;
    }

    void UpdateBars()
    {
        playerHealthBar.value = player.currentHealth;
        enemyHealthBar.value = enemy.currentHealth;
    }

    bool CheckEnd()
    {
        if (enemy.IsDead())
        {
            state = BattleState.Won;
            battleLog.text = "¡Victoria!";
            turnIndicator.text = "? Combate terminado";
            DisablePlayerInput();
            onPlayerWin?.Invoke();

            Destroy(enemy.gameObject); // ?? El enemigo desaparece
            return true;
        }

        if (player.IsDead())
        {
            state = BattleState.Lost;
            battleLog.text = "Has sido derrotado...";
            turnIndicator.text = "? Combate terminado";
            DisablePlayerInput();
            onPlayerLose?.Invoke();

            Destroy(player.gameObject); // ?? El jugador desaparece
            return true;
        }

        return false;
    }

    void EnablePlayerInput()
    {
        attackButton.interactable = true;
        defendButton.interactable = true;
        healButton.interactable = true;
    }

    void DisablePlayerInput()
    {
        attackButton.interactable = false;
        defendButton.interactable = false;
        healButton.interactable = false;
    }
}



