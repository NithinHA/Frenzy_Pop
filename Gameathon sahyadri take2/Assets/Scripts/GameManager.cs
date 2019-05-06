using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public bool can_power_up = true;

	public GameObject end_game_canvas;
	public GameObject score_canvas;

	private void Awake()
	{
		instance = this;
	}

	void Start()
    {
        
    }
	
    void Update()
    {
        
    }

	public void dontSpawnPowerUp(){
		StartCoroutine(dontSpawnPowerUP());
	}
	IEnumerator dontSpawnPowerUP()
	{
		can_power_up = false;
		yield return new WaitForSeconds(5);
		can_power_up = true;
	}

	public void endGame()
	{
		score_canvas.SetActive(false);
		end_game_canvas.SetActive(true);
	}
}
