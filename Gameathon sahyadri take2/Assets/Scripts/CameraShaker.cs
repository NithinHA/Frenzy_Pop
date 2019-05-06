using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
	public static CameraShaker instance;

	private void Awake()
	{
		instance = this;
	}

	void Start()
    {
        
    }
	
    void Update()
    {
        
    }

	public IEnumerator shake(float duration, float magnitude)		//.15, .4
	{
		Vector3 originalPos = transform.localPosition;
		float elapsed = 0;
		while(elapsed < duration)
		{
			float x = Random.Range(-1, 1) * magnitude;
			float y = Random.Range(-1, 1) * magnitude;
			transform.localPosition = new Vector3(x, y, originalPos.z);
			elapsed += Time.deltaTime;
			yield return null;
		}
		transform.localPosition = originalPos;
	}
}
