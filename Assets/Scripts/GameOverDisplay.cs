using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI caveText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private int bestScore;

    private void Awake() {
        caveText.text = TerrainHandler.Seed.ToString();
    }

    public void UpdateScore(int score) {
        currentScoreText.text = score.ToString();
        if (score > bestScore) {
            bestScore = score;
            bestScoreText.text = bestScore.ToString();
        }
    }
    
    public void SetActive(bool active) {
        gameObject.SetActive(active);
    }
}
