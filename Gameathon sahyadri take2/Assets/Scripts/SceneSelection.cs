using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
	Animator anim;

    void Start()
    {
		anim = SceneTransition.instance.GetComponent<Animator>();
    }

    void Update()
    {
        
    }
	
	public void loadScene(string str)
	{
		StartCoroutine(loadSceneCoroutine(str));
	}

	IEnumerator loadSceneCoroutine(string str)
	{
		anim.SetTrigger("end");
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(str);
	}
}
