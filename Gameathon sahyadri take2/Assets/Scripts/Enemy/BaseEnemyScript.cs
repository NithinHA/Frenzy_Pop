using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
	public int enemy_life;
	public GameObject enemy_destroy_effect;

	public GameObject power_up_prefab;

    [SerializeField] private bool is_active;
    public bool isActive
    {
        get
        {
            return is_active;
        }
    }
	
	//public AudioSource death_audio;

    void Start()
    {

    }
	
    public virtual void Update()
    {
		if(enemy_life <= 0)
		{
			Instantiate(enemy_destroy_effect, transform.position, Quaternion.identity);
			//death_audio.Play();
			//add randomness for generating power up
			int random_spawn = Random.Range(0, 5);
			if (random_spawn % 5 != 0 && GameManager.instance.can_power_up)
			{
				Instantiate(power_up_prefab, transform.position, Quaternion.identity);
				GameManager.instance.dontSpawnPowerUp();
			}
			Destroy(gameObject);
			Score.instance.AddScore(10);
		}
		
	}

	public virtual void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			enemy_life--;
			collision.gameObject.GetComponent<PlayerControl>().jump_count++;
            //StartCoroutine(CameraShaker.instance.shake(.15f, .4f));
            // shake camera
            Camera.main.GetComponent<CameraShake_old>().shake(0.05f, 0.1f);
		}
	}
	
    public void activateEnemy()
    {
        is_active = true;
    }
    public void deactivateEnemy()
    {
        is_active = false;
    }
}
