using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI score_text;
    public TextMeshProUGUI health_text;

    //public GameObject ball;
    Health ball_health;

    void Awake()
    {
        ball_health = FindObjectOfType<Health>().GetComponent<Health>();
    }

    private void OnEnable()
    {
        Health.player_health_change += updateHealth;        // subscribe to health change event
        Score.score_change += updateScore;                  // subscribe to score change event
    }

    private void OnDisable()
    {
        Health.player_health_change -= updateHealth;        // unsubscribe from health change event
        Score.score_change -= updateScore;                  // unsubscribe from score change event
    }

    void updateScore()
    {
        score_text.text = "Score: " + Score.instance.GetScore();
    }

    void updateHealth()
    {
        health_text.text = "Health: " + ball_health.life;
    }
}
