using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public GameObject power_up_destroy_particles;

    void Start()
    {
        
    }
	
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			GameObject player = collision.gameObject;
            //int random_power = Random.Range(0, 4);
            int random_power = 0;                           // TEMPORARY
            Debug.Log("Power = " + random_power);
			switch (random_power)
			{
				case 0:
					player.GetComponent<Health>().makeInvisible();
					break;
				case 1:
					player.GetComponent<PlayerControl>().speedOrSlowPowerUp(0);
					break;
				case 2:
					player.GetComponent<PlayerControl>().speedOrSlowPowerUp(1);
					break;
				case 3:
                    player.GetComponent<Health>().increaseHealth(1);
					break;
				default:
					Debug.Log("Unidentified power up");
					break;
			}
			Instantiate(power_up_destroy_particles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
