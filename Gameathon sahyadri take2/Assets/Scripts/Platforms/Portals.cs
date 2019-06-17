using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public Transform other_portal;
    public bool can_teleport = true;
    public float can_teleport_delay;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (can_teleport)
            {
                anim.SetTrigger("player_hit");

                other_portal.GetComponent<Portals>().can_teleport = false;
                collision.transform.position = other_portal.position;
                StartCoroutine(canTeleportCoroutine());
            }
        }
    }
    IEnumerator canTeleportCoroutine()
    {
        yield return new WaitForSeconds(can_teleport_delay);
        other_portal.GetComponent<Portals>().can_teleport = true;
    }
}
