using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : BaseEnemyScript {
	
    public GameObject bulletPrefab;
	public float rate_of_fire;
    // Cached References
    private bool canShoot = true;
    public Transform gunsTransform;

	Animator anim;

	//public AudioSource thunder_audio;

    void Start() {
        gunsTransform = GetComponentInChildren<Transform>();
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update() {
		base.Update();

        if (canShoot && base.isActive) {
            // Shoot
            for (int i = 1; i < transform.childCount; i++) {    // starts from 1 because 0th child is the range
                Vector2 position = transform.GetChild(i).transform.position;
                Quaternion rotation = transform.GetChild(i).transform.rotation;

                Instantiate(bulletPrefab, position, rotation);
            }
			//thunder_audio.Play();
            canShoot = false;
            // wait for rate_of_fire seconds before taking next shot
            StartCoroutine(WaitSeconds(rate_of_fire));
        }

		if (base.isActive)
		{
			anim.SetBool("is_active", true);
		}
		else
		{
			anim.SetBool("is_active", false);
		}
	}

    IEnumerator WaitSeconds(float seconds) {

        yield return new WaitForSeconds(seconds);
        canShoot = true;
    }
	
}
