using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedPlatform : MonoBehaviour {

	public float destroy_delay;
	public float platform_shake_magnitude;
	public GameObject platform_destroy_effect;

	//bool player_landed = false;

    Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}

	int count = 0;
	void Update () {
		//if (player_landed)
		//{
		//	if (count % 10 == 0)
		//	{
		//		transform.position += new Vector3(platform_shake_magnitude, 0, 0);		// moves platform right
		//		count++;
		//	}
		//	else if (count % 10 == 5)
		//	{
		//		transform.position -= new Vector3(platform_shake_magnitude, 0, 0);		// moves platform left
		//		count++;
		//	}
		//	else
		//		count++;
		//}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			StartCoroutine(DestroyPlatform());			// coroutine that destroys platform after some delay
			//player_landed = true;						// this is responsible for the platform shake before it is destroyed
		}
	}

	IEnumerator DestroyPlatform()
	{
        anim.SetBool("player_landed", true);
		yield return new WaitForSeconds(destroy_delay);
        anim.SetBool("player_landed", false);
		Instantiate(platform_destroy_effect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
