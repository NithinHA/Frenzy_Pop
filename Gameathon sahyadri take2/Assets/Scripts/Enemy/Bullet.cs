using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public GameObject bullet_particles;
	public float bullet_speed;

    public float bullet_destroy_timer = 3f;

	Rigidbody2D rb;

	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * bullet_speed;

        StartCoroutine(bulletSelfDestruct());
    }
	
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("platform") || collision.CompareTag("Player"))
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				collision.GetComponent<Health>().reduceHealth(1);
				// camera shake is done in health script and not here
			}
			Instantiate(bullet_particles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

    IEnumerator bulletSelfDestruct()
    {
        yield return new WaitForSeconds(bullet_destroy_timer);
        Instantiate(bullet_particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
