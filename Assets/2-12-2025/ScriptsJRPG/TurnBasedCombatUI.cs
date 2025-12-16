using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importante para usar TextMeshProUGUI

public class TurnBasedCombatUI : MonoBehaviour
{
    [Header("Stats")]
    public int playerHP = 20;
    public int enemyHP = 15;

    [Header("UI Elements")]
    public TextMeshProUGUI combatLog;   // TMP en lugar de Text
    public Button attackButton;
    public Button defendButton;

    private bool playerTurn = true;

    void Start()
    {
        // Asegúrate de arrastrar los objetos en el Inspector
        attackButton.onClick.AddListener(PlayerAttack);
        defendButton.onClick.AddListener(PlayerDefend);

        combatLog.text = "¡Comienza el combate!";
    }

    void PlayerAttack()
    {
        if (!playerTurn) return;

        int damage = Random.Range(2, 6);
        enemyHP -= damage;
        combatLog.text = "Jugador ataca y hace " + damage + " de daño.\nVida enemigo: " + enemyHP;

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

        combatLog.text = "Jugador se defiende y recibe menos daño en el próximo ataque.";
        playerTurn = false;
        EnemyTurn(true);
    }

    void EnemyTurn(bool playerDefended = false)
    {
        int damage = Random.Range(1, 4);
        if (playerDefended) damage = Mathf.Max(0, damage - 2);

        playerHP -= damage;
        combatLog.text += "\nEnemigo ataca y hace " + damage + " de daño.\nVida jugador: " + playerHP;

        if (playerHP <= 0)
        {
            combatLog.text += "\n¡El enemigo gana!";
            DisableButtons();
            return;
        }

        playerTurn = true;
    }

    void DisableButtons()
    {
        attackButton.interactable = false;
        defendButton.interactable = false;
    }
}


