using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	public static Score instance;
    private int score = 0;

    public delegate void scoreChange();
    public static event scoreChange score_change;

    private void Awake() {
		instance = this;
    }

    private void Start()
    {
        score_change?.Invoke();             // score value is initialized in UI by causing event
    }

    public int GetScore() {
        return score;
    }

    public void AddScore(int score) {
        this.score += score;
        score_change?.Invoke();             // score change event occurs
    }
}
