using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
	public GameObject ball;
	TextMeshProUGUI text;
	//public Health health;
	private Health health;

	void Start()
	{
		text = GetComponent<TextMeshProUGUI>();
		health = ball.GetComponent<Health>();
	}

	void Update()
	{
		text.text = "Lives: " + health.life;
	}
}
