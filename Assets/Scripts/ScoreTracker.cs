using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI scoreDisplay;

    [SerializeField]
    private Transform tracked;

    public int Score {
        get => score;
    }

    private int score;
    
    void Start() {
        scoreDisplay.text = "0";
    }

    void Update() {
        var curPos = (int)tracked.transform.position.x;
        if (curPos <= score) return;
        score = curPos;
        scoreDisplay.text = score.ToString();
    }

    public void Restart(Transform newTracked) {
        tracked = newTracked;
        gameObject.SetActive(true);
        score = 0;
    }
}
