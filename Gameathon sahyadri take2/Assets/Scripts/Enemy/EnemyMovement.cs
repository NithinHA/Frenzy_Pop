using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : BaseEnemyScript {

	[Header("Enemy Movement")]
	public float speed;
	public Transform[] waypoints;
	int i = 0;

	void Start () {
		
	}
	
	public override void Update () {
		base.Update();

		if (Vector2.Distance(transform.position, waypoints[i].position) < 0.2f)
		{
			i++;				//choose the next point in waypoints[].
		}

		if (i == waypoints.Length)
			i = 0;				//once last point in waypoints[] is reached, move to first point, and keep looping

		transform.position = Vector2.MoveTowards(transform.position, waypoints[i].position, speed * Time.deltaTime);
	}

	public override void OnCollisionEnter2D(Collision2D collision)
	{
		base.OnCollisionEnter2D(collision);

		if (collision.gameObject.CompareTag("Player"))
		{
            collision.gameObject.GetComponent<Health>().reduceHealth(1);
		}
	}

}
