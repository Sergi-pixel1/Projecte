using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public List<Combatant> turnQueue = new List<Combatant>();
    public Combatant currentUnit;

    [Header("Player")]
    public int maxPlayerActions = 3;
    public int playerActions;

    [Header("Target Selection")]
    public bool selectingTarget = false;
    public EnemyCombat selectedEnemy;

    [Header("UI")]
    public Button endTurnButton;
    public TMP_Text combatLogText;
    public float messageDuration = 2f;

    private Queue<string> messageQueue = new Queue<string>();
    private bool showingMessage = false;

    void Start()
    {
        BuildTurnQueue();
        StartNextTurn();
    }

    // Construye la cola de turnos por velocidad
    void BuildTurnQueue()
    {
        turnQueue.Clear();
        Combatant[] allUnits = Object.FindObjectsByType<Combatant>(FindObjectsSortMode.None);
        turnQueue.AddRange(allUnits);
        turnQueue.Sort((a, b) => b.speed.CompareTo(a.speed));

        Debug.Log("Cola de turnos inicial:");
        foreach (var c in turnQueue)
        {
            Debug.Log("- " + c.unitName + " | Tipo: " + c.GetType() + " | Activo: " + c.gameObject.activeSelf + " | HP: " + c.currentHP);
        }
    }

    // Inicia el siguiente turno
    void StartNextTurn()
    {
        turnQueue.RemoveAll(c => !c.IsAlive());

        if (turnQueue.Count == 0)
        {
            Log("¡Todos los combatientes han muerto!");
            return;
        }

        currentUnit = turnQueue[0];
        turnQueue.RemoveAt(0);

        Log("=== NUEVO TURNO ===");
        Log("Turno de: " + currentUnit.unitName);

        if (currentUnit is PlayerCombat)
        {
            playerActions = maxPlayerActions;
            Log("Jugador tiene " + playerActions + " acciones disponibles.");
            endTurnButton?.gameObject.SetActive(true);
        }
        else if (currentUnit is EnemyCombat enemy)
        {
            Log("Turno del enemigo: " + enemy.unitName);
            enemy.TakeTurn(this);
        }
        else
        {
            Log("ERROR: Turno de un Combatant desconocido.");
            EndCurrentTurn();
        }
    }

    // Termina el turno actual
    public void EndCurrentTurn()
    {
        turnQueue.Add(currentUnit);
        StartNextTurn();
    }

    // ======================  
    // ACCIONES DEL JUGADOR
    // ======================
    public void StartTargetSelection()
    {
        if (!(currentUnit is PlayerCombat)) return;

        selectingTarget = true;
        HighlightEnemies(true);
        Log("Jugador: selecciona un enemigo para atacar.");
    }

    public void SelectEnemy(EnemyCombat enemy)
    {
        if (!selectingTarget) return;

        selectingTarget = false;
        HighlightEnemies(false);

        selectedEnemy = enemy;
        Log("Jugador ha seleccionado a: " + enemy.unitName);

        PlayerAttack();
    }

    void PlayerAttack()
    {
        if (!(currentUnit is PlayerCombat)) return;
        if (selectedEnemy == null) return;

        selectedEnemy.TakeDamage(3);
        playerActions--;

        Log("<color=green>Jugador ataca a " + selectedEnemy.unitName + " por 3 de daño. Acciones restantes: " + playerActions + "</color>");

        selectedEnemy = null;

        if (playerActions <= 0)
        {
            Log("Jugador ha gastado todas sus acciones. Fin del turno.");
            EndCurrentTurn();
        }
    }

    void Update()
    {
        if (currentUnit is PlayerCombat)
            endTurnButton?.gameObject.SetActive(playerActions > 0);

        if (!showingMessage && messageQueue.Count > 0)
            StartCoroutine(ShowNextMessage());
    }

    void HighlightEnemies(bool active)
    {
        foreach (EnemyCombat e in Object.FindObjectsByType<EnemyCombat>(FindObjectsSortMode.None))
        {
            if (e.IsAlive())
                e.Highlight(active);
        }
    }

    public void EndPlayerTurnButton()
    {
        if (currentUnit is PlayerCombat)
        {
            Log("Jugador decide pasar turno manualmente.");
            playerActions = 0;
            EndCurrentTurn();
        }
    }

    // ======================
    // LOG EN PANTALLA
    // ======================
    public void Log(string message)
    {
        Debug.Log(message);
        messageQueue.Enqueue(message);
    }

    IEnumerator ShowNextMessage()
    {
        if (messageQueue.Count == 0) yield break;

        showingMessage = true;
        string msg = messageQueue.Dequeue();

        if (combatLogText != null)
            combatLogText.text = msg;

        yield return new WaitForSeconds(messageDuration);

        if (combatLogText != null)
            combatLogText.text = "";

        showingMessage = false;
    }
}










