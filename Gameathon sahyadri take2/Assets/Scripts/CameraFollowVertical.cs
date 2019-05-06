using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowVertical : MonoBehaviour {

	public GameObject player;

	Vector3 target;

	float upperbound;
	float lowerbound;

	public float screen_width, screen_height;

    float distance_delta_to_follow = .2f;

	void Start () {
        screen_width = Screen.width;
        screen_height = Screen.height;
        Debug.Log("width:" + screen_width);

		target = new Vector3(player.transform.position.x, player.transform.position.y, -10);

		upperbound = transform.position.y + distance_delta_to_follow;
		lowerbound = transform.position.y - distance_delta_to_follow;
	}
	
	void Update () {
		if (player != null)
		{
			if (player.transform.position.y > upperbound || player.transform.position.y < lowerbound)
			{
				target = new Vector3(target.x, player.transform.position.y, target.z);
				upperbound = player.transform.position.y + distance_delta_to_follow;
				lowerbound = player.transform.position.y - distance_delta_to_follow;
			}

            //if (player.transform.position.x > target.x + screen_width / 2f)
            //{
            //    //target = new Vector3(target.x + screen_width, target.y, -10);
            //    player.GetComponent<Health>().life = 0;
            //}
            //else if (player.transform.position.x < target.x - screen_width / 2f)
            //{
            //    //target = new Vector3(target.x - screen_width, target.y, -10);
            //    player.GetComponent<Health>().life = 0;
            //}
        }
	}

    private void LateUpdate()
	{
		if (target != transform.position)
		{
			transform.position = Vector3.Lerp(transform.position, target, 0.05f);
		}
	}
}
