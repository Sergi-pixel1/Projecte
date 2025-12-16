using UnityEngine;
using UnityEngine.UI;

public class BattleSystemUI : MonoBehaviour
{
    public BattleSystem battleSystem;
    public Button attackButton;
    public Button defendButton;
    public Button healButton;
    public Text battleLog;

    void Start()
    {
        attackButton.onClick.AddListener(OnAttack);
        defendButton.onClick.AddListener(OnDefend);
        healButton.onClick.AddListener(OnHeal);
    }

    void OnAttack()
    {
        battleSystem.PlayerAttack();
        battleLog.text = "El jugador ataca al enemigo.";
    }

    void OnDefend()
    {
        battleSystem.PlayerDefend();
        battleLog.text = "El jugador se defiende.";
    }

    void OnHeal()
    {
        battleSystem.PlayerHeal();
        battleLog.text = "El jugador se cura.";
    }
}

