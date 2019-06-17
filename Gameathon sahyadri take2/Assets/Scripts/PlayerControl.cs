using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	[Header("ball data")]
	public int jump_count;
	public float velocity_magnitude;
	Rigidbody2D rb;

	[Header("other objects")]
	public GameObject particles;

	private bool fast_game = false;
    private bool slow_game = false;

    public AudioSource jumpAudio;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(startForce());       // wait for 1 second before game begins

        defaultTimeScale();     // initially set time scale to 1
    }

	IEnumerator startForce()
	{
		yield return new WaitForSeconds(1);
		rb.velocity = Vector2.down * velocity_magnitude;
	}
	
    void Update()
    {
		if (jump_count > 0 && !PauseMenu.is_game_paused)
		{
			if (Input.GetMouseButton(0))
			{
				changeTimeScale(0.1f);			// reduce time scale
			}
			else if (Input.GetMouseButtonUp(0))
			{
				Vector2 direction = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * 1000);
				//Debug.Log("transform:" + transform.position + "\ntarget:" + main_cam.ScreenToWorldPoint(Input.mousePosition) + "\ndirection:" + direction);
				direction.Normalize();                          // ignore magnitude and retain only the direction
				rb.velocity = Vector2.zero;						// nullify existing velocity of ball
				rb.velocity = direction * velocity_magnitude;		// add new velocity at computed direction
				
				changeTimeScale(1);				// increase time scale

				jump_count--;
			}
		}
    }

    private void OnBecameInvisible()        // if ball moves out of screen, kill it and restart
    {
        GetComponent<Health>().killPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            if (jump_count <= 0)
            {
                jump_count += collision.gameObject.GetComponent<Platform>().number_of_jumps;
            }
        }
        Instantiate(particles, transform.position, Quaternion.identity);
        jumpAudio.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            if (jump_count <= 0)
            {
                jump_count += collision.gameObject.GetComponent<Platform>().number_of_jumps;
            }
        }
        // Instantiate(particles, transform.position, Quaternion.identity);
        jumpAudio.Play();
    }

    void changeTimeScale(float time_scale)
	{    
        if (fast_game)                  // if speed up power is enabled, increase game time scale
        {
            Time.timeScale = time_scale * 1.5f;
        }
        else if (slow_game)             // if slow down power is enabled, decrease game time scale
        {
            Time.timeScale = time_scale * 0.5f;
        }
        else
        {
            Time.timeScale = time_scale;
        }
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
	}

    public void defaultTimeScale()          // set time scale to default values
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

	public void speedOrSlowPowerUp(int power)       // handles speed up or slow down power up
	{
		StartCoroutine(speedOrSlowCoroutine(power));
	}
	IEnumerator speedOrSlowCoroutine(int power)
	{
		if (power == 0) {
			fast_game = true;
			yield return new WaitForSeconds(5);
			fast_game = false;
		} else {
			slow_game = true;
			yield return new WaitForSeconds(5);
			slow_game = false;
		}
	}

    public void makeBallStatic()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;      // make the ball stop
        GetComponent<SpriteRenderer>().sprite = null;                       // remove ball sprite
        GetComponent<TrailRenderer>().time = 0;                             // remove ball trail
    }

    public void disableAllPowerUPs()
    {
        fast_game = false;
        slow_game = false;
    }

}
