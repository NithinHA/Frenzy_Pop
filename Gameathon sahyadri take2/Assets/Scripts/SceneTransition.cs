using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	Animator anim;

    public static SceneTransition instance;
    void Awake()
	{
		instance = this;
	}

    void Start()
    {
		anim = GetComponent<Animator>();
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			sceneTransition(SceneManager.GetActiveScene().buildIndex);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			sceneTransition(1);
		}
	}

    public void restartLevel()
    {
        if (PauseMenu.is_game_paused)           // in case pause menu is open
        {
            PauseMenu.instance.otherPauseButtons();     // disable pause menu and set time scale to 1
        }
        int scene_index = SceneManager.GetActiveScene().buildIndex;     // get current scene index
        StartCoroutine(sceneTransitionCoroutine(scene_index));
    }

    public void mainMenu()
    {
        if (PauseMenu.is_game_paused)           // in case pause menu is open
        {
            PauseMenu.instance.otherPauseButtons();     // disable pause menu and set time scale to 1
        }
        StartCoroutine(sceneTransitionCoroutine("start"));
    }

    public void sceneTransition(int scene_index)        // assign this method to buttons in the end-game canvas
	{
		StartCoroutine(sceneTransitionCoroutine(scene_index));
	}

	public void sceneTransition(string scene_name)
	{
		StartCoroutine(sceneTransitionCoroutine(scene_name));
	}

	IEnumerator sceneTransitionCoroutine(int scene_index)
	{
		anim.SetTrigger("end");
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(scene_index);
	}

	IEnumerator sceneTransitionCoroutine(string scene_name)
	{
		anim.SetTrigger("end");
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(scene_name);
	}

}
