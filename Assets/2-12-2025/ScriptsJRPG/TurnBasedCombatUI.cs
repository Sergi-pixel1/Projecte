using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnBasedCombatUI : MonoBehaviour
{
    [Header("Stats")]
    public int playerHP = 20;
    public int enemyHP = 15;

    public int playerMana = 10;
    public int maxMana = 10;

    [Header("UI Elements")]
    public TextMeshProUGUI combatLog;
    public Button attackButton;
    public Button defendButton;
    public Button healButton;

    private bool playerTurn = true;

    void Start()
    {
        attackButton.onClick.AddListener(PlayerAttack);
        defendButton.onClick.AddListener(PlayerDefend);
        healButton.onClick.AddListener(PlayerHeal);

        combatLog.text = "¡Comienza el combate!";
    }

    void PlayerAttack()
    {
        if (!playerTurn) return;

        int damage = Random.Range(2, 6);
        enemyHP -= damage;

        combatLog.text =
            "Jugador ataca y hace " + damage + " de daño." +
            "\nVida enemigo: " + enemyHP;

        if (enemyHP <= 0)
        {
            combatLog.text += "\n¡El jugador gana!";
            DisableButtons();
            return;
        }

        playerTurn = false;
        EnemyTurn();
    }

    void PlayerDefend()
    {
        if (!playerTurn) return;

        combatLog.text =
            "Jugador se defiende y recibirá menos daño.";

        playerTurn = false;
        EnemyTurn(true);
    }

    void PlayerHeal()
    {
        if (!playerTurn) return;

        if (playerMana < 5)
        {
            combatLog.text += "\nNo hay suficiente Mana para curarse.";
            return;
        }

        int heal = Random.Range(4, 7);
        playerHP += heal;
        playerMana -= 5;

        combatLog.text =
            "Jugador se cura " + heal + " de vida." +
            "\nVida jugador: " + playerHP +
            "\nMana restante: " + playerMana;

        playerTurn = false;
        EnemyTurn();
    }

    void EnemyTurn(bool playerDefended = false)
    {
        int damage = Random.Range(1, 4);
        if (playerDefended)
            damage = Mathf.Max(0, damage - 2);

        playerHP -= damage;

        combatLog.text +=
            "\nEnemigo ataca y hace " + damage + " de daño." +
            "\nVida jugador: " + playerHP;

        if (playerHP <= 0)
        {
            combatLog.text += "\n¡El enemigo gana!";
            DisableButtons();
            return;
        }

        // 
        playerMana = Mathf.Min(maxMana, playerMana + 1);
        combatLog.text +=
            "\nJugador recupera 1 de Mana." +
            "\nMana actual: " + playerMana;

        playerTurn = true;
    }

    void DisableButtons()
    {
        attackButton.interactable = false;
        defendButton.interactable = false;
        healButton.interactable = false;
    }
}




