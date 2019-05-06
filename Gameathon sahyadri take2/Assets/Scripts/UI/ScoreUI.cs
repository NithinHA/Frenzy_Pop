using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
	TextMeshProUGUI text;


    void Start()
    {
		text = GetComponent<TextMeshProUGUI>();
    }
	
    void Update()
    {
		text.text = "Score: " + Score.instance.GetScore();
    }
}
