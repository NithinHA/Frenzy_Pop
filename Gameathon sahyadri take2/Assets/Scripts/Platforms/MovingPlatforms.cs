using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : Platforms {

	[Header("Movement")]
	public Vector2[] waypoints;					// positions that the platform will cover
	public float speed;

	protected override void Start () {
		base.Start();			// calls the parent Start()
		
	}

	int next_point = 0;
	float waypoint_radius = 0.1f;

	void FixedUpdate () {		// first checks if platform has reached next waypoint position. Makes use of waypoint_radius to prevent minute
		if(Vector2.Distance(waypoints[next_point], transform.position) < waypoint_radius)		// variation in distance measurement
		{
			next_point += 1;
			if (next_point >= waypoints.Length)		// once player has reached final position in waypoints array, set next position to 0th position
				next_point = 0;
		}
		transform.position = Vector2.MoveTowards(transform.position, waypoints[next_point], speed / 100);		// moves the platform towards
	}																											// next waypoint position
}
