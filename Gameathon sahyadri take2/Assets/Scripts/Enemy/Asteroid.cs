using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public GameObject meteor_particles;

    private void Start()
    {
        StartCoroutine(selfDestruct());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.gameObject.GetComponent<Health>().reduceHealth(1);
			Instantiate(meteor_particles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		} else if (collision.CompareTag("platform"))
		{
			Instantiate(meteor_particles, transform.position, Quaternion.identity);
			Destroy(gameObject);
        }
    }

    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
