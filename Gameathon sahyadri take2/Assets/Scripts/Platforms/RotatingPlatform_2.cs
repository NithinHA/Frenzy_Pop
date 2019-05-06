using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform_2 : MonoBehaviour
{
    [Header("Rotation info")]
    public int count;               // number of floors on the platform
    public float radius;			// distance of floors from center
    public bool is_clockwise;
    public float rotation_speed = 70f;
    public GameObject floor;      // array of floor GameObejects.. Make sure to keep size equal to count

    [Header("Alternate Rotations")]
    public bool is_alternating;         // should the rotation direction change mid rotation
    public float alternating_time;      // time after which rotation direction changes
    bool can_alternate = true;

    GameObject[] floor_items;		// array that we use to store each individual floor of this platform

    void Start()
    {
        floor_items = new GameObject[count];

        float init_angle = 0;       // initialize each floor of platform starting at angle 0
        for (int i = 0; i < count; i++)
        {       // position of item = (x+rcos(Ɵ), y+rsin(Ɵ));	here, x and y are position of center and Ɵ is angle in degree which we convert to radian (*pi/180) 
            Vector2 pos = new Vector2(transform.position.x + radius * Mathf.Cos(init_angle * Mathf.PI / 180), transform.position.y + radius * Mathf.Sin(init_angle * Mathf.PI / 180));
            GameObject obj = Instantiate(floor, pos, Quaternion.Euler(new Vector3(0, 0, init_angle)));
            init_angle += 360 / count;      // increment Ɵ by the division.. eg.- if count=4 => init_angle += 90		since 90*4=360
            obj.transform.SetParent(transform);
            floor_items[i] = obj;
        }
    }

    void Update()
    {
        if(is_clockwise)
            transform.Rotate(Vector3.forward * -rotation_speed * Time.deltaTime);
        else
            transform.Rotate(Vector3.forward * rotation_speed * Time.deltaTime);

        if (is_alternating)         // if this rotating platform can alternate rotations, then
        {
            if(can_alternate)       // if time has exceeded alternating time
                StartCoroutine(alternateRotations());
        }
    }

    IEnumerator alternateRotations()
    {
        can_alternate = false;
        is_clockwise = !is_clockwise;       // change rotation direction
        yield return new WaitForSeconds(alternating_time);      // wait for alternating rotation time
        can_alternate = true;
    }
}
