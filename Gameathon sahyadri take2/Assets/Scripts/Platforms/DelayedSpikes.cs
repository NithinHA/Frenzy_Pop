using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSpikes : MonoBehaviour
{
    [Header("Spikes enable and disable timer")]
    public float spikes_enable_delay;
    public float spikes_disable_delay;

    [Header("Spikes enable effects")]
    public float platform_shake_magnitude;
    public GameObject spike_popup_effect;

    Transform[] all_spikes;

    bool spikes_enabled = false;

    Animator anim;

    void Awake()
    {
        all_spikes = new Transform[transform.childCount];
        for(int i=0; i<transform.childCount; i++)
        {
            all_spikes[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!spikes_enabled)
        {
            if (collision.collider.CompareTag("Player"))
            {
                StartCoroutine(enableSpikesCoroutine());            // coroutine that enables spikes after certain time
            }
        }
        else
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Health>().reduceHealth(1);
            }
        }
    }

    IEnumerator enableSpikesCoroutine()
    {
        anim.SetBool("player_landed", true);
        yield return new WaitForSeconds(spikes_enable_delay);
        anim.SetBool("player_landed", false);

        Instantiate(spike_popup_effect, transform.position, Quaternion.identity);
        spikes_enabled = true;

        foreach(Transform spike in all_spikes)
        {
            spike.gameObject.SetActive(true);
        }
        StartCoroutine(disableSpikesCoroutine());
    }

    IEnumerator disableSpikesCoroutine()
    {
        yield return new WaitForSeconds(spikes_disable_delay);
        spikes_enabled = false;

        foreach (Transform spike in all_spikes)
        {
            spike.gameObject.SetActive(false);
        }
    }
}
