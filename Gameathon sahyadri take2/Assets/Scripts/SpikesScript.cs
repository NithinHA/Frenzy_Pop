using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{


    void Start()
    {
        
    }
	
    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
            int remaining_lives = collision.gameObject.GetComponent<Health>().life;
            collision.gameObject.GetComponent<Health>().reduceHealth(remaining_lives);     // reduce health to 0
		}
	}
}
