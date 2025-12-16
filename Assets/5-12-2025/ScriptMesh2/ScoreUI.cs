using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;

    void Update()
    {
        if (ScoreManager.instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.instance.score;

            if (ScoreManager.instance.comboMultiplicador > 1)
            {
                comboText.text = "Combo x" + ScoreManager.instance.comboMultiplicador;
                comboText.gameObject.SetActive(true);
            }
            else
            {
                comboText.gameObject.SetActive(false);
            }
        }
    }
}
