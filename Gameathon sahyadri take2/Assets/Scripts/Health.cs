using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int life { get; set; } = 3;
	bool is_player_alive = true;

    public bool is_invisible { get; private set; } = false;
    public float time_between_damage = 1.5f;

    Coroutine make_invisible_coroutine;

	public GameObject ball_destroy_effect;

    public delegate void healthChangeDelegate();
    public static healthChangeDelegate player_health_change;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        player_health_change?.Invoke();             // health value is initialized in UI by causing event

        //is_invisible = false;     // done above
    }
	
    void Update()
    {
        if(life <= 0 && is_player_alive)
		{
			playerDeath();
		}
    }

	public void reduceHealth(int delta_health)
	{
        if (!is_invisible)
        {
            life -= delta_health;
            make_invisible_coroutine = StartCoroutine(timeBetweenHealthReduction());
            if (Camera.main != null)       // was getting null reference exception when ball went out of bounds and died
                Camera.main.GetComponent<CameraShake_old>().shake(.05f, .1f);           // shake camera
            player_health_change?.Invoke();             // player health change event occurs
        }
	}

    public void increaseHealth(int delta_health)
    {
        life += delta_health;
        player_health_change?.Invoke();         // player health change event occurs
    }

    public void killPlayer()
    {
        life = 0;
        player_health_change?.Invoke();             // player health change event occurs
    }

    IEnumerator timeBetweenHealthReduction()
    {
        is_invisible = true;
        anim.SetBool("is_invisible", true);     //play is_invisible anim
        yield return new WaitForSeconds(time_between_damage);
        //if (make_invisible_coroutine == null)
        {
            anim.SetBool("is_invisible", false);     //stop ins_invisible anim
            is_invisible = false;
        }
    }

	public void makeInvisible()
	{
        if (make_invisible_coroutine != null)
            StopCoroutine(make_invisible_coroutine);

		make_invisible_coroutine = StartCoroutine(makeVisibleCoroutine());
	}

	IEnumerator makeVisibleCoroutine()
	{
		is_invisible = true;
        anim.SetBool("is_invisible", true);     //play is_invisible anim
        // change the color of the ball to indicate the player
        // maybe play audio with a lower pitch
		yield return new WaitForSeconds(5);
        // change color to default
        // change audio to default
        anim.SetBool("is_invisible", false);     //stop ins_invisible anim
        is_invisible = false;
	}

	void playerDeath()
	{
		Debug.Log("Player dieded");
		is_player_alive = false;
		Instantiate(ball_destroy_effect, transform.position, Quaternion.identity);

        GetComponent<PlayerControl>().makeBallStatic();         // stop player and disable player sprite
        GetComponent<PlayerControl>().disableAllPowerUPs();     // disables speed and slow power ups

        PauseMenu.isGamePaused = true;                          // pausing the game prevents PlayerControl script from altering the time scale
        GetComponent<PlayerControl>().defaultTimeScale();       // set time scale to default. Overcomes the scenario in which ball is already in slo-mo when death occurs
        SceneTransition.instance.restartLevel();                // finally restart the scene
	}

}
